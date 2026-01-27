using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for viewing completed time study recordings.
/// </summary>
public partial class ResultsControl : UserControl
{
    private readonly RecordingService _recordingService;
    private readonly DefinitionService _definitionService;
    private TimeStudyRecording? _recording;
    private TimeStudyDefinition? _definition;

    /// <summary>
    /// Gets the current recording being displayed.
    /// </summary>
    public TimeStudyRecording? Recording => _recording;

    /// <summary>
    /// Gets the definition for the current recording.
    /// </summary>
    public TimeStudyDefinition? Definition => _definition;

    public ResultsControl(RecordingService recordingService, DefinitionService definitionService)
    {
        _recordingService = recordingService;
        _definitionService = definitionService;

        InitializeComponent();
        ApplyLocalization();
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Dock = DockStyle.Fill;
        Padding = new Padding(10);

        // Header panel
        _panelHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100,
            Padding = new Padding(5)
        };

        // Definition name
        var lblDefName = new Label
        {
            Text = Resources.LblDefinitionName + ":",
            Location = new Point(5, 10),
            Size = new Size(120, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblDefinitionName = new Label
        {
            Location = new Point(130, 10),
            Size = new Size(300, 23),
            Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft
        };

        // Recording times
        var lblStarted = new Label
        {
            Text = "Started:",
            Location = new Point(5, 40),
            Size = new Size(60, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblStarted = new Label
        {
            Location = new Point(70, 40),
            Size = new Size(150, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var lblCompleted = new Label
        {
            Text = "Completed:",
            Location = new Point(230, 40),
            Size = new Size(80, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblCompleted = new Label
        {
            Location = new Point(315, 40),
            Size = new Size(150, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var lblDuration = new Label
        {
            Text = "Duration:",
            Location = new Point(5, 65),
            Size = new Size(60, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblDuration = new Label
        {
            Location = new Point(70, 65),
            Size = new Size(150, 23),
            Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var lblEntries = new Label
        {
            Text = "Entries:",
            Location = new Point(230, 65),
            Size = new Size(80, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblEntryCount = new Label
        {
            Location = new Point(315, 65),
            Size = new Size(100, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _panelHeader.Controls.AddRange([
            lblDefName, _lblDefinitionName,
            lblStarted, _lblStarted,
            lblCompleted, _lblCompleted,
            lblDuration, _lblDuration,
            lblEntries, _lblEntryCount
        ]);

        // Tab control for details and summary
        _tabControl = new TabControl
        {
            Dock = DockStyle.Fill
        };

        // Details tab
        _tabDetails = new TabPage
        {
            Text = "Details",
            Padding = new Padding(5)
        };

        _dgvDetails = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = SystemColors.Window,
            BorderStyle = BorderStyle.Fixed3D
        };
        SetupDetailsGrid();
        _tabDetails.Controls.Add(_dgvDetails);
        _tabControl.TabPages.Add(_tabDetails);

        // Summary tab
        _tabSummary = new TabPage
        {
            Text = "Summary",
            Padding = new Padding(5)
        };

        _dgvSummary = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = SystemColors.Window,
            BorderStyle = BorderStyle.Fixed3D
        };
        SetupSummaryGrid();
        _tabSummary.Controls.Add(_dgvSummary);
        _tabControl.TabPages.Add(_tabSummary);

        Controls.Add(_tabControl);
        Controls.Add(_panelHeader);

        ResumeLayout(true);
    }

    private void SetupDetailsGrid()
    {
        _dgvDetails.Columns.Clear();

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Seq",
            HeaderText = "#",
            Width = 50
        });

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.ColOrderNumber,
            Width = 60
        });

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.ColDescription,
            FillWeight = 100
        });

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Timestamp",
            HeaderText = Resources.ColTimestamp,
            Width = 100
        });

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Progressive",
            HeaderText = Resources.ColProgressiveTime,
            Width = 100
        });

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Duration",
            HeaderText = Resources.ColDuration,
            Width = 100
        });

        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DurationSec",
            HeaderText = "Duration (s)",
            Width = 80
        });
    }

