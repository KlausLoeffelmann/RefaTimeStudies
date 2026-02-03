using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TimeStudiesApp.Models;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.ViewModels;

/// <summary>
/// ViewModel for the recording view.
/// </summary>
public partial class RecordingViewModel : ObservableObject
{
    private readonly RecordingService _recordingService;

    [ObservableProperty]
    private TimeStudyDefinition? _definition;

    [ObservableProperty]
    private TimeSpan _elapsedTime;

    [ObservableProperty]
    private int _currentStepOrderNumber;

    [ObservableProperty]
    private string _currentStepDescription = string.Empty;

    [ObservableProperty]
    private int _entryCount;

    [ObservableProperty]
    private bool _isActive;

    /// <summary>
    /// Event raised when a step is recorded.
    /// </summary>
    public event EventHandler<TimeEntry>? StepRecorded;

    public RecordingViewModel(RecordingService recordingService)
    {
        _recordingService = recordingService;
        _recordingService.EntryRecorded += OnEntryRecorded;
    }

    private void OnEntryRecorded(object? sender, TimeEntry entry)
    {
        EntryCount = _recordingService.CurrentRecording?.Entries.Count ?? 0;
        UpdateCurrentStep();
        StepRecorded?.Invoke(this, entry);
    }

    /// <summary>
    /// Loads a definition for recording.
    /// </summary>
    public void LoadDefinition(TimeStudyDefinition? definition)
    {
        Definition = definition;
        UpdateCurrentStep();
    }

    /// <summary>
    /// Updates the elapsed time display.
    /// </summary>
    public void UpdateElapsedTime()
    {
        if (_recordingService.IsRecording)
        {
            ElapsedTime = _recordingService.GetElapsedTime();
            IsActive = true;
        }
        else
        {
            IsActive = false;
        }
    }

    /// <summary>
    /// Records a step change.
    /// </summary>
    [RelayCommand]
    public void RecordStep(int orderNumber)
    {
        if (!_recordingService.IsRecording)
            return;

        _recordingService.RecordStep(orderNumber);
        UpdateCurrentStep();
    }

    /// <summary>
    /// Pauses recording (switches to default step).
    /// </summary>
    [RelayCommand]
    public void Pause()
    {
        if (!_recordingService.IsRecording || Definition == null)
            return;

        _recordingService.Pause();
        UpdateCurrentStep();
    }

    private void UpdateCurrentStep()
    {
        if (_recordingService.IsRecording && Definition != null)
        {
            CurrentStepOrderNumber = _recordingService.CurrentStepOrderNumber;
            var step = Definition.GetProcessStep(CurrentStepOrderNumber);
            CurrentStepDescription = step?.Description ?? $"Step {CurrentStepOrderNumber}";
        }
        else
        {
            CurrentStepOrderNumber = 0;
            CurrentStepDescription = string.Empty;
        }
    }

    /// <summary>
    /// Gets the recording service for direct access when needed.
    /// </summary>
    public RecordingService GetRecordingService() => _recordingService;
}
