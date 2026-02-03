using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TimeStudiesApp.Models;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.ViewModels;

/// <summary>
/// Main ViewModel for the application.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly SettingsService _settingsService;
    private readonly DefinitionService _definitionService;
    private readonly RecordingService _recordingService;
    private readonly CsvExportService _csvExportService;

    [ObservableProperty]
    private ApplicationSettings _settings;

    [ObservableProperty]
    private TimeStudyDefinition? _currentDefinition;

    [ObservableProperty]
    private TimeStudyRecording? _currentRecording;

    [ObservableProperty]
    private TimeStudyRecording? _lastCompletedRecording;

    [ObservableProperty]
    private string _currentView = "Definition";

    [ObservableProperty]
    private bool _isRecording;

    [ObservableProperty]
    private bool _hasUnsavedChanges;

    [ObservableProperty]
    private string? _currentFilePath;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public MainViewModel(
        SettingsService settingsService,
        DefinitionService definitionService,
        RecordingService recordingService,
        CsvExportService csvExportService)
    {
        _settingsService = settingsService;
        _definitionService = definitionService;
        _recordingService = recordingService;
        _csvExportService = csvExportService;

        _settings = settingsService.Load();
        _settings.EnsureDirectoriesExist();
    }

    /// <summary>
    /// Creates a new definition.
    /// </summary>
    [RelayCommand]
    public void NewDefinition()
    {
        CurrentDefinition = _definitionService.CreateNew();
        CurrentFilePath = null;
        HasUnsavedChanges = true;
        CurrentView = "Definition";
        StatusMessage = "New definition created.";
    }

    /// <summary>
    /// Opens a definition from a file.
    /// </summary>
    [RelayCommand]
    public void OpenDefinition(string filePath)
    {
        var definition = _definitionService.Load(filePath);
        if (definition != null)
        {
            // Check if recordings exist and update lock status
            if (_definitionService.HasRecordings(definition.Id, Settings.RecordingsDirectory))
            {
                definition.IsLocked = true;
            }

            CurrentDefinition = definition;
            CurrentFilePath = filePath;
            HasUnsavedChanges = false;
            CurrentView = "Definition";
            StatusMessage = $"Opened: {definition.Name}";
        }
    }

    /// <summary>
    /// Saves the current definition.
    /// </summary>
    public bool SaveDefinition()
    {
        if (CurrentDefinition == null)
            return false;

        var (isValid, errorMessage) = _definitionService.Validate(CurrentDefinition);
        if (!isValid)
        {
            StatusMessage = errorMessage ?? "Validation failed.";
            return false;
        }

        string filePath = CurrentFilePath
            ?? _definitionService.GetDefaultFilePath(CurrentDefinition, Settings.DefinitionsDirectory);

        _definitionService.Save(CurrentDefinition, filePath);
        CurrentFilePath = filePath;
        HasUnsavedChanges = false;
        StatusMessage = "Definition saved.";
        return true;
    }

    /// <summary>
    /// Saves the current definition to a new file.
    /// </summary>
    public bool SaveDefinitionAs(string filePath)
    {
        if (CurrentDefinition == null)
            return false;

        var (isValid, errorMessage) = _definitionService.Validate(CurrentDefinition);
        if (!isValid)
        {
            StatusMessage = errorMessage ?? "Validation failed.";
            return false;
        }

        _definitionService.Save(CurrentDefinition, filePath);
        CurrentFilePath = filePath;
        HasUnsavedChanges = false;
        StatusMessage = "Definition saved.";
        return true;
    }

    /// <summary>
    /// Creates a copy of the current definition.
    /// </summary>
    [RelayCommand]
    public void CreateCopy()
    {
        if (CurrentDefinition == null)
            return;

        CurrentDefinition = _definitionService.CreateCopy(CurrentDefinition);
        CurrentFilePath = null;
        HasUnsavedChanges = true;
        StatusMessage = "Copy created. Save to keep changes.";
    }

    /// <summary>
    /// Starts a recording session.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanStartRecording))]
    public void StartRecording()
    {
        if (CurrentDefinition == null)
            return;

        // Lock the definition
        if (!CurrentDefinition.IsLocked)
        {
            CurrentDefinition.IsLocked = true;
            if (CurrentFilePath != null)
            {
                _definitionService.Save(CurrentDefinition, CurrentFilePath);
            }
        }

        CurrentRecording = _recordingService.StartRecording(CurrentDefinition);
        IsRecording = true;
        CurrentView = "Recording";
        StatusMessage = "Recording started.";
    }

    public bool CanStartRecording()
    {
        return CurrentDefinition != null && !IsRecording;
    }

    /// <summary>
    /// Stops the current recording.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanStopRecording))]
    public void StopRecording()
    {
        if (!IsRecording)
            return;

        LastCompletedRecording = _recordingService.StopRecording();
        CurrentRecording = null;
        IsRecording = false;

        // Auto-save if enabled
        if (Settings.AutoSaveRecordings && LastCompletedRecording != null)
        {
            _recordingService.Save(LastCompletedRecording, Settings.RecordingsDirectory);
        }

        CurrentView = "Results";
        StatusMessage = "Recording stopped.";
    }

    public bool CanStopRecording()
    {
        return IsRecording;
    }

    /// <summary>
    /// Records a step during active recording.
    /// </summary>
    [RelayCommand]
    public void RecordStep(int orderNumber)
    {
        if (!IsRecording)
            return;

        _recordingService.RecordStep(orderNumber);
    }

    /// <summary>
    /// Pauses recording (switches to default step).
    /// </summary>
    [RelayCommand]
    public void PauseRecording()
    {
        if (!IsRecording)
            return;

        _recordingService.Pause();
    }

    /// <summary>
    /// Exports the last completed recording to CSV.
    /// </summary>
    public bool ExportCsv(string filePath)
    {
        if (LastCompletedRecording == null)
        {
            StatusMessage = "No recording to export.";
            return false;
        }

        _csvExportService.Export(LastCompletedRecording, filePath);
        StatusMessage = "Export completed.";
        return true;
    }

    /// <summary>
    /// Saves application settings.
    /// </summary>
    [RelayCommand]
    public void SaveSettings()
    {
        _settingsService.Save(Settings);
        Settings.EnsureDirectoriesExist();
        StatusMessage = "Settings saved.";
    }

    /// <summary>
    /// Switches the current view.
    /// </summary>
    [RelayCommand]
    public void SwitchView(string viewName)
    {
        CurrentView = viewName;
    }

    /// <summary>
    /// Gets the elapsed time from the recording service.
    /// </summary>
    public TimeSpan GetElapsedTime()
    {
        return _recordingService.GetElapsedTime();
    }

    /// <summary>
    /// Gets the current step order number from the recording service.
    /// </summary>
    public int GetCurrentStepOrderNumber()
    {
        return _recordingService.CurrentStepOrderNumber;
    }

    /// <summary>
    /// Marks the definition as having unsaved changes.
    /// </summary>
    public void MarkAsModified()
    {
        HasUnsavedChanges = true;
    }
}
