using TimeStudiesApp.Models;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for viewing completed time study recordings and their summaries.
/// </summary>
public partial class ResultsControl : UserControl
{
    private readonly CsvExportService _csvExportService;
    private TimeStudyRecording? _currentRecording;

    /// <summary>
    /// Event raised when export is requested.
    /// </summary>
    public event EventHandler? ExportRequested;

    public ResultsControl()
    {
        InitializeComponent();

        _csvExportService = new CsvExportService();

        SetupEventHandlers();
        SetupDataGridViews();
    }

    private void SetupEventHandlers()
    {
        _btnExport.Click += OnExportClick;
    }

    private void SetupDataGridViews()
    {
        // Details columns
        _dgvDetails.Columns.Add("Seq", "Seq");
        _dgvDetails.Columns.Add("OrderNumber", "Order #");
        _dgvDetails.Columns.Add("Description", "Description");
        _dgvDetails.Columns.Add("Timestamp", "Timestamp");
        _dgvDetails.Columns.Add("ProgressiveTime", "Progressive Time");
        _dgvDetails.Columns.Add("Duration", "Duration");
        _dgvDetails.Columns.Add("DimensionValue", "Dimension Value");

        _dgvDetails.Columns["Seq"]!.FillWeight = 40;
        _dgvDetails.Columns["OrderNumber"]!.FillWeight = 50;
        _dgvDetails.Columns["Description"]!.FillWeight = 150;
        _dgvDetails.Columns["Timestamp"]!.FillWeight = 100;
        _dgvDetails.Columns["ProgressiveTime"]!.FillWeight = 80;
        _dgvDetails.Columns["Duration"]!.FillWeight = 80;
        _dgvDetails.Columns["DimensionValue"]!.FillWeight = 70;

        // Summary columns
        _dgvSummary.Columns.Add("OrderNumber", "Order #");
        _dgvSummary.Columns.Add("Description", "Description");
        _dgvSummary.Columns.Add("Count", "Count");
        _dgvSummary.Columns.Add("TotalDuration", "Total Duration");
        _dgvSummary.Columns.Add("AvgDuration", "Avg Duration");

        _dgvSummary.Columns["OrderNumber"]!.FillWeight = 60;
        _dgvSummary.Columns["Description"]!.FillWeight = 200;
        _dgvSummary.Columns["Count"]!.FillWeight = 60;
        _dgvSummary.Columns["TotalDuration"]!.FillWeight = 100;
        _dgvSummary.Columns["AvgDuration"]!.FillWeight = 100;
    }

    /// <summary>
    /// Loads a recording and displays its data.
    /// </summary>
    public void LoadRecording(TimeStudyRecording? recording)
    {
        _currentRecording = recording;

        _dgvDetails.Rows.Clear();
        _dgvSummary.Rows.Clear();

        if (recording == null)
        {
            _lblRecordingInfo.Text = "No recording available";
            _btnExport.Enabled = false;
            return;
        }

        // Update header info
        string duration = FormatTimeSpan(recording.TotalDuration);
        _lblRecordingInfo.Text = $"{recording.DefinitionName} | {recording.StartedAt:g} | Duration: {duration} | Entries: {recording.EntryCount}";
        _btnExport.Enabled = true;

        // Populate details
        foreach (var entry in recording.Entries)
        {
            _dgvDetails.Rows.Add(
                entry.SequenceNumber,
                entry.ProcessStepOrderNumber,
                entry.ProcessStepDescription,
                entry.Timestamp.ToString("HH:mm:ss"),
                FormatTimeSpan(entry.ElapsedFromStart),
                FormatTimeSpan(entry.Duration),
                entry.DimensionValue?.ToString() ?? "-"
            );
        }

        // Populate summary
        foreach (var summary in recording.GetSummaryByStep())
        {
            _dgvSummary.Rows.Add(
                summary.OrderNumber,
                summary.Description,
                summary.Count,
                FormatTimeSpan(summary.TotalDuration),
                FormatTimeSpan(summary.AverageDuration)
            );
        }
    }

    private void OnExportClick(object? sender, EventArgs e)
    {
        ExportRequested?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Exports the current recording to a CSV file.
    /// </summary>
    public bool ExportToCsv(string filePath)
    {
        if (_currentRecording == null)
            return false;

        try
        {
            _csvExportService.Export(_currentRecording, filePath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the current recording.
    /// </summary>
    public TimeStudyRecording? GetCurrentRecording() => _currentRecording;

    private static string FormatTimeSpan(TimeSpan ts)
    {
        if (ts.TotalHours >= 1)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)ts.TotalHours,
                ts.Minutes,
                ts.Seconds);
        }
        return string.Format("{0:D2}:{1:D2}",
            ts.Minutes,
            ts.Seconds);
    }

    /// <summary>
    /// Applies localization to the control.
    /// </summary>
    public void ApplyLocalization(
        string tabDetails,
        string tabSummary,
        string btnExport,
        string colSeq,
        string colOrderNumber,
        string colDescription,
        string colTimestamp,
        string colProgressiveTime,
        string colDuration,
        string colDimensionValue,
        string colCount,
        string colTotalDuration,
        string colAvgDuration)
    {
        _tabDetails.Text = tabDetails;
        _tabSummary.Text = tabSummary;
        _btnExport.Text = btnExport;

        _dgvDetails.Columns["Seq"]!.HeaderText = colSeq;
        _dgvDetails.Columns["OrderNumber"]!.HeaderText = colOrderNumber;
        _dgvDetails.Columns["Description"]!.HeaderText = colDescription;
        _dgvDetails.Columns["Timestamp"]!.HeaderText = colTimestamp;
        _dgvDetails.Columns["ProgressiveTime"]!.HeaderText = colProgressiveTime;
        _dgvDetails.Columns["Duration"]!.HeaderText = colDuration;
        _dgvDetails.Columns["DimensionValue"]!.HeaderText = colDimensionValue;

        _dgvSummary.Columns["OrderNumber"]!.HeaderText = colOrderNumber;
        _dgvSummary.Columns["Description"]!.HeaderText = colDescription;
        _dgvSummary.Columns["Count"]!.HeaderText = colCount;
        _dgvSummary.Columns["TotalDuration"]!.HeaderText = colTotalDuration;
        _dgvSummary.Columns["AvgDuration"]!.HeaderText = colAvgDuration;
    }
}
