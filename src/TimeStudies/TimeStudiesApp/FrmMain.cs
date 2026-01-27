using TimeStudiesApp.Controls;
using TimeStudiesApp.Dialogs;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

public partial class FrmMain : Form
{
    // Services
    private readonly SettingsService _settingsService;
    private readonly DefinitionService _definitionService;
    private readonly RecordingService _recordingService;
    private readonly CsvExportService _csvExportService;

    // Controls
    private DefinitionEditorControl? _editorControl;
    private RecordingControl? _recordingControl;
    private ResultsControl? _resultsControl;

    // State
    private TimeStudyDefinition? _currentDefinition;
    private string? _currentDefinitionPath;
    private TimeStudyRecording? _lastRecording;

    private enum ViewMode { Editor, Recording, Results }
    private ViewMode _currentViewMode = ViewMode.Editor;

    public FrmMain()
    {
        // Initialize services first
        _settingsService = new SettingsService();
        _settingsService.ApplyLanguage();

        _definitionService = new DefinitionService(_settingsService);
        _recordingService = new RecordingService(_settingsService);
        _csvExportService = new CsvExportService(_settingsService);

        InitializeComponent();
        CreateControls();
        ApplyLocalization();
        RestoreWindowPosition();
        UpdateUI();
    }

    private void CreateControls()
    {
        // Editor control
        _editorControl = new DefinitionEditorControl
        {
            Dock = DockStyle.Fill,
            Visible = true
        };
        _editorControl.DefinitionChanged += (s, e) => UpdateUI();
        _panelContent.Controls.Add(_editorControl);

        // Recording control
        _recordingControl = new RecordingControl(_recordingService, _settingsService)
        {
            Dock = DockStyle.Fill,
            Visible = false
        };
        _recordingControl.RecordingStateChanged += (s, e) => UpdateUI();
        _recordingControl.RecordingCompleted += RecordingControl_RecordingCompleted;
        _panelContent.Controls.Add(_recordingControl);

        // Results control
        _resultsControl = new ResultsControl(_recordingService, _definitionService)
        {
            Dock = DockStyle.Fill,
            Visible = false
        };
        _panelContent.Controls.Add(_resultsControl);
    }

    private void ApplyLocalization()
    {
        Text = Resources.AppTitle;

        // Menu
        _menuFile.Text = Resources.MenuFile;
        _menuFileNew.Text = Resources.MenuFileNew;
        _menuFileOpen.Text = Resources.MenuFileOpen;
        _menuFileSave.Text = Resources.MenuFileSave;
        _menuFileSaveAs.Text = Resources.MenuFileSaveAs;
        _menuFileExport.Text = Resources.MenuFileExportCsv;
        _menuFileExit.Text = Resources.MenuFileExit;

        _menuRecording.Text = Resources.MenuRecording;
        _menuRecordingStart.Text = Resources.MenuRecordingStart;
        _menuRecordingStop.Text = Resources.MenuRecordingStop;
        _menuRecordingPause.Text = Resources.MenuRecordingPause;

        _menuTools.Text = Resources.MenuTools;
        _menuToolsSettings.Text = Resources.MenuToolsSettings;

        _menuHelp.Text = Resources.MenuHelp;
        _menuHelpAbout.Text = Resources.MenuHelpAbout;

        // Toolbar tooltips
        _tsbNew.ToolTipText = Resources.ToolTipNew;
        _tsbOpen.ToolTipText = Resources.ToolTipOpen;
        _tsbSave.ToolTipText = Resources.ToolTipSave;
        _tsbExport.ToolTipText = Resources.ToolTipExport;
        _tsbStartRecording.ToolTipText = Resources.ToolTipStartRecording;
        _tsbStopRecording.ToolTipText = Resources.ToolTipStopRecording;

        // Status bar
        _statusLabel.Text = Resources.LblReady;
    }

    private void RestoreWindowPosition()
    {
        _settingsService.RestoreMainFormPosition(this);
    }

    private void SaveWindowPosition()
    {
        _settingsService.UpdateMainFormPosition(this);
        _settingsService.Save();
    }

    private void SwitchView(ViewMode mode)
    {
        _currentViewMode = mode;

        _editorControl!.Visible = mode == ViewMode.Editor;
        _recordingControl!.Visible = mode == ViewMode.Recording;
        _resultsControl!.Visible = mode == ViewMode.Results;

        // Update tab strip if we have one
        _tabEditor.Checked = mode == ViewMode.Editor;
        _tabRecording.Checked = mode == ViewMode.Recording;
        _tabResults.Checked = mode == ViewMode.Results;

        UpdateUI();
    }

