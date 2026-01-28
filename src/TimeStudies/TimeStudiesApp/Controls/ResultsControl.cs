using System.ComponentModel;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Controls;

/// <summary>
/// UserControl for displaying completed time study recording results.
/// </summary>
public partial class ResultsControl : UserControl
{
    private TimeStudyRecording? _recording;

    /// <summary>
    /// Gets or sets the recording to display.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeStudyRecording? Recording
    {
        get => _recording;
        set
        {
            _recording = value;
            LoadRecording();
        }
    }

    public ResultsControl()
    {
        InitializeComponent();
        ApplyLocalization();
        SetupDetailGrid();
        SetupSummaryGrid();
    }

    private void ApplyLocalization()
    {
        _lblDefinitionCaption.Text = Resources.LblDefinitionName + ":";
        _lblStartedCaption.Text = "Started:";
        _lblCompletedCaption.Text = "Completed:";
        _lblTotalDurationCaption.Text = Resources.LblTotalDuration + ":";
        _lblEntriesCaption.Text = Resources.LblEntries + ":";

        _tabDetail.Text = "Detail";
        _tabSummary.Text = "Summary";
    }

    private void SetupDetailGrid()
    {
        _dgvDetail.AutoGenerateColumns = false;
        _dgvDetail.AllowUserToAddRows = false;
        _dgvDetail.AllowUserToDeleteRows = false;
        _dgvDetail.ReadOnly = true;
        _dgvDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDetail.RowHeadersVisible = false;

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
            HeaderText = Resources.ColOrderNumber,
            DataPropertyName = "ProcessStepOrderNumber",
            Width = 60
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ProcessStepDescription",
            HeaderText = Resources.ColDescription,
            DataPropertyName = "ProcessStepDescription",
            Width = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Timestamp",
            HeaderText = Resources.ColTimestamp,
            DataPropertyName = "Timestamp",
            Width = 140,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" }
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ElapsedFromStart",
            HeaderText = Resources.ColProgressiveTime,
            DataPropertyName = "ElapsedFromStart",
            Width = 100,
            DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm\:ss" }
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Duration",
            HeaderText = Resources.ColDuration,
            DataPropertyName = "Duration",
            Width = 100,
            DefaultCellStyle = new DataGridViewCellStyle { Format = @"mm\:ss\.f" }
        });

        _dgvDetail.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DimensionValue",
            HeaderText = Resources.ColDimensionValue,
            DataPropertyName = "DimensionValue",
            Width = 60
        });
    }

    private void SetupSummaryGrid()
    {
        _dgvSummary.AutoGenerateColumns = false;
        _dgvSummary.AllowUserToAddRows = false;
        _dgvSummary.AllowUserToDeleteRows = false;
        _dgvSummary.ReadOnly = true;
        _dgvSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvSummary.RowHeadersVisible = false;

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.ColOrderNumber,
            DataPropertyName = "OrderNumber",
            Width = 60
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.ColDescription,
            DataPropertyName = "Description",
            Width = 250,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "EntryCount",
            HeaderText = Resources.ColCount,
            DataPropertyName = "EntryCount",
            Width = 60
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TotalDuration",
            HeaderText = Resources.ColTotalDuration,
            DataPropertyName = "TotalDuration",
            Width = 120,
            DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm\:ss" }
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "AverageDuration",
            HeaderText = Resources.ColAvgDuration,
            DataPropertyName = "AverageDuration",
            Width = 120,
            DefaultCellStyle = new DataGridViewCellStyle { Format = @"mm\:ss\.f" }
        });
    }

    private void LoadRecording()
    {
        if (_recording is null)
        {
            ClearDisplay();
            return;
        }

        _lblDefinition.Text = _recording.DefinitionName;
        _lblStarted.Text = _recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss");
        _lblCompleted.Text = _recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
        _lblTotalDuration.Text = FormatTimeSpan(_recording.TotalDuration);
        _lblEntries.Text = _recording.Entries.Count.ToString();

        // Load detail grid
        BindingSource detailBindingSource = new()
        {
            DataSource = _recording.Entries
        };
        _dgvDetail.DataSource = detailBindingSource;

        // Load summary grid
        List<ProcessStepSummary> summaryList = _recording.GetSummary().ToList();
        BindingSource summaryBindingSource = new()
        {
            DataSource = summaryList
        };
        _dgvSummary.DataSource = summaryBindingSource;
    }

    private void ClearDisplay()
    {
        _lblDefinition.Text = "-";
        _lblStarted.Text = "-";
        _lblCompleted.Text = "-";
        _lblTotalDuration.Text = "-";
        _lblEntries.Text = "-";
        _dgvDetail.DataSource = null;
        _dgvSummary.DataSource = null;
    }

    private static string FormatTimeSpan(TimeSpan ts)
    {
        return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
    }
}
