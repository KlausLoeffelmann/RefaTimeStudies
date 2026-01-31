using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Controls;

/// <summary>
///  UserControl for displaying time study recording results.
/// </summary>
public partial class ResultsControl : UserControl
{
    private TimeStudyRecording? _recording;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ResultsControl"/> class.
    /// </summary>
    public ResultsControl()
    {
        InitializeComponent();
        ApplyLocalization();
        SetupDataGridViews();
    }

    /// <summary>
    ///  Applies localization to all UI elements.
    /// </summary>
    private void ApplyLocalization()
    {
        _lblDetailTitle.Text = Resources.ViewResults;
        _lblSummaryTitle.Text = "Summary";
    }

    /// <summary>
    ///  Sets up the DataGridView columns.
    /// </summary>
    private void SetupDataGridViews()
    {
        // Detail grid columns
        _dgvDetail.AutoGenerateColumns = false;
        _dgvDetail.Columns.Clear();

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "SequenceNumber",
            HeaderText = Resources.ColSequence,
            DataPropertyName = "SequenceNumber",
            Width = 50
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ProcessStepOrderNumber",
            HeaderText = Resources.LblOrderNumber,
            DataPropertyName = "ProcessStepOrderNumber",
            Width = 60
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ProcessStepDescription",
            HeaderText = Resources.LblDescription,
            DataPropertyName = "ProcessStepDescription",
            Width = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Timestamp",
            HeaderText = Resources.ColTimestamp,
            DataPropertyName = "Timestamp",
            Width = 150
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ElapsedFromStart",
            HeaderText = Resources.LblProgressiveTime,
            DataPropertyName = "ElapsedFromStart",
            Width = 100
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Duration",
            HeaderText = Resources.ColDuration,
            DataPropertyName = "Duration",
            Width = 100
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DurationSeconds",
            HeaderText = Resources.ColDurationSeconds,
            Width = 80
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DimensionValue",
            HeaderText = Resources.LblDefaultValue,
            DataPropertyName = "DimensionValue",
            Width = 80
        });

        // Summary grid columns
        _dgvSummary.AutoGenerateColumns = false;
        _dgvSummary.Columns.Clear();

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.LblOrderNumber,
            DataPropertyName = "OrderNumber",
            Width = 60
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.LblDescription,
            DataPropertyName = "Description",
            Width = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Count",
            HeaderText = Resources.ColCount,
            DataPropertyName = "Count",
            Width = 60
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TotalDuration",
            HeaderText = Resources.ColTotal,
            DataPropertyName = "TotalDuration",
            Width = 100
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TotalSeconds",
            HeaderText = Resources.ColDurationSeconds,
            Width = 100
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "AverageDuration",
            HeaderText = Resources.ColAverage,
            DataPropertyName = "AverageDuration",
            Width = 100
        });
    }

    /// <summary>
    ///  Loads a recording and displays its results.
    /// </summary>
    /// <param name="recording">The recording to display.</param>
    public void LoadRecording(TimeStudyRecording recording)
    {
        ArgumentNullException.ThrowIfNull(recording);

        _recording = recording;

        // Update header info
        _lblDefinitionName.Text = recording.DefinitionName;
        _lblStartTime.Text = recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss");
        _lblEndTime.Text = recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
        _lblTotalDuration.Text = FormatTimeSpan(recording.TotalDuration);
        _lblEntryCount.Text = recording.Entries.Count.ToString();

        // Populate detail grid
        PopulateDetailGrid();

        // Populate summary grid
        PopulateSummaryGrid();
    }

    /// <summary>
    ///  Populates the detail grid with all time entries.
    /// </summary>
    private void PopulateDetailGrid()
    {
        if (_recording is null)
        {
            return;
        }

        _dgvDetail.Rows.Clear();

        foreach (var entry in _recording.Entries.OrderBy(e => e.SequenceNumber))
        {
            int rowIndex = _dgvDetail.Rows.Add();
            var row = _dgvDetail.Rows[rowIndex];

            row.Cells["SequenceNumber"].Value = entry.SequenceNumber;
            row.Cells["ProcessStepOrderNumber"].Value = entry.ProcessStepOrderNumber;
            row.Cells["ProcessStepDescription"].Value = entry.ProcessStepDescription;
            row.Cells["Timestamp"].Value = entry.Timestamp.ToString("HH:mm:ss.fff");
            row.Cells["ElapsedFromStart"].Value = FormatTimeSpan(entry.ElapsedFromStart);
            row.Cells["Duration"].Value = FormatTimeSpan(entry.Duration);
            row.Cells["DurationSeconds"].Value = entry.Duration.TotalSeconds.ToString("F2");
            row.Cells["DimensionValue"].Value = entry.DimensionValue;
        }
    }

    /// <summary>
    ///  Populates the summary grid with aggregated statistics.
    /// </summary>
    private void PopulateSummaryGrid()
    {
        if (_recording is null)
        {
            return;
        }

        _dgvSummary.Rows.Clear();

        var summary = _recording.GetSummaryByProcessStep();

        foreach (var item in summary.Values.OrderBy(s => s.OrderNumber))
        {
            int rowIndex = _dgvSummary.Rows.Add();
            var row = _dgvSummary.Rows[rowIndex];

            row.Cells["OrderNumber"].Value = item.OrderNumber;
            row.Cells["Description"].Value = item.Description;
            row.Cells["Count"].Value = item.Count;
            row.Cells["TotalDuration"].Value = FormatTimeSpan(item.TotalDuration);
            row.Cells["TotalSeconds"].Value = item.TotalDuration.TotalSeconds.ToString("F2");
            row.Cells["AverageDuration"].Value = FormatTimeSpan(item.AverageDuration);
        }
    }

    /// <summary>
    ///  Formats a TimeSpan as HH:MM:SS.
    /// </summary>
    private static string FormatTimeSpan(TimeSpan ts)
    {
        if (ts.TotalHours >= 1)
        {
            return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
        }
        return $"{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds / 100}";
    }
}
