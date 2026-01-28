using System.Globalization;
using TimeStudiesApp.Controls;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

/// <summary>
/// Main application form for REFA Time Study.
/// </summary>
public partial class FrmMain : Form
{
    private readonly SettingsService _settingsService;
    private readonly DefinitionService _definitionService;
    private readonly RecordingService _recordingService;
    private readonly CsvExportService _csvExportService;

    private DefinitionEditorControl? _definitionEditor;
    private RecordingControl? _recordingControl;
    private ResultsControl? _resultsControl;

    private TimeStudyDefinition? _currentDefinition;
    private string? _currentDefinitionPath;
    private TimeStudyRecording? _lastCompletedRecording;

    private enum ViewMode
    {
        DefinitionEditor,
        Recording,
        Results
    }

    private ViewMode _currentView = ViewMode.DefinitionEditor;

    public FrmMain()
    {
        _settingsService = new SettingsService();
        _definitionService = new DefinitionService(_settingsService);
        _recordingService = new RecordingService(_settingsService);
        _csvExportService = new CsvExportService(_settingsService);

        // Apply culture before InitializeComponent
        ApplyCulture();

        InitializeComponent();
        ApplyLocalization();
        SetupToolbarImages();
        SetupEventHandlers();

        // Apply saved window position
        _settingsService.ApplyWindowBounds(this);

        // Ensure directories exist
        _settingsService.Settings.EnsureDirectoriesExist();
    }