    private void UpdateUI()
    {
        var hasDefinition = _currentDefinition != null;
        var isRecording = _recordingControl?.IsRecording ?? false;
        var hasRecording = _lastRecording != null;
        var isLocked = _currentDefinition?.IsLocked ?? false;

        // Menu items
        _menuFileNew.Enabled = !isRecording;
        _menuFileOpen.Enabled = !isRecording;
        _menuFileSave.Enabled = hasDefinition && !isRecording && !isLocked && _editorControl!.IsDirty;
        _menuFileSaveAs.Enabled = hasDefinition && !isRecording;
        _menuFileExport.Enabled = hasRecording && !isRecording;

        _menuRecordingStart.Enabled = hasDefinition && !isRecording && _currentViewMode == ViewMode.Recording;
        _menuRecordingStop.Enabled = isRecording;
        _menuRecordingPause.Enabled = isRecording;

        // Toolbar
        _tsbNew.Enabled = !isRecording;
        _tsbOpen.Enabled = !isRecording;
        _tsbSave.Enabled = hasDefinition && !isRecording && !isLocked && _editorControl!.IsDirty;
        _tsbExport.Enabled = hasRecording && !isRecording;
        _tsbStartRecording.Enabled = hasDefinition && !isRecording && _currentViewMode == ViewMode.Recording;
        _tsbStopRecording.Enabled = isRecording;

        // View tabs
        _tabEditor.Enabled = !isRecording;
        _tabRecording.Enabled = hasDefinition;
        _tabResults.Enabled = hasRecording;

        // Status
        if (isRecording)
        {
            _statusLabel.Text = Resources.LblRecording;
            _statusLabel.ForeColor = Color.Red;
        }
        else
        {
            _statusLabel.Text = Resources.LblReady;
            _statusLabel.ForeColor = SystemColors.ControlText;
        }

        // Title
        var title = Resources.AppTitle;
        if (_currentDefinition != null)
        {
            title += $" - {_currentDefinition.Name}";
            if (_editorControl!.IsDirty)
                title += " *";
            if (isLocked)
                title += $" ({Resources.LblDefaultStep})";
        }
        Text = title;
    }

    #region Menu Handlers

    private void MenuFileNew_Click(object? sender, EventArgs e)
    {
        if (!ConfirmUnsavedChanges()) return;

        _currentDefinition = _definitionService.CreateNew();
        _currentDefinitionPath = null;
        _editorControl!.Definition = _currentDefinition;
        _lastRecording = null;
        _resultsControl!.Clear();

        SwitchView(ViewMode.Editor);
    }

