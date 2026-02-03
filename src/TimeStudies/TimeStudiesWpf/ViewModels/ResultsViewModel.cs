using System.Collections.ObjectModel;
using System.Windows.Input;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.ViewModels;

/// <summary>
/// Summary statistics for a process step.
/// </summary>
public class StepSummary
{
    public int OrderNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Count { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public TimeSpan AverageDuration { get; set; }
    public double TotalSeconds => TotalDuration.TotalSeconds;
    public double TotalMinutes => TotalDuration.TotalMinutes;
    public double AverageSeconds => AverageDuration.TotalSeconds;
}

/// <summary>
/// ViewModel for the Results view, displaying recording details and summaries.
/// </summary>
public class ResultsViewModel : ViewModelBase
{
    private readonly Guid _definitionId;
    private TimeStudyRecording? _selectedRecording;

    public event Action<TimeStudyRecording>? RequestExport;

    public ResultsViewModel(Guid definitionId, TimeStudyRecording? initialRecording = null)
    {
        _definitionId = definitionId;

        Recordings = new ObservableCollection<TimeStudyRecording>();
        Entries = new ObservableCollection<TimeEntry>();
        Summary = new ObservableCollection<StepSummary>();

        // Commands
        ExportCommand = new RelayCommand(ExecuteExport, () => SelectedRecording != null);
        RefreshCommand = new RelayCommand(ExecuteRefresh);

        // Load recordings
        _ = LoadRecordingsAsync(initialRecording);
    }

    #region Properties

    public ObservableCollection<TimeStudyRecording> Recordings { get; }

    public TimeStudyRecording? SelectedRecording
    {
        get => _selectedRecording;
        set
        {
            if (SetProperty(ref _selectedRecording, value))
            {
                UpdateDetailView();
            }
        }
    }

    public ObservableCollection<TimeEntry> Entries { get; }

    public ObservableCollection<StepSummary> Summary { get; }

    public string RecordingSummaryText => _selectedRecording != null
        ? $"Recorded on {_selectedRecording.StartedAt:g} - {_selectedRecording.Entries.Count} entries, {_selectedRecording.TotalDuration:hh\\:mm\\:ss} total"
        : "Select a recording to view details";

    #endregion

    #region Commands

    public ICommand ExportCommand { get; }
    public ICommand RefreshCommand { get; }

    #endregion

    #region Command Implementations

    private void ExecuteExport()
    {
        if (SelectedRecording != null)
        {
            RequestExport?.Invoke(SelectedRecording);
        }
    }

    private async void ExecuteRefresh()
    {
        await LoadRecordingsAsync(null);
    }

    #endregion

    #region Private Methods

    private async Task LoadRecordingsAsync(TimeStudyRecording? selectRecording)
    {
        var recordings = await App.RecordingService.GetForDefinitionAsync(_definitionId);

        Recordings.Clear();
        foreach (var recording in recordings.Where(r => r.IsCompleted))
        {
            Recordings.Add(recording);
        }

        // Select the initial recording or the first one
        if (selectRecording != null && Recordings.Any(r => r.Id == selectRecording.Id))
        {
            SelectedRecording = Recordings.First(r => r.Id == selectRecording.Id);
        }
        else if (Recordings.Count > 0)
        {
            SelectedRecording = Recordings[0];
        }
    }

    private void UpdateDetailView()
    {
        Entries.Clear();
        Summary.Clear();

        if (_selectedRecording == null)
        {
            OnPropertyChanged(nameof(RecordingSummaryText));
            return;
        }

        // Populate entries
        foreach (var entry in _selectedRecording.Entries)
        {
            Entries.Add(entry);
        }

        // Calculate summary
        var grouped = _selectedRecording.Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .Select(g => new StepSummary
            {
                OrderNumber = g.Key,
                Description = g.First().ProcessStepDescription,
                Count = g.Count(),
                TotalDuration = TimeSpan.FromTicks(g.Sum(e => e.Duration.Ticks)),
                AverageDuration = TimeSpan.FromTicks((long)g.Average(e => e.Duration.Ticks))
            })
            .OrderBy(s => s.OrderNumber);

        foreach (var summary in grouped)
        {
            Summary.Add(summary);
        }

        OnPropertyChanged(nameof(RecordingSummaryText));
    }

    #endregion
}