    private void ApplyCulture()
    {
        string language = _settingsService.Settings.UiLanguage;

        try
        {
            CultureInfo culture = new(language);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
        catch (CultureNotFoundException)
        {
            // Use default culture
        }
    }

    private void ApplyLocalization()
    {
        Text = Resources.AppTitle;

        // Menu: File
        _mnuFile.Text = Resources.MenuFile;
        _mnuFileNew.Text = Resources.MenuFileNew;
        _mnuFileOpen.Text = Resources.MenuFileOpen;
        _mnuFileSave.Text = Resources.MenuFileSave;
        _mnuFileSaveAs.Text = Resources.MenuFileSaveAs;
        _mnuFileExport.Text = Resources.MenuFileExportCsv;
        _mnuFileExit.Text = Resources.MenuFileExit;

        // Menu: Tools
        _mnuTools.Text = Resources.MenuTools;
        _mnuToolsSettings.Text = Resources.MenuToolsSettings;

        // Menu: Help
        _mnuHelp.Text = Resources.MenuHelp;
        _mnuHelpAbout.Text = Resources.MenuHelpAbout;

        // Toolbar
        _tsbNew.Text = Resources.ToolNew;
        _tsbNew.ToolTipText = Resources.MenuFileNew;
        _tsbOpen.Text = Resources.ToolOpen;
        _tsbOpen.ToolTipText = Resources.MenuFileOpen;
        _tsbSave.Text = Resources.ToolSave;
        _tsbSave.ToolTipText = Resources.MenuFileSave;
        _tsbStartStop.Text = Resources.ToolStartRecording;
        _tsbStartStop.ToolTipText = Resources.BtnStartRecording;
        _tsbExport.Text = Resources.ToolExport;
        _tsbExport.ToolTipText = Resources.MenuFileExportCsv;

        // Status bar
        _sslStatus.Text = Resources.StatusReady;
    }

    private void SetupToolbarImages()
    {
        int iconSize = 24;
        Color foreColor = SystemColors.ControlText;

        _tsbNew.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.NewDocument, iconSize, foreColor);
        _tsbOpen.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.OpenFile, iconSize, foreColor);
        _tsbSave.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Save, iconSize, foreColor);
        _tsbStartStop.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Play, iconSize, foreColor);
        _tsbExport.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Export, iconSize, foreColor);
    }

    private void SetupEventHandlers()
    {
        _recordingService.RecordingStarted += RecordingService_RecordingStarted;
        _recordingService.RecordingStopped += RecordingService_RecordingStopped;
    }

    private void ShowView(ViewMode mode)
    {
        _currentView = mode;

        // Hide all views first
        if (_definitionEditor is not null)
        {
            _definitionEditor.Visible = false;
        }

        if (_recordingControl is not null)
        {
            _recordingControl.Visible = false;
        }

        if (_resultsControl is not null)
        {
            _resultsControl.Visible = false;
        }

        // Show the requested view
        switch (mode)
        {
            case ViewMode.DefinitionEditor:
                EnsureDefinitionEditor();
                _definitionEditor!.Visible = true;
                _definitionEditor.BringToFront();
                break;

            case ViewMode.Recording:
                EnsureRecordingControl();
                _recordingControl!.Visible = true;
                _recordingControl.BringToFront();
                break;

            case ViewMode.Results:
                EnsureResultsControl();
                _resultsControl!.Visible = true;
                _resultsControl.BringToFront();
                break;
        }

        UpdateUIState();
    }

    private void EnsureDefinitionEditor()
    {
        if (_definitionEditor is null)
        {
            _definitionEditor = new DefinitionEditorControl
            {
                Dock = DockStyle.Fill
            };
            _definitionEditor.DefinitionChanged += DefinitionEditor_DefinitionChanged;
            _pnlContent.Controls.Add(_definitionEditor);
        }

        _definitionEditor.Definition = _currentDefinition;
    }

    private void EnsureRecordingControl()
    {
        if (_recordingControl is null)
        {
            _recordingControl = new RecordingControl(_recordingService, _settingsService.Settings.ButtonSize)
            {
                Dock = DockStyle.Fill
            };
            _recordingControl.RecordingCompleted += RecordingControl_RecordingCompleted;
            _pnlContent.Controls.Add(_recordingControl);
        }

        _recordingControl.Definition = _currentDefinition;
    }

    private void EnsureResultsControl()
    {
        if (_resultsControl is null)
        {
            _resultsControl = new ResultsControl
            {
                Dock = DockStyle.Fill
            };
            _pnlContent.Controls.Add(_resultsControl);
        }

        _resultsControl.Recording = _lastCompletedRecording;
    }

    private void UpdateUIState()
    {
        bool hasDefinition = _currentDefinition is not null;
        bool isRecording = _recordingService.IsRecording;
        bool hasCompletedRecording = _lastCompletedRecording is not null;
        bool definitionLocked = _currentDefinition?.IsLocked ?? false;

        // Menu items
        _mnuFileSave.Enabled = hasDefinition && !definitionLocked && _currentView == ViewMode.DefinitionEditor;
        _mnuFileSaveAs.Enabled = hasDefinition && _currentView == ViewMode.DefinitionEditor;
        _mnuFileExport.Enabled = hasCompletedRecording;

        // Toolbar
        _tsbSave.Enabled = hasDefinition && !definitionLocked && _currentView == ViewMode.DefinitionEditor;
        _tsbExport.Enabled = hasCompletedRecording;

        // Start/Stop button
        if (isRecording)
        {
            _tsbStartStop.Text = Resources.ToolStopRecording;
            _tsbStartStop.ToolTipText = Resources.BtnStopRecording;
            _tsbStartStop.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Stop, 24, SystemColors.ControlText);
        }
        else
        {
            _tsbStartStop.Text = Resources.ToolStartRecording;
            _tsbStartStop.ToolTipText = Resources.BtnStartRecording;
            _tsbStartStop.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Play, 24, SystemColors.ControlText);
        }

        _tsbStartStop.Enabled = hasDefinition || isRecording;

        // Status text
        if (isRecording)
        {
            _sslStatus.Text = Resources.StatusRecording;
        }
        else if (!hasDefinition)
        {
            _sslStatus.Text = Resources.StatusNoDefinition;
        }
        else
        {
            _sslStatus.Text = Resources.StatusReady;
        }
    }

    private void NewDefinition()
    {
        if (!CheckUnsavedChanges())
        {
            return;
        }

        _currentDefinition = _definitionService.CreateNew();
        _currentDefinitionPath = null;
        ShowView(ViewMode.DefinitionEditor);
    }

    private void OpenDefinition()
    {
        if (!CheckUnsavedChanges())
        {
            return;
        }

        using OpenFileDialog dialog = new()
        {
            Title = Resources.TitleOpenDefinition,
            Filter = Resources.FilterDefinition,
            InitialDirectory = _settingsService.Settings.DefinitionsDirectory
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _currentDefinition = _definitionService.Load(dialog.FileName);
                _currentDefinitionPath = dialog.FileName;
                ShowView(ViewMode.DefinitionEditor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Error loading definition: {ex.Message}",
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void SaveDefinition()
    {
        if (_currentDefinition is null || _definitionEditor is null)
        {
            return;
        }

        // Validate first
        List<string> errors = _definitionEditor.Validate();

        if (errors.Count > 0)
        {
            string message = Resources.MsgValidationErrors + "\n\n" + string.Join("\n", errors);
            MessageBox.Show(
                this,
                message,
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        _definitionEditor.SaveChanges();

        try
        {
            _currentDefinitionPath = _definitionService.Save(_currentDefinition, _currentDefinitionPath);
            MessageBox.Show(
                this,
                Resources.MsgSaveSuccess,
                Resources.TitleInformation,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Error saving definition: {ex.Message}",
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void SaveDefinitionAs()
    {
        if (_currentDefinition is null || _definitionEditor is null)
        {
            return;
        }

        _definitionEditor.SaveChanges();

        using SaveFileDialog dialog = new()
        {
            Title = Resources.TitleSaveDefinition,
            Filter = Resources.FilterDefinition,
            InitialDirectory = _settingsService.Settings.DefinitionsDirectory,
            FileName = $"{_currentDefinition.Name}.json"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _currentDefinitionPath = _definitionService.Save(_currentDefinition, dialog.FileName);
                MessageBox.Show(
                    this,
                    Resources.MsgSaveSuccess,
                    Resources.TitleInformation,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Error saving definition: {ex.Message}",
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void ExportCsv()
    {
        if (_lastCompletedRecording is null)
        {
            return;
        }

        using SaveFileDialog dialog = new()
        {
            Title = Resources.TitleExportCsv,
            Filter = Resources.FilterCsv,
            InitialDirectory = _settingsService.Settings.BaseDirectory,
            FileName = $"{_lastCompletedRecording.DefinitionName}_{_lastCompletedRecording.StartedAt:yyyy-MM-dd_HHmmss}.csv"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _csvExportService.Export(_lastCompletedRecording, dialog.FileName);
                MessageBox.Show(
                    this,
                    Resources.MsgExportSuccess,
                    Resources.TitleInformation,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Error exporting CSV: {ex.Message}",
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void ToggleRecording()
    {
        if (_recordingService.IsRecording)
        {
            // Show confirmation if configured
            if (_settingsService.Settings.ConfirmBeforeClosingRecording)
            {
                DialogResult result = MessageBox.Show(
                    this,
                    Resources.MsgConfirmStop,
                    Resources.TitleConfirm,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                _recordingService.StopRecording();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        else
        {
            if (_currentDefinition is null)
            {
                return;
            }

            // Validate definition has a default step
            if (_currentDefinition.DefaultProcessStep is null)
            {
                MessageBox.Show(
                    this,
                    Resources.MsgNoDefaultStep,
                    Resources.TitleWarning,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Lock the definition
            if (!_currentDefinition.IsLocked && _currentDefinitionPath is not null)
            {
                _definitionService.LockDefinition(_currentDefinition, _currentDefinitionPath);
            }

            ShowView(ViewMode.Recording);

            try
            {
                _recordingService.StartRecording(_currentDefinition);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void ShowSettings()
    {
        using SettingsDialog dialog = new(_settingsService);

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            // Settings were saved
        }
    }

    private void ShowAbout()
    {
        using AboutDialog dialog = new();
        dialog.ShowDialog(this);
    }

    private bool CheckUnsavedChanges()
    {
        if (_definitionEditor?.IsDirty == true)
        {
            DialogResult result = MessageBox.Show(
                this,
                Resources.MsgUnsavedChanges,
                Resources.TitleConfirm,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveDefinition();
                return true;
            }
            else if (result == DialogResult.No)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    // Event Handlers

    private void DefinitionEditor_DefinitionChanged(object? sender, EventArgs e)
    {
        UpdateUIState();
    }

    private void RecordingService_RecordingStarted(object? sender, TimeStudyRecording recording)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_RecordingStarted(sender, recording));
            return;
        }

        UpdateUIState();
    }

    private void RecordingService_RecordingStopped(object? sender, TimeStudyRecording recording)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_RecordingStopped(sender, recording));
            return;
        }

        _lastCompletedRecording = recording;
        ShowView(ViewMode.Results);
        UpdateUIState();
    }

    private void RecordingControl_RecordingCompleted(object? sender, TimeStudyRecording recording)
    {
        _lastCompletedRecording = recording;
        ShowView(ViewMode.Results);
    }

    // Menu Event Handlers

    private void MnuFileNew_Click(object? sender, EventArgs e)
    {
        NewDefinition();
    }

    private void MnuFileOpen_Click(object? sender, EventArgs e)
    {
        OpenDefinition();
    }

    private void MnuFileSave_Click(object? sender, EventArgs e)
    {
        SaveDefinition();
    }

    private void MnuFileSaveAs_Click(object? sender, EventArgs e)
    {
        SaveDefinitionAs();
    }

    private void MnuFileExport_Click(object? sender, EventArgs e)
    {
        ExportCsv();
    }

    private void MnuFileExit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void MnuToolsSettings_Click(object? sender, EventArgs e)
    {
        ShowSettings();
    }

    private void MnuHelpAbout_Click(object? sender, EventArgs e)
    {
        ShowAbout();
    }

    // Toolbar Event Handlers

    private void TsbNew_Click(object? sender, EventArgs e)
    {
        NewDefinition();
    }

    private void TsbOpen_Click(object? sender, EventArgs e)
    {
        OpenDefinition();
    }

    private void TsbSave_Click(object? sender, EventArgs e)
    {
        SaveDefinition();
    }

    private void TsbStartStop_Click(object? sender, EventArgs e)
    {
        ToggleRecording();
    }

    private void TsbExport_Click(object? sender, EventArgs e)
    {
        ExportCsv();
    }

    // Form Events

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        UpdateUIState();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Check for recording in progress
        if (_recordingService.IsRecording)
        {
            DialogResult result = MessageBox.Show(
                this,
                Resources.MsgRecordingInProgress + "\n\n" + Resources.MsgConfirmStop,
                Resources.TitleWarning,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            _recordingService.StopRecording();
        }

        // Check for unsaved changes
        if (!CheckUnsavedChanges())
        {
            e.Cancel = true;
            return;
        }

        // Save window position
        _settingsService.UpdateWindowBounds(this);
        _settingsService.SaveSettings();

        base.OnFormClosing(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        // Update window bounds if normal state
        if (WindowState == FormWindowState.Normal)
        {
            _settingsService.UpdateWindowBounds(this);
        }
    }

    protected override void OnMove(EventArgs e)
    {
        base.OnMove(e);

        // Update window bounds if normal state
        if (WindowState == FormWindowState.Normal)
        {
            _settingsService.UpdateWindowBounds(this);
        }
    }
}