    private void MenuFileOpen_Click(object? sender, EventArgs e)
    {
        if (!ConfirmUnsavedChanges()) return;

        using var dialog = new OpenFileDialog
        {
            Title = Resources.TitleOpenDefinition,
            Filter = Resources.FilterJsonFiles,
            InitialDirectory = _settingsService.CurrentSettings.DefinitionsDirectory
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var definition = _definitionService.Load(dialog.FileName);
                if (definition != null)
                {
                    _currentDefinition = definition;
                    _currentDefinitionPath = dialog.FileName;
                    _editorControl!.Definition = _currentDefinition;
                    _lastRecording = null;
                    _resultsControl!.Clear();

                    SwitchView(ViewMode.Editor);
                }
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

    private void MenuFileSave_Click(object? sender, EventArgs e)
    {
        SaveDefinition(false);
    }

    private void MenuFileSaveAs_Click(object? sender, EventArgs e)
    {
        SaveDefinition(true);
    }

    private void SaveDefinition(bool saveAs)
    {
        if (_currentDefinition == null) return;

        // Validate
        var errors = _editorControl!.Validate();
        if (errors.Count > 0)
        {
            var message = Resources.MsgValidationErrors + "\n\n" + string.Join("\n", errors);
            MessageBox.Show(message, Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _editorControl.SaveToDefinition();

        if (saveAs || string.IsNullOrEmpty(_currentDefinitionPath))
        {
            using var dialog = new SaveFileDialog
            {
                Title = Resources.TitleSaveDefinition,
                Filter = Resources.FilterJsonFiles,
                InitialDirectory = _settingsService.CurrentSettings.DefinitionsDirectory,
                FileName = $"{_currentDefinition.Name}.json"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            _currentDefinitionPath = dialog.FileName;
        }

        try
        {
            _definitionService.Save(_currentDefinition, _currentDefinitionPath);
            _editorControl.Definition = _currentDefinition; // Reset dirty flag
            UpdateUI();

            _statusLabel.Text = Resources.MsgSaveSuccess;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void MenuFileExport_Click(object? sender, EventArgs e)
    {
        if (_lastRecording == null || _currentDefinition == null)
        {
            MessageBox.Show(Resources.MsgNoRecordingToExport, Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = Resources.TitleExportCsv,
            Filter = Resources.FilterCsvFiles,
            InitialDirectory = _settingsService.CurrentSettings.RecordingsDirectory,
            FileName = _csvExportService.GetSuggestedFileName(_lastRecording)
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _csvExportService.ExportToFile(_lastRecording, _currentDefinition, dialog.FileName);
                MessageBox.Show(Resources.MsgExportSuccess, Resources.TitleInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void MenuFileExit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void MenuRecordingStart_Click(object? sender, EventArgs e)
    {
        _recordingControl?.StartRecording();
    }

    private void MenuRecordingStop_Click(object? sender, EventArgs e)
    {
        _recordingControl?.StopRecording();
    }

    private void MenuRecordingPause_Click(object? sender, EventArgs e)
    {
        // Trigger pause button click
        if (_currentDefinition != null && _recordingControl != null)
        {
            var defaultStep = _currentDefinition.GetDefaultProcessStep();
            // The recording control will handle this via its pause button
        }
    }

    private void MenuToolsSettings_Click(object? sender, EventArgs e)
    {
        using var dialog = new SettingsDialog(_settingsService);
        dialog.ShowDialog(this);

        // Refresh button sizes if changed
        if (_currentDefinition != null && _recordingControl != null)
        {
            _recordingControl.Definition = _currentDefinition;
        }
    }

    private void MenuHelpAbout_Click(object? sender, EventArgs e)
    {
        using var dialog = new AboutDialog();
        dialog.ShowDialog(this);
    }

    #endregion

    #region View Tab Handlers

    private void TabEditor_Click(object? sender, EventArgs e)
    {
        if (_recordingControl?.IsRecording == true) return;
        SwitchView(ViewMode.Editor);
    }

    private void TabRecording_Click(object? sender, EventArgs e)
    {
        if (_currentDefinition == null) return;

        // Sync definition to recording control
        if (_editorControl!.IsDirty)
        {
            _editorControl.SaveToDefinition();
        }
        _recordingControl!.Definition = _currentDefinition;

        SwitchView(ViewMode.Recording);
    }

    private void TabResults_Click(object? sender, EventArgs e)
    {
        if (_lastRecording == null || _currentDefinition == null) return;
        SwitchView(ViewMode.Results);
    }

    #endregion

    #region Event Handlers

    private void RecordingControl_RecordingCompleted(object? sender, TimeStudyRecording recording)
    {
        _lastRecording = recording;

        // Lock the definition if it isn't already
        if (_currentDefinition != null && !_currentDefinition.IsLocked)
        {
            _definitionService.Lock(_currentDefinition, _currentDefinitionPath);
            _editorControl!.Definition = _currentDefinition;
        }

        // Show results
        _resultsControl!.ShowRecording(recording, _currentDefinition!);
        SwitchView(ViewMode.Results);
    }

    private bool ConfirmUnsavedChanges()
    {
        if (_editorControl?.IsDirty == true)
        {
            var result = MessageBox.Show(
                Resources.MsgUnsavedChanges,
                Resources.TitleConfirm,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
            {
                SaveDefinition(false);
            }
        }

        return true;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_recordingControl?.IsRecording == true)
        {
            var result = MessageBox.Show(
                Resources.MsgRecordingInProgress,
                Resources.TitleWarning,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        if (!ConfirmUnsavedChanges())
        {
            e.Cancel = true;
            return;
        }

        SaveWindowPosition();
        base.OnFormClosing(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (WindowState == FormWindowState.Normal)
        {
            // Track size changes for saving
        }
    }

    protected override void OnMove(EventArgs e)
    {
        base.OnMove(e);
        if (WindowState == FormWindowState.Normal)
        {
            // Track position changes for saving
        }
    }

    #endregion
}
