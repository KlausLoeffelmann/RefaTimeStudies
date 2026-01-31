using System.ComponentModel;
using System.Globalization;
using TimeStudiesApp.Controls;
using TimeStudiesApp.Dialogs;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

/// <summary>
///  Main application form for the REFA Time Study application.
/// </summary>
public partial class MainForm : Form
{
    private readonly SettingsService _settingsService;
    private readonly DefinitionService _definitionService;
    private readonly RecordingService _recordingService;
    private readonly CsvExportService _csvExportService;

    private TimeStudyDefinition? _currentDefinition;
    private TimeStudyRecording? _currentRecording;
    private string? _currentDefinitionPath;
    private bool _hasUnsavedChanges;

    private DefinitionEditorControl? _definitionEditor;
    private RecordingControl? _recordingControl;
    private ResultsControl? _resultsControl;

    /// <summary>
    ///  Initializes a new instance of the <see cref="MainForm"/> class.
    /// </summary>
    public MainForm(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);

        _settingsService = settingsService;
        _definitionService = new DefinitionService(settingsService);
        _recordingService = new RecordingService(settingsService);
        _csvExportService = new CsvExportService(settingsService);

        InitializeComponent();
        ApplyLocalization();
        UpdateUIState();
    }

    /// <summary>
    ///  Applies localization to all UI elements.
    /// </summary>
    private void ApplyLocalization()
    {
        Text = Resources.AppTitle;

        // Menu - File
        _menuFile.Text = Resources.MenuFile;
        _menuFileNew.Text = Resources.MenuFileNew;
        _menuFileOpen.Text = Resources.MenuFileOpen;
        _menuFileSave.Text = Resources.MenuFileSave;
        _menuFileSaveAs.Text = Resources.MenuFileSaveAs;
        _menuFileExportCsv.Text = Resources.MenuFileExportCsv;
        _menuFileExit.Text = Resources.MenuFileExit;

        // Menu - Tools
        _menuTools.Text = Resources.MenuTools;
        _menuToolsSettings.Text = Resources.MenuToolsSettings;

        // Menu - Help
        _menuHelp.Text = Resources.MenuHelp;
        _menuHelpAbout.Text = Resources.MenuHelpAbout;

        // Toolbar
        _toolBtnNew.Text = Resources.MenuFileNew;
        _toolBtnNew.ToolTipText = Resources.MenuFileNew;
        _toolBtnOpen.Text = Resources.MenuFileOpen;
        _toolBtnOpen.ToolTipText = Resources.MenuFileOpen;
        _toolBtnSave.Text = Resources.MenuFileSave;
        _toolBtnSave.ToolTipText = Resources.MenuFileSave;
        _toolBtnStartStop.Text = Resources.BtnStartRecording;
        _toolBtnStartStop.ToolTipText = Resources.BtnStartRecording;
        _toolBtnExport.Text = Resources.MenuFileExportCsv;
        _toolBtnExport.ToolTipText = Resources.MenuFileExportCsv;

        // Status bar
        _statusLabel.Text = Resources.StatusReady;
    }

    /// <summary>
    ///  Updates the enabled/disabled state of UI elements based on current state.
    /// </summary>
    private void UpdateUIState()
    {
        bool hasDefinition = _currentDefinition is not null;
        bool isRecording = _recordingControl?.IsRecording ?? false;
        bool hasRecording = _currentRecording is not null;

        // File menu
        _menuFileSave.Enabled = hasDefinition && !isRecording;
        _menuFileSaveAs.Enabled = hasDefinition && !isRecording;
        _menuFileExportCsv.Enabled = hasRecording && !isRecording;

        // Toolbar
        _toolBtnSave.Enabled = hasDefinition && !isRecording;
        _toolBtnStartStop.Enabled = hasDefinition;
        _toolBtnStartStop.Text = isRecording ? Resources.BtnStopRecording : Resources.BtnStartRecording;
        _toolBtnExport.Enabled = hasRecording && !isRecording;

        // Update title
        string title = Resources.AppTitle;
        if (_currentDefinition is not null)
        {
            title = $"{_currentDefinition.Name} - {title}";
            if (_hasUnsavedChanges)
            {
                title = $"* {title}";
            }
        }
        Text = title;
    }

    /// <summary>
    ///  Shows the definition editor view.
    /// </summary>
    private void ShowDefinitionEditor()
    {
        ClearContentPanel();

        _definitionEditor = new DefinitionEditorControl();
        _definitionEditor.Dock = DockStyle.Fill;
        _definitionEditor.DefinitionChanged += OnDefinitionChanged;

        if (_currentDefinition is not null)
        {
            _definitionEditor.LoadDefinition(_currentDefinition);
        }

        _contentPanel.Controls.Add(_definitionEditor);
        _statusLabel.Text = Resources.ViewDefinitionEditor;
    }

    /// <summary>
    ///  Shows the recording view.
    /// </summary>
    private void ShowRecordingView()
    {
        if (_currentDefinition is null)
        {
            return;
        }

        ClearContentPanel();

        _recordingControl = new RecordingControl(_settingsService);
        _recordingControl.Dock = DockStyle.Fill;
        _recordingControl.RecordingStarted += OnRecordingStarted;
        _recordingControl.RecordingStopped += OnRecordingStopped;
        _recordingControl.LoadDefinition(_currentDefinition);

        _contentPanel.Controls.Add(_recordingControl);
        _statusLabel.Text = Resources.ViewRecording;
        UpdateUIState();
    }

    /// <summary>
    ///  Shows the results view for a completed recording.
    /// </summary>
    private void ShowResultsView()
    {
        if (_currentRecording is null)
        {
            return;
        }

        ClearContentPanel();

        _resultsControl = new ResultsControl();
        _resultsControl.Dock = DockStyle.Fill;
        _resultsControl.LoadRecording(_currentRecording);

        _contentPanel.Controls.Add(_resultsControl);
        _statusLabel.Text = Resources.ViewResults;
    }

    /// <summary>
    ///  Clears the content panel and disposes any child controls.
    /// </summary>
    private void ClearContentPanel()
    {
        foreach (Control control in _contentPanel.Controls)
        {
            control.Dispose();
        }
        _contentPanel.Controls.Clear();

        _definitionEditor = null;
        _recordingControl = null;
        _resultsControl = null;
    }

    /// <summary>
    ///  Handles the definition changed event from the editor.
    /// </summary>
    private void OnDefinitionChanged(object? sender, EventArgs e)
    {
        _hasUnsavedChanges = true;
        UpdateUIState();
    }

    /// <summary>
    ///  Handles the recording started event.
    /// </summary>
    private void OnRecordingStarted(object? sender, EventArgs e)
    {
        _statusLabel.Text = Resources.StatusRecording;
        UpdateUIState();
    }

    /// <summary>
    ///  Handles the recording stopped event.
    /// </summary>
    private void OnRecordingStopped(object? sender, TimeStudyRecording recording)
    {
        _currentRecording = recording;
        _recordingService.CompleteRecording(recording);

        if (_settingsService.Settings.AutoSaveRecordings)
        {
            _recordingService.Save(recording);
        }

        // Check if definition should be locked now
        if (_currentDefinition is not null)
        {
            _definitionService.CheckIfLocked(_currentDefinition, _recordingService);
        }

        _statusLabel.Text = Resources.StatusCompleted;
        UpdateUIState();

        // Ask if user wants to view results
        var result = MessageBox.Show(
            Resources.MsgExportSuccess,
            Resources.TitleInformation,
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        ShowResultsView();
    }

    #region Menu Event Handlers

    private void MenuFileNew_Click(object? sender, EventArgs e)
    {
        if (!ConfirmUnsavedChanges())
        {
            return;
        }

        _currentDefinition = _definitionService.CreateNew();
        _currentDefinitionPath = null;
        _hasUnsavedChanges = true;

        ShowDefinitionEditor();
        UpdateUIState();
    }

    private void MenuFileOpen_Click(object? sender, EventArgs e)
    {
        if (!ConfirmUnsavedChanges())
        {
            return;
        }

        using var dialog = new OpenFileDialog
        {
            Title = Resources.TitleOpenDefinition,
            Filter = Resources.FilterJsonFiles,
            InitialDirectory = _settingsService.Settings.DefinitionsDirectory
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var definition = _definitionService.Load(dialog.FileName);
            if (definition is not null)
            {
                _currentDefinition = definition;
                _currentDefinitionPath = dialog.FileName;
                _hasUnsavedChanges = false;

                // Check if locked
                _definitionService.CheckIfLocked(definition, _recordingService);

                ShowDefinitionEditor();
                UpdateUIState();
            }
            else
            {
                MessageBox.Show(
                    "Failed to load definition.",
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void MenuFileSave_Click(object? sender, EventArgs e)
    {
        SaveDefinition(false);
    }

    private void MenuFileSaveAs_Click(object? sender, EventArgs e)
    {
        SaveDefinition(true);
    }

    private void MenuFileExportCsv_Click(object? sender, EventArgs e)
    {
        if (_currentRecording is null)
        {
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = Resources.TitleExportCsv,
            Filter = Resources.FilterCsvFiles,
            FileName = $"{_currentRecording.DefinitionName}_{_currentRecording.StartedAt:yyyy-MM-dd}.csv"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _csvExportService.Export(_currentRecording, dialog.FileName);
                MessageBox.Show(
                    Resources.MsgExportSuccess,
                    Resources.TitleInformation,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void MenuFileExit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void MenuToolsSettings_Click(object? sender, EventArgs e)
    {
        using var dialog = new SettingsDialog(_settingsService);
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Settings saved by dialog
            UpdateUIState();
        }
    }

    private void MenuHelpAbout_Click(object? sender, EventArgs e)
    {
        using var dialog = new AboutDialog();
        dialog.ShowDialog();
    }

    #endregion

    #region Toolbar Event Handlers

    private void ToolBtnNew_Click(object? sender, EventArgs e)
    {
        MenuFileNew_Click(sender, e);
    }

    private void ToolBtnOpen_Click(object? sender, EventArgs e)
    {
        MenuFileOpen_Click(sender, e);
    }

    private void ToolBtnSave_Click(object? sender, EventArgs e)
    {
        MenuFileSave_Click(sender, e);
    }

    private void ToolBtnStartStop_Click(object? sender, EventArgs e)
    {
        if (_recordingControl is null)
        {
            // Switch to recording view
            if (_definitionEditor is not null && _currentDefinition is not null)
            {
                // Save current definition from editor
                _currentDefinition = _definitionEditor.GetDefinition();

                var errors = _currentDefinition.Validate();
                if (errors.Count > 0)
                {
                    MessageBox.Show(
                        $"{Resources.MsgValidationErrors}\n\n{string.Join("\n", errors)}",
                        Resources.TitleError,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            ShowRecordingView();
        }
        else if (_recordingControl.IsRecording)
        {
            // Stop recording
            if (_settingsService.Settings.ConfirmBeforeClosingRecording)
            {
                var result = MessageBox.Show(
                    Resources.MsgConfirmStop,
                    Resources.TitleConfirm,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            _recordingControl.StopRecording();
        }
        else
        {
            // Start recording
            _recordingControl.StartRecording();
        }
    }

    private void ToolBtnExport_Click(object? sender, EventArgs e)
    {
        MenuFileExportCsv_Click(sender, e);
    }

    #endregion

    #region Helper Methods

    /// <summary>
    ///  Saves the current definition.
    /// </summary>
    /// <param name="saveAs">If true, always prompts for a new file name.</param>
    private void SaveDefinition(bool saveAs)
    {
        if (_currentDefinition is null)
        {
            return;
        }

        // Get latest from editor
        if (_definitionEditor is not null)
        {
            _currentDefinition = _definitionEditor.GetDefinition();
        }

        // Validate
        var errors = _currentDefinition.Validate();
        if (errors.Count > 0)
        {
            MessageBox.Show(
                $"{Resources.MsgValidationErrors}\n\n{string.Join("\n", errors)}",
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string? filePath = _currentDefinitionPath;

        if (saveAs || string.IsNullOrEmpty(filePath))
        {
            using var dialog = new SaveFileDialog
            {
                Title = Resources.TitleSaveDefinition,
                Filter = Resources.FilterJsonFiles,
                InitialDirectory = _settingsService.Settings.DefinitionsDirectory,
                FileName = $"{_currentDefinition.Name}.json"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            filePath = dialog.FileName;
        }

        try
        {
            _currentDefinitionPath = _definitionService.Save(_currentDefinition, filePath);
            _hasUnsavedChanges = false;
            UpdateUIState();

            MessageBox.Show(
                Resources.MsgSaveSuccess,
                Resources.TitleInformation,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///  Confirms unsaved changes with the user.
    /// </summary>
    /// <returns>True if it's OK to proceed, false to cancel.</returns>
    private bool ConfirmUnsavedChanges()
    {
        if (!_hasUnsavedChanges)
        {
            return true;
        }

        var result = MessageBox.Show(
            Resources.MsgUnsavedChanges,
            Resources.TitleConfirm,
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question);

        if (result == DialogResult.Cancel)
        {
            return false;
        }

        if (result == DialogResult.Yes)
        {
            SaveDefinition(false);
        }

        return true;
    }

    #endregion

    /// <summary>
    ///  Handles the form closing event.
    /// </summary>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_recordingControl?.IsRecording == true)
        {
            var result = MessageBox.Show(
                Resources.MsgConfirmStop,
                Resources.TitleConfirm,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            _recordingControl.StopRecording();
        }

        if (!ConfirmUnsavedChanges())
        {
            e.Cancel = true;
            return;
        }

        base.OnFormClosing(e);
    }
}