    private void SetupSummaryGrid()
    {
        _dgvSummary.Columns.Clear();

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.ColOrderNumber,
            Width = 70
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.ColDescription,
            FillWeight = 100
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Count",
            HeaderText = "Count",
            Width = 70
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TotalDuration",
            HeaderText = "Total Duration",
            Width = 120
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TotalSeconds",
            HeaderText = "Total (s)",
            Width = 80
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TotalMinutes",
            HeaderText = "Total (min)",
            Width = 80
        });

        _dgvSummary.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "AvgSeconds",
            HeaderText = "Avg (s)",
            Width = 80
        });
    }

    private void ApplyLocalization()
    {
        // Update column headers if needed
    }

    /// <summary>
    /// Displays a recording result.
    /// </summary>
    public void ShowRecording(TimeStudyRecording recording, TimeStudyDefinition definition)
    {
        _recording = recording;
        _definition = definition;

        // Update header
        _lblDefinitionName.Text = recording.DefinitionName;
        _lblStarted.Text = recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss");
        _lblCompleted.Text = recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
        _lblDuration.Text = recording.GetTotalDuration().ToString(@"hh\:mm\:ss");
        _lblEntryCount.Text = recording.Entries.Count.ToString();

        // Load details
        LoadDetails();

        // Load summary
        LoadSummary();
    }

    private void LoadDetails()
    {
        _dgvDetails.Rows.Clear();

        if (_recording == null) return;

        foreach (var entry in _recording.Entries)
        {
            var rowIndex = _dgvDetails.Rows.Add();
            var row = _dgvDetails.Rows[rowIndex];

            row.Cells["Seq"].Value = entry.SequenceNumber;
            row.Cells["OrderNumber"].Value = entry.ProcessStepOrderNumber;
            row.Cells["Description"].Value = entry.ProcessStepDescription;
            row.Cells["Timestamp"].Value = entry.Timestamp.ToString("HH:mm:ss");
            row.Cells["Progressive"].Value = entry.ElapsedFromStart.ToString(@"hh\:mm\:ss\.f");
            row.Cells["Duration"].Value = entry.Duration.ToString(@"mm\:ss\.f");
            row.Cells["DurationSec"].Value = entry.Duration.TotalSeconds.ToString("F2");
        }
    }

    private void LoadSummary()
    {
        _dgvSummary.Rows.Clear();

        if (_recording == null || _definition == null) return;

        var stepSummary = _recording.Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .Select(g => new
            {
                OrderNumber = g.Key,
                Description = g.First().ProcessStepDescription,
                Count = g.Count(),
                TotalDuration = TimeSpan.FromTicks(g.Sum(e => e.Duration.Ticks))
            })
            .OrderBy(s => s.OrderNumber)
            .ToList();

        foreach (var item in stepSummary)
        {
            var rowIndex = _dgvSummary.Rows.Add();
            var row = _dgvSummary.Rows[rowIndex];

            row.Cells["OrderNumber"].Value = item.OrderNumber;
            row.Cells["Description"].Value = item.Description;
            row.Cells["Count"].Value = item.Count;
            row.Cells["TotalDuration"].Value = item.TotalDuration.ToString(@"hh\:mm\:ss");
            row.Cells["TotalSeconds"].Value = item.TotalDuration.TotalSeconds.ToString("F2");
            row.Cells["TotalMinutes"].Value = item.TotalDuration.TotalMinutes.ToString("F2");
            row.Cells["AvgSeconds"].Value = (item.TotalDuration.TotalSeconds / item.Count).ToString("F2");

            // Highlight default step
            var step = _definition.ProcessSteps.FirstOrDefault(s => s.OrderNumber == item.OrderNumber);
            if (step?.IsDefaultStep == true)
            {
                row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            }
        }
    }

    /// <summary>
    /// Clears the displayed results.
    /// </summary>
    public void Clear()
    {
        _recording = null;
        _definition = null;

        _lblDefinitionName.Text = "-";
        _lblStarted.Text = "-";
        _lblCompleted.Text = "-";
        _lblDuration.Text = "-";
        _lblEntryCount.Text = "-";

        _dgvDetails.Rows.Clear();
        _dgvSummary.Rows.Clear();
    }

    // Controls
    private Panel _panelHeader = null!;
    private Label _lblDefinitionName = null!;
    private Label _lblStarted = null!;
    private Label _lblCompleted = null!;
    private Label _lblDuration = null!;
    private Label _lblEntryCount = null!;
    private TabControl _tabControl = null!;
    private TabPage _tabDetails = null!;
    private TabPage _tabSummary = null!;
    private DataGridView _dgvDetails = null!;
    private DataGridView _dgvSummary = null!;
}
