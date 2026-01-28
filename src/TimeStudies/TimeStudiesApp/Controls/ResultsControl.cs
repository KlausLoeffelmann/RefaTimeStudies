using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for displaying completed recording results and summaries.
/// </summary>
public partial class ResultsControl : UserControl
{
    private readonly RecordingService _recordingService;
    private TimeStudyDefinition? _definition;
    private readonly List<TimeStudyRecording> _recordings = [];
    private TimeStudyRecording? _selectedRecording;

    public ResultsControl(RecordingService recordingService)
    {
        ArgumentNullException.ThrowIfNull(recordingService);
        _recordingService = recordingService;

        InitializeComponent();
        ApplyLocalization();
    }

    /// <summary>
    /// Gets or sets the current definition.
    /// </summary>
    public TimeStudyDefinition? Definition
    {
        get => _definition;
        set
        {
            _definition = value;
            _ = LoadRecordingsAsync();
        }
    }

    /// <summary>
    /// Gets the selected recording for export.
    /// </summary>
    public TimeStudyRecording? SelectedRecording => _selectedRecording;

    /// <summary>
    /// Event raised when a recording is selected.
    /// </summary>
    public event EventHandler? RecordingSelected;

    private void ApplyLocalization()
    {
        _grpRecordings.Text = Resources.ViewRecording + "s";
        _grpDetails.Text = Resources.LblEntryCount;
        _grpSummary.Text = Resources.CsvHeaderSummary;

        _colSeq.HeaderText = "#";
        _colOrderNumber.HeaderText = Resources.LblOrderNumber;
        _colDescription.HeaderText = Resources.LblDescription;
        _colTimestamp.HeaderText = Resources.CsvHeaderTimestamp;
        _colElapsed.HeaderText = Resources.LblProgressiveTime;
        _colDuration.HeaderText = Resources.CsvHeaderDurationSec;

        _colSumOrderNumber.HeaderText = Resources.LblOrderNumber;
        _colSumDescription.HeaderText = Resources.LblDescription;
        _colSumCount.HeaderText = Resources.CsvHeaderCount;
        _colSumTotalDuration.HeaderText = Resources.CsvHeaderTotalDurationSec;
        _colSumAvgDuration.HeaderText = Resources.CsvHeaderAvgDurationSec;
    }

    /// <summary>
    /// Loads recordings for the current definition.
    /// </summary>
    public async Task LoadRecordingsAsync()
    {
        _listRecordings.Items.Clear();
        _recordings.Clear();
        _selectedRecording = null;
        ClearGrids();

        if (_definition is null)
        {
            return;
        }

        try
        {
            IReadOnlyList<TimeStudyRecording> recordings =
                await _recordingService.ListByDefinitionAsync(_definition.Id);

            foreach (TimeStudyRecording recording in recordings.Where(r => r.IsCompleted))
            {
                _recordings.Add(recording);
                _listRecordings.Items.Add(new RecordingItem(recording));
            }

            if (_listRecordings.Items.Count > 0)
            {
                _listRecordings.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading recordings: {ex.Message}");
        }
    }

    /// <summary>
    /// Refreshes the list of recordings.
    /// </summary>
    public async Task RefreshAsync()
    {
        await LoadRecordingsAsync();
    }

    private void ClearGrids()
    {
        _dataGridDetails.Rows.Clear();
        _dataGridSummary.Rows.Clear();
    }

    private void DisplayRecording(TimeStudyRecording recording)
    {
        _selectedRecording = recording;
        ClearGrids();

        // Display time entries
        foreach (TimeEntry entry in recording.Entries)
        {
            int rowIndex = _dataGridDetails.Rows.Add();
            DataGridViewRow row = _dataGridDetails.Rows[rowIndex];

            row.Cells[_colSeq.Index].Value = entry.SequenceNumber;
            row.Cells[_colOrderNumber.Index].Value = entry.ProcessStepOrderNumber;
            row.Cells[_colDescription.Index].Value = entry.ProcessStepDescription;
            row.Cells[_colTimestamp.Index].Value = entry.Timestamp.ToString("HH:mm:ss.fff");
            row.Cells[_colElapsed.Index].Value = FormatTimeSpan(entry.ElapsedFromStart);
            row.Cells[_colDuration.Index].Value = FormatTimeSpan(entry.Duration);
        }

        // Display summary
        foreach (ProcessStepSummary summary in recording.GetSummary())
        {
            int rowIndex = _dataGridSummary.Rows.Add();
            DataGridViewRow row = _dataGridSummary.Rows[rowIndex];

            row.Cells[_colSumOrderNumber.Index].Value = summary.OrderNumber;
            row.Cells[_colSumDescription.Index].Value = summary.Description;
            row.Cells[_colSumCount.Index].Value = summary.Count;
            row.Cells[_colSumTotalDuration.Index].Value = FormatTimeSpan(summary.TotalDuration);
            row.Cells[_colSumAvgDuration.Index].Value = FormatTimeSpan(summary.AverageDuration);
        }

        RecordingSelected?.Invoke(this, EventArgs.Empty);
    }

    private static string FormatTimeSpan(TimeSpan timeSpan)
    {
        if (timeSpan.TotalHours >= 1)
        {
            return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    // Event handlers

    private void ListRecordings_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_listRecordings.SelectedItem is RecordingItem item)
        {
            DisplayRecording(item.Recording);
        }
    }

    /// <summary>
    /// Represents a recording item in the list.
    /// </summary>
    private sealed class RecordingItem
    {
        public TimeStudyRecording Recording { get; }

        public RecordingItem(TimeStudyRecording recording)
        {
            Recording = recording;
        }

        public override string ToString()
        {
            string duration = Recording.CompletedAt.HasValue
                ? $" ({(Recording.CompletedAt.Value - Recording.StartedAt).TotalMinutes:F1} min)"
                : "";

            return $"{Recording.StartedAt:yyyy-MM-dd HH:mm}{duration}";
        }
    }
}
