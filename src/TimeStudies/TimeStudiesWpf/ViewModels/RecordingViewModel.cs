using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.ViewModels;

/// <summary>
/// ViewModel for a process step button during recording.
/// </summary>
public class RecordingStepViewModel : ViewModelBase
{
    private bool _isActive;

    public ProcessStepDefinition Step { get; }

    public int OrderNumber => Step.OrderNumber;
    public string Description => Step.Description;
    public bool IsDefaultStep => Step.IsDefaultStep;

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }

    public RecordingStepViewModel(ProcessStepDefinition step)
    {
        Step = step;
    }
}

/// <summary>
/// ViewModel for the Recording view, handling the time study recording session.
/// </summary>
public class RecordingViewModel : ViewModelBase
{
    private readonly TimeStudyDefinition _definition;
    private readonly Stopwatch _stopwatch;
    private readonly DispatcherTimer _uiTimer;
    private TimeStudyRecording? _recording;
    private RecordingStepViewModel? _currentStep;
    private TimeEntry? _currentEntry;
    private bool _isRecording;
    private TimeSpan _progressiveTime;
    private int _buttonSize = 60;

    public event Action? RecordingStarted;
    public event Action? RecordingStopped;
    public event Action<TimeStudyRecording>? RecordingCompleted;

    public RecordingViewModel(TimeStudyDefinition definition)
    {
        _definition = definition;
        _stopwatch = new Stopwatch();

        // Load button size from settings
        _buttonSize = App.SettingsService.Current.ButtonSize;

        // Initialize process step buttons
        ProcessSteps = new ObservableCollection<RecordingStepViewModel>(
            definition.ProcessSteps.OrderBy(ps => ps.OrderNumber)
                .Select(ps => new RecordingStepViewModel(ps)));

        // Find default step
        DefaultStep = ProcessSteps.FirstOrDefault(ps => ps.IsDefaultStep);

        // Initialize entries collection
        Entries = new ObservableCollection<TimeEntry>();

        // Set up UI update timer
        _uiTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _uiTimer.Tick += OnTimerTick;

        // Initialize commands
        StartCommand = new RelayCommand(ExecuteStart, () => !IsRecording);
        StopCommand = new RelayCommand(ExecuteStop, () => IsRecording);
        PauseCommand = new RelayCommand(ExecutePause, () => IsRecording && DefaultStep != null);
        SelectStepCommand = new RelayCommand<RecordingStepViewModel>(ExecuteSelectStep, _ => IsRecording);
    }

    #region Properties

    public string DefinitionName => _definition.Name;

    public ObservableCollection<RecordingStepViewModel> ProcessSteps { get; }

    public ObservableCollection<TimeEntry> Entries { get; }

    public RecordingStepViewModel? DefaultStep { get; }

    public RecordingStepViewModel? CurrentStep
    {
        get => _currentStep;
        private set
        {
            if (_currentStep != null)
                _currentStep.IsActive = false;

            if (SetProperty(ref _currentStep, value))
            {
                if (_currentStep != null)
                    _currentStep.IsActive = true;

                OnPropertyChanged(nameof(CurrentStepDescription));
            }
        }
    }

    public string CurrentStepDescription => _currentStep != null
        ? $"[{_currentStep.OrderNumber}] {_currentStep.Description}"
        : "Not started";

    public bool IsRecording
    {
        get => _isRecording;
        private set => SetProperty(ref _isRecording, value);
    }

    public TimeSpan ProgressiveTime
    {
        get => _progressiveTime;
        private set => SetProperty(ref _progressiveTime, value);
    }

    public int EntryCount => Entries.Count;

    public int ButtonSize
    {
        get => _buttonSize;
        set => SetProperty(ref _buttonSize, value);
    }

    #endregion

    #region Commands

    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand SelectStepCommand { get; }

    #endregion

    #region Command Implementations

    private void ExecuteStart()
    {
        // Create new recording
        _recording = new TimeStudyRecording
        {
            Id = Guid.NewGuid(),
            DefinitionId = _definition.Id,
            DefinitionName = _definition.Name,
            StartedAt = DateTime.Now
        };

        // Start timing
        _stopwatch.Restart();
        _uiTimer.Start();
        IsRecording = true;

        // Time starts flowing to default step
        if (DefaultStep != null)
        {
            SwitchToStep(DefaultStep);
        }

        RecordingStarted?.Invoke();
    }

    public void StopRecording()
    {
        ExecuteStop();
    }

    private async void ExecuteStop()
    {
        if (!IsRecording || _recording == null)
            return;

        // Stop timing
        _uiTimer.Stop();
        _stopwatch.Stop();
        IsRecording = false;

        // Complete the current entry
        if (_currentEntry != null)
        {
            _currentEntry.Duration = _stopwatch.Elapsed - _currentEntry.ElapsedFromStart;
        }

        // Mark recording as complete
        _recording.CompletedAt = DateTime.Now;
        _recording.IsCompleted = true;
        _recording.Entries = Entries.ToList();

        // Save recording
        await App.RecordingService.SaveAsync(_recording);

        CurrentStep = null;
        RecordingStopped?.Invoke();
        RecordingCompleted?.Invoke(_recording);
    }

    private void ExecutePause()
    {
        if (DefaultStep != null)
        {
            SwitchToStep(DefaultStep);
        }
    }

    private void ExecuteSelectStep(RecordingStepViewModel? step)
    {
        if (step != null && IsRecording)
        {
            SwitchToStep(step);
        }
    }

    #endregion

    #region Private Methods

    private void SwitchToStep(RecordingStepViewModel newStep)
    {
        var elapsed = _stopwatch.Elapsed;

        // Complete the previous entry
        if (_currentEntry != null)
        {
            _currentEntry.Duration = elapsed - _currentEntry.ElapsedFromStart;
        }

        // Create new entry
        _currentEntry = new TimeEntry
        {
            SequenceNumber = Entries.Count + 1,
            ProcessStepOrderNumber = newStep.OrderNumber,
            ProcessStepDescription = newStep.Description,
            Timestamp = DateTime.Now,
            ElapsedFromStart = elapsed,
            DimensionValue = newStep.Step.DefaultDimensionValue
        };

        Entries.Add(_currentEntry);
        CurrentStep = newStep;
        OnPropertyChanged(nameof(EntryCount));

        // Auto-save if enabled
        if (App.SettingsService.Current.AutoSaveRecordings && _recording != null)
        {
            _recording.Entries = Entries.ToList();
            _ = App.RecordingService.SaveAsync(_recording);
        }
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        ProgressiveTime = _stopwatch.Elapsed;
    }

    #endregion
}
