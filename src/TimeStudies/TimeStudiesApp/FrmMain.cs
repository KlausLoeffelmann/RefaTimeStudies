using TimeStudiesApp.Controls;
using TimeStudiesApp.Dialogs;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;
using TimeStudiesApp.ViewModels;

namespace TimeStudiesApp;

/// <summary>
/// Main application form for REFA Time Study.
/// </summary>
public partial class FrmMain : Form
{
    private readonly MainViewModel _viewModel;
    private readonly SettingsService _settingsService;
    private readonly DefinitionService _definitionService;
    private readonly RecordingService _recordingService;
    private readonly CsvExportService _csvExportService;

    private DefinitionEditorControl? _definitionEditor;
    private RecordingControl? _recordingControl;
    private ResultsControl? _resultsControl;

    private string _currentView = "Definition";

    public FrmMain()
    {
        _settingsService = new SettingsService();
        _definitionService = new DefinitionService();
        _recordingService = new RecordingService();
        _csvExportService = new CsvExportService();

        _viewModel = new MainViewModel(
            _settingsService,
            _definitionService,
            _recordingService,
            _csvExportService);

        InitializeComponent();
        ApplyLocalization();
        SetupEventHandlers();
        SwitchToView("Definition");

        // Start with a new definition
        _viewModel.NewDefinition();
        UpdateUI();
    }

    private void ApplyLocalization()
    {
        try
        {
            Text = Resources.AppTitle;

            // Menu - File
            _tsmFile.Text = Resources.MenuFile;
            _tsmFileNew.Text = Resources.MenuFileNew;
            _tsmFileOpen.Text = Resources.MenuFileOpen;
            _tsmFileSave.Text = Resources.MenuFileSave;
            _tsmFileSaveAs.Text = Resources.MenuFileSaveAs;
            _tsmFileExportCsv.Text = Resources.MenuFileExportCsv;
            _tsmFileExit.Text = Resources.MenuFileExit;

            // Menu - Tools
            _tsmTools.Text = Resources.MenuTools;
            _tsmToolsSettings.Text = Resources.MenuToolsSettings;

            // Menu - Help
            _tsmHelp.Text = Resources.MenuHelp;
            _tsmHelpAbout.Text = Resources.MenuHelpAbout;

            // Toolbar
            _tsbNew.Text = Resources.MenuFileNew.Replace(" Definition", "");
            _tsbOpen.Text = Resources.MenuFileOpen.Replace(" Definition", "").Replace("Definition ", "");
            _tsbSave.Text = Resources.MenuFileSave.Replace(" Definition", "").Replace("Definition ", "");
            _tsbStartStop.Text = Resources.BtnStartRecording;
            _tsbExport.Text = Resources.BtnExport;

            // Status
            _tslView.Text = Resources.ViewDefinition;
        }
        catch
        {
            // Resources not yet generated - use defaults
        }
    }

    private void SetupEventHandlers()
    {
        // Menu events
        _tsmFileNew.Click += OnNewClick;
        _tsmFileOpen.Click += OnOpenClick;
        _tsmFileSave.Click += OnSaveClick;
        _tsmFileSaveAs.Click += OnSaveAsClick;
        _tsmFileExportCsv.Click += OnExportClick;
        _tsmFileExit.Click += OnExitClick;
        _tsmToolsSettings.Click += OnSettingsClick;
        _tsmHelpAbout.Click += OnAboutClick;

        // Toolbar events
        _tsbNew.Click += OnNewClick;
        _tsbOpen.Click += OnOpenClick;
        _tsbSave.Click += OnSaveClick;
        _tsbStartStop.Click += OnStartStopClick;
        _tsbExport.Click += OnExportClick;

        // Form events
        FormClosing += OnFormClosing;

        // ViewModel events
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.StatusMessage))
        {
            _tslStatus.Text = _viewModel.StatusMessage;
        }
        else if (e.PropertyName == nameof(MainViewModel.CurrentView))
        {
            SwitchToView(_viewModel.CurrentView);
        }
        else if (e.PropertyName == nameof(MainViewModel.IsRecording))
        {
            UpdateRecordingUI();
        }
    }

    private void SwitchToView(string viewName)
    {
        _mainPanel.SuspendLayout();
        _mainPanel.Controls.Clear();

        Control? activeControl = null;

        switch (viewName)
        {
            case "Definition":
                if (_definitionEditor == null)
                {
                    _definitionEditor = new DefinitionEditorControl();
                    _definitionEditor.DefinitionModified += OnDefinitionModified;
                    ApplyControlLocalization(_definitionEditor);
                }
                _definitionEditor.LoadDefinition(_viewModel.CurrentDefinition);
                activeControl = _definitionEditor;
                _tslView.Text = GetViewText("Definition");
                break;

            case "Recording":
                if (_recordingControl == null)
                {
                    _recordingControl = new RecordingControl();
                    _recordingControl.RecordingStarted += OnRecordingStarted;
                    _recordingControl.RecordingStopped += OnRecordingStopped;
                    ApplyControlLocalization(_recordingControl);
                }
                _recordingControl.LoadDefinition(_viewModel.CurrentDefinition);
                _recordingControl.SetButtonSize(_viewModel.Settings.ButtonSize);
                activeControl = _recordingControl;
                _tslView.Text = GetViewText("Recording");
                break;

            case "Results":
                if (_resultsControl == null)
                {
                    _resultsControl = new ResultsControl();
                    _resultsControl.ExportRequested += OnResultsExportRequested;
                    ApplyControlLocalization(_resultsControl);
                }
                _resultsControl.LoadRecording(_viewModel.LastCompletedRecording);
                activeControl = _resultsControl;
                _tslView.Text = GetViewText("Results");
                break;
        }

        if (activeControl != null)
        {
            activeControl.Dock = DockStyle.Fill;
            _mainPanel.Controls.Add(activeControl);
        }

        _mainPanel.ResumeLayout(true);
        _currentView = viewName;
        UpdateUI();
    }

    private string GetViewText(string viewName)
    {
        try
        {
            return viewName switch
            {
                "Definition" => Resources.ViewDefinition,
                "Recording" => Resources.ViewRecording,
                "Results" => Resources.ViewResults,
                _ => viewName
            };
        }
        catch
        {
            return viewName;
        }
    }

    private void ApplyControlLocalization(Control control)
    {
        try
        {
            if (control is DefinitionEditorControl def)
            {
                def.ApplyLocalization(
                    Resources.LblDefinitionName,
                    Resources.LblDescription,
                    Resources.BtnAdd,
                    Resources.BtnRemove,
                    Resources.BtnSetDefault,
                    Resources.ColOrderNumber,
                    Resources.ColDescription,
                    Resources.ColProduct,
                    Resources.ColDimensionType,
                    Resources.ColUnit,
                    Resources.ColDefaultValue,
                    Resources.ColIsDefault);
            }
            else if (control is RecordingControl rec)
            {
                rec.ApplyLocalization(
                    Resources.BtnStartRecording,
                    Resources.BtnStopRecording,
                    Resources.BtnPause,
                    Resources.LblProgressiveTime,
                    Resources.LblCurrentStep,
                    Resources.LblEntryCount,
                    Resources.LblNotRecording,
                    Resources.LblRecording);
            }
            else if (control is ResultsControl res)
            {
                res.ApplyLocalization(
                    "Details",
                    "Summary",
                    Resources.BtnExport,
                    "Seq",
                    Resources.ColOrderNumber,
                    Resources.ColDescription,
                    Resources.ColTimestamp,
                    Resources.ColProgressiveTime,
                    Resources.ColDuration,
                    Resources.LblDimension,
                    Resources.ColCount,
                    Resources.ColTotalDuration,
                    Resources.ColAvgDuration);
            }
        }
        catch
        {
            // Resources not yet generated
        }
    }

    private void UpdateUI()
    {
        bool hasDefinition = _viewModel.CurrentDefinition != null;
        bool isRecording = _viewModel.IsRecording;

        _tsmFileSave.Enabled = hasDefinition && !isRecording;
        _tsmFileSaveAs.Enabled = hasDefinition && !isRecording;
        _tsmFileExportCsv.Enabled = _viewModel.LastCompletedRecording != null;

        _tsbSave.Enabled = hasDefinition && !isRecording;
        _tsbStartStop.Enabled = hasDefinition;
        _tsbExport.Enabled = _viewModel.LastCompletedRecording != null;
    }

    private void UpdateRecordingUI()
    {
        bool isRecording = _viewModel.IsRecording;

        try
        {
            if (isRecording)
            {
                _tsbStartStop.Text = Resources.BtnStopRecording;
                _tsbStartStop.BackColor = Color.LightCoral;
            }
            else
            {
                _tsbStartStop.Text = Resources.BtnStartRecording;
                _tsbStartStop.BackColor = Color.LightGreen;
            }
        }
        catch
        {
            _tsbStartStop.Text = isRecording ? "Stop Recording" : "Start Recording";
            _tsbStartStop.BackColor = isRecording ? Color.LightCoral : Color.LightGreen;
        }
    }

    private void OnNewClick(object? sender, EventArgs e)
    {
        if (!ConfirmUnsavedChanges())
            return;

        _viewModel.NewDefinition();
        SwitchToView("Definition");
    }

    private void OnOpenClick(object? sender, EventArgs e)
    {
        if (!ConfirmUnsavedChanges())
            return;

        string filter;
        string title;
        try
        {
            filter = Resources.FilterDefinition;
            title = Resources.TitleOpenDefinition;
        }
        catch
        {
            filter = "Time Study Definition (*.json)|*.json";
            title = "Open Definition";
        }

        using var dialog = new OpenFileDialog
        {
            Title = title,
            Filter = filter,
            InitialDirectory = _viewModel.Settings.DefinitionsDirectory
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _viewModel.OpenDefinition(dialog.FileName);
            SwitchToView("Definition");
        }
    }

    private void OnSaveClick(object? sender, EventArgs e)
    {
        SyncDefinitionFromEditor();

        if (_viewModel.CurrentFilePath == null)
        {
            OnSaveAsClick(sender, e);
            return;
        }

        _viewModel.SaveDefinition();
    }

    private void OnSaveAsClick(object? sender, EventArgs e)
    {
        SyncDefinitionFromEditor();

        string filter;
        string title;
        try
        {
            filter = Resources.FilterDefinition;
            title = Resources.TitleSaveDefinition;
        }
        catch
        {
            filter = "Time Study Definition (*.json)|*.json";
            title = "Save Definition";
        }

        using var dialog = new SaveFileDialog
        {
            Title = title,
            Filter = filter,
            InitialDirectory = _viewModel.Settings.DefinitionsDirectory,
            FileName = _viewModel.CurrentDefinition?.Name ?? "Definition"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _viewModel.SaveDefinitionAs(dialog.FileName);
        }
    }

    private void OnExportClick(object? sender, EventArgs e)
    {
        if (_viewModel.LastCompletedRecording == null)
        {
            try
            {
                MessageBox.Show(Resources.MsgNoRecording, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("No recording available to export.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return;
        }

        string filter;
        string title;
        try
        {
            filter = Resources.FilterCsv;
            title = Resources.TitleExportCsv;
        }
        catch
        {
            filter = "CSV Files (*.csv)|*.csv";
            title = "Export to CSV";
        }

        using var dialog = new SaveFileDialog
        {
            Title = title,
            Filter = filter,
            FileName = $"{_viewModel.LastCompletedRecording.DefinitionName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _viewModel.ExportCsv(dialog.FileName);

            try
            {
                MessageBox.Show(Resources.MsgExportSuccess, Resources.TitleInfo,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Export completed successfully.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private void OnExitClick(object? sender, EventArgs e)
    {
        Close();
    }

    private void OnSettingsClick(object? sender, EventArgs e)
    {
        using var dialog = new SettingsDialog(_viewModel.Settings);

        try
        {
            dialog.ApplyLocalization(
                Resources.SettingsTitle,
                Resources.SettingsLanguage,
                Resources.SettingsBaseDir,
                Resources.SettingsButtonSize,
                Resources.SettingsAutoSave,
                Resources.SettingsConfirmClose,
                Resources.BtnBrowse,
                Resources.BtnOK,
                Resources.BtnCancel);
        }
        catch
        {
            // Resources not yet generated
        }

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _viewModel.Settings.UiLanguage = dialog.Settings.UiLanguage;
            _viewModel.Settings.BaseDirectory = dialog.Settings.BaseDirectory;
            _viewModel.Settings.ButtonSize = dialog.Settings.ButtonSize;
            _viewModel.Settings.AutoSaveRecordings = dialog.Settings.AutoSaveRecordings;
            _viewModel.Settings.ConfirmBeforeClosingRecording = dialog.Settings.ConfirmBeforeClosingRecording;

            _viewModel.SaveSettings();

            if (dialog.LanguageChanged)
            {
                try
                {
                    MessageBox.Show(Resources.MsgRestartRequired, Resources.TitleInfo,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Please restart the application for language changes to take effect.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Update button size if recording control exists
            _recordingControl?.SetButtonSize(_viewModel.Settings.ButtonSize);
        }
    }

    private void OnAboutClick(object? sender, EventArgs e)
    {
        using var dialog = new AboutDialog();

        try
        {
            dialog.ApplyLocalization(
                Resources.TitleAbout,
                Resources.AppTitle,
                Resources.AboutDescription,
                Resources.BtnOK);
        }
        catch
        {
            // Resources not yet generated
        }

        dialog.ShowDialog(this);
    }

    private void OnStartStopClick(object? sender, EventArgs e)
    {
        if (_viewModel.IsRecording)
        {
            // Stop recording
            if (_viewModel.Settings.ConfirmBeforeClosingRecording)
            {
                string message;
                string title;
                try
                {
                    message = Resources.MsgConfirmStop;
                    title = Resources.TitleConfirm;
                }
                catch
                {
                    message = "Stop the current recording?";
                    title = "Confirm";
                }

                var result = MessageBox.Show(message, title,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            _viewModel.StopRecording();
        }
        else
        {
            // Sync definition first
            SyncDefinitionFromEditor();

            // Start recording
            _viewModel.StartRecording();
        }
    }

    private void OnRecordingStarted(object? sender, EventArgs e)
    {
        _viewModel.StartRecording();
        UpdateRecordingUI();
    }

    private void OnRecordingStopped(object? sender, TimeStudyRecording recording)
    {
        _viewModel.StopRecording();
        UpdateRecordingUI();
    }

    private void OnResultsExportRequested(object? sender, EventArgs e)
    {
        OnExportClick(sender, e);
    }

    private void OnDefinitionModified(object? sender, EventArgs e)
    {
        _viewModel.MarkAsModified();
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_viewModel.IsRecording)
        {
            string message;
            string title;
            try
            {
                message = Resources.MsgConfirmStop;
                title = Resources.TitleConfirm;
            }
            catch
            {
                message = "A recording is in progress. Stop and exit?";
                title = "Confirm";
            }

            var result = MessageBox.Show(message, title,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            _viewModel.StopRecording();
        }

        if (!ConfirmUnsavedChanges())
        {
            e.Cancel = true;
        }
    }

    private bool ConfirmUnsavedChanges()
    {
        if (!_viewModel.HasUnsavedChanges)
            return true;

        string message;
        string title;
        try
        {
            message = Resources.MsgUnsavedChanges;
            title = Resources.TitleConfirm;
        }
        catch
        {
            message = "You have unsaved changes. Save before closing?";
            title = "Confirm";
        }

        var result = MessageBox.Show(message, title,
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        switch (result)
        {
            case DialogResult.Yes:
                SyncDefinitionFromEditor();
                return _viewModel.SaveDefinition();
            case DialogResult.No:
                return true;
            default:
                return false;
        }
    }

    private void SyncDefinitionFromEditor()
    {
        if (_definitionEditor != null && _currentView == "Definition")
        {
            var definition = _definitionEditor.GetDefinition();
            if (definition != null)
            {
                _viewModel.CurrentDefinition = definition;
            }
        }
    }
}
