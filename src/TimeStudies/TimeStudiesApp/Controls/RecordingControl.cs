using System.ComponentModel;
using System.Diagnostics;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for conducting time study recordings with touch-friendly buttons.
/// </summary>
public partial class RecordingControl : UserControl
{
    private readonly RecordingService _recordingService;
    private readonly SettingsService _settingsService;

    private TimeStudyDefinition? _definition;
    private TimeStudyRecording? _recording;
    private readonly Stopwatch _stopwatch = new();
    private System.Windows.Forms.Timer? _timer;
    private ProcessStepDefinition? _currentStep;
    private readonly List<Button> _stepButtons = [];

    /// <summary>
    /// Event raised when recording state changes.
    /// </summary>
    public event EventHandler? RecordingStateChanged;

    /// <summary>
    /// Event raised when a recording is completed.
    /// </summary>
    public event EventHandler<TimeStudyRecording>? RecordingCompleted;

    /// <summary>
    /// Gets whether a recording is currently in progress.
    /// </summary>
    [Browsable(false)]
    public bool IsRecording => _stopwatch.IsRunning;

    /// <summary>
    /// Gets the current recording, if any.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeStudyRecording? CurrentRecording => _recording;

    /// <summary>
    /// Gets or sets the current definition.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeStudyDefinition? Definition
    {
        get => _definition;
        set
        {
            _definition = value;
            BuildStepButtons();
            UpdateUI();
        }
    }

    public RecordingControl(RecordingService recordingService, SettingsService settingsService)
    {
        _recordingService = recordingService;
        _settingsService = settingsService;

        InitializeComponent();
        ApplyLocalization();
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Dock = DockStyle.Fill;
        AutoScroll = true;

        // Top control panel
        _panelTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 120,
            Padding = new Padding(10),
            BackColor = Color.FromArgb(245, 245, 245)
        };

        // Start/Stop buttons
        _btnStart = new Button
        {
            Text = Resources.BtnStartRecording,
            Size = new Size(180, 50),
            Location = new Point(10, 10),
            Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
            BackColor = Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Image = IconFactory.CreateIcon(IconFactory.Icons.Play, 24, Color.White),
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextAlign = ContentAlignment.MiddleCenter
        };
        _btnStart.FlatAppearance.BorderSize = 0;
        _btnStart.Click += BtnStart_Click;

        _btnStop = new Button
        {
            Text = Resources.BtnStopRecording,
            Size = new Size(180, 50),
            Location = new Point(200, 10),
            Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
            BackColor = Color.FromArgb(220, 53, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Enabled = false,
            Image = IconFactory.CreateIcon(IconFactory.Icons.Stop, 24, Color.White),
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextAlign = ContentAlignment.MiddleCenter
        };
        _btnStop.FlatAppearance.BorderSize = 0;
        _btnStop.Click += BtnStop_Click;

        _btnPause = new Button
        {
            Text = Resources.BtnPause,
            Size = new Size(140, 50),
            Location = new Point(390, 10),
            Font = new Font(Font.FontFamily, 11),
            BackColor = Color.FromArgb(255, 193, 7),
            ForeColor = Color.Black,
            FlatStyle = FlatStyle.Flat,
            Enabled = false,
            Image = IconFactory.CreateIcon(IconFactory.Icons.Pause, 24, Color.Black),
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextAlign = ContentAlignment.MiddleCenter
        };
        _btnPause.FlatAppearance.BorderSize = 0;
        _btnPause.Click += BtnPause_Click;

        _panelTop.Controls.AddRange([_btnStart, _btnStop, _btnPause]);

        // Status panel
        _panelStatus = new Panel
        {
            Location = new Point(10, 70),
            Size = new Size(600, 40)
        };

        _lblTimeLabel = new Label
        {
            Text = Resources.LblProgressiveTime + ":",
            Location = new Point(0, 8),
            Size = new Size(130, 25),
            Font = new Font(Font.FontFamily, 11),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblTime = new Label
        {
            Text = "00:00:00.0",
            Location = new Point(135, 3),
            Size = new Size(150, 35),
            Font = new Font("Consolas", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 120, 212),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblCurrentStepLabel = new Label
        {
            Text = Resources.LblCurrentStep + ":",
            Location = new Point(300, 8),
            Size = new Size(120, 25),
            Font = new Font(Font.FontFamily, 11),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _lblCurrentStep = new Label
        {
            Text = "-",
            Location = new Point(425, 8),
            Size = new Size(200, 25),
            Font = new Font(Font.FontFamily, 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 120, 212),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _panelStatus.Controls.AddRange([_lblTimeLabel, _lblTime, _lblCurrentStepLabel, _lblCurrentStep]);
        _panelTop.Controls.Add(_panelStatus);

        // Entry count label
        _lblEntryCount = new Label
        {
            Location = new Point(550, 15),
            Size = new Size(150, 40),
            Font = new Font(Font.FontFamily, 10),
            TextAlign = ContentAlignment.MiddleRight,
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        _panelTop.Controls.Add(_lblEntryCount);

        // Process step buttons panel
        _panelSteps = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10),
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true
        };

        // Entries list (bottom)
        _panelEntries = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 200,
            Padding = new Padding(5)
        };

        _dgvEntries = new DataGridView
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
        SetupEntriesGrid();

        _panelEntries.Controls.Add(_dgvEntries);

        // Splitter between steps and entries
        _splitter = new Splitter
        {
            Dock = DockStyle.Bottom,
            Height = 5,
            BackColor = Color.LightGray
        };

        Controls.Add(_panelSteps);
        Controls.Add(_splitter);
        Controls.Add(_panelEntries);
        Controls.Add(_panelTop);

        // Timer for UI updates
        _timer = new System.Windows.Forms.Timer
        {
            Interval = 100
        };
        _timer.Tick += Timer_Tick;

        ResumeLayout(true);
    }

    private void SetupEntriesGrid()
    {
        _dgvEntries.Columns.Clear();

        _dgvEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Seq",
            HeaderText = "#",
            Width = 40
        });

        _dgvEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.ColOrderNumber,
            Width = 50
        });

        _dgvEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.ColDescription,
            FillWeight = 100
        });

        _dgvEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Timestamp",
            HeaderText = Resources.ColTimestamp,
            Width = 100
        });

        _dgvEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Progressive",
            HeaderText = Resources.ColProgressiveTime,
            Width = 90
        });

        _dgvEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Duration",
            HeaderText = Resources.ColDuration,
            Width = 90
        });
    }

    private void ApplyLocalization()
    {
        _btnStart.Text = Resources.BtnStartRecording;
        _btnStop.Text = Resources.BtnStopRecording;
        _btnPause.Text = Resources.BtnPause;
        _lblTimeLabel.Text = Resources.LblProgressiveTime + ":";
        _lblCurrentStepLabel.Text = Resources.LblCurrentStep + ":";
    }

    private void BuildStepButtons()
    {
        // Clear existing buttons
        foreach (var btn in _stepButtons)
        {
            btn.Dispose();
        }
        _stepButtons.Clear();
        _panelSteps.Controls.Clear();

        if (_definition == null) return;

        var buttonSize = _settingsService.CurrentSettings.ButtonSize;
        var buttonFont = new Font(Font.FontFamily, 10, FontStyle.Bold);

        foreach (var step in _definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            var btn = new Button
            {
                Size = new Size(buttonSize + 40, buttonSize),
                Margin = new Padding(5),
                Font = buttonFont,
                FlatStyle = FlatStyle.Flat,
                BackColor = step.IsDefaultStep ? Color.LightGoldenrodYellow : Color.FromArgb(240, 240, 240),
                Tag = step,
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false
            };

            // Multi-line text: Order number on top, description below
            btn.Text = $"{step.OrderNumber}\n{TruncateText(step.Description, 15)}";

            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.FlatAppearance.BorderSize = 1;
            btn.Click += StepButton_Click;

            _stepButtons.Add(btn);
            _panelSteps.Controls.Add(btn);
        }
    }

    private static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Length <= maxLength ? text : text[..(maxLength - 3)] + "...";
    }

    private void UpdateUI()
    {
        var hasDefinition = _definition != null;
        var isRecording = _stopwatch.IsRunning;

        _btnStart.Enabled = hasDefinition && !isRecording;
        _btnStop.Enabled = isRecording;
        _btnPause.Enabled = isRecording;

        foreach (var btn in _stepButtons)
        {
            btn.Enabled = isRecording;
        }

        if (!isRecording)
        {
            _lblTime.Text = "00:00:00.0";
            _lblCurrentStep.Text = "-";
        }

        UpdateEntryCount();
    }

    private void UpdateEntryCount()
    {
        var count = _recording?.Entries.Count ?? 0;
        _lblEntryCount.Text = $"{Resources.LblEntryCount}: {count}";
    }

    private void HighlightCurrentStep()
    {
        foreach (var btn in _stepButtons)
        {
            if (btn.Tag is ProcessStepDefinition step)
            {
                if (step == _currentStep)
                {
                    btn.BackColor = Color.FromArgb(0, 120, 212);
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = Color.DarkBlue;
                    btn.FlatAppearance.BorderSize = 3;
                }
                else
                {
                    btn.BackColor = step.IsDefaultStep ? Color.LightGoldenrodYellow : Color.FromArgb(240, 240, 240);
                    btn.ForeColor = Color.Black;
                    btn.FlatAppearance.BorderColor = Color.Gray;
                    btn.FlatAppearance.BorderSize = 1;
                }
            }
        }
    }

    private void AddEntryToGrid(TimeEntry entry)
    {
        var rowIndex = _dgvEntries.Rows.Add();
        var row = _dgvEntries.Rows[rowIndex];

        row.Cells["Seq"].Value = entry.SequenceNumber;
        row.Cells["OrderNumber"].Value = entry.ProcessStepOrderNumber;
        row.Cells["Description"].Value = entry.ProcessStepDescription;
        row.Cells["Timestamp"].Value = entry.Timestamp.ToString("HH:mm:ss");
        row.Cells["Progressive"].Value = entry.ElapsedFromStart.ToString(@"hh\:mm\:ss\.f");
        row.Cells["Duration"].Value = "...";

        // Scroll to the new row
        _dgvEntries.FirstDisplayedScrollingRowIndex = rowIndex;
    }

    private void UpdateLastEntryDuration()
    {
        if (_dgvEntries.Rows.Count > 1)
        {
            var previousRow = _dgvEntries.Rows[_dgvEntries.Rows.Count - 2];
            if (_recording?.Entries.Count > 1)
            {
                var entry = _recording.Entries[^2];
                previousRow.Cells["Duration"].Value = entry.Duration.ToString(@"mm\:ss\.f");
            }
        }
    }

    #region Event Handlers

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        StartRecording();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        if (_settingsService.CurrentSettings.ConfirmBeforeClosingRecording)
        {
            var result = MessageBox.Show(
                Resources.MsgConfirmStop,
                Resources.TitleConfirm,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;
        }

        StopRecording();
    }

    private void BtnPause_Click(object? sender, EventArgs e)
    {
        // Switch to default step
        var defaultStep = _definition?.GetDefaultProcessStep();
        if (defaultStep != null && defaultStep != _currentStep)
        {
            RecordStep(defaultStep);
        }
    }

    private void StepButton_Click(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.Tag is ProcessStepDefinition step)
        {
            if (step != _currentStep)
            {
                RecordStep(step);
            }
        }
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_stopwatch.IsRunning)
        {
            _lblTime.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.f");
        }
    }

    #endregion

    #region Recording Logic

    /// <summary>
    /// Starts a new recording.
    /// </summary>
    public void StartRecording()
    {
        if (_definition == null) return;

        _recording = _recordingService.CreateNew(_definition);
        _dgvEntries.Rows.Clear();

        // Start with default step
        _currentStep = _definition.GetDefaultProcessStep();

        _stopwatch.Restart();
        _timer?.Start();

        // Add first entry for the default step
        if (_currentStep != null)
        {
            _recordingService.AddEntry(_recording, _currentStep, TimeSpan.Zero);
            AddEntryToGrid(_recording.Entries[^1]);
        }

        HighlightCurrentStep();
        UpdateUI();
        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Stops the current recording.
    /// </summary>
    public void StopRecording()
    {
        if (_recording == null) return;

        _stopwatch.Stop();
        _timer?.Stop();

        _recordingService.Complete(_recording, _stopwatch.Elapsed);

        // Update the last entry duration in the grid
        if (_dgvEntries.Rows.Count > 0)
        {
            var lastRow = _dgvEntries.Rows[_dgvEntries.Rows.Count - 1];
            if (_recording.Entries.Count > 0)
            {
                var lastEntry = _recording.Entries[^1];
                lastRow.Cells["Duration"].Value = lastEntry.Duration.ToString(@"mm\:ss\.f");
            }
        }

        // Auto-save if enabled
        if (_settingsService.CurrentSettings.AutoSaveRecordings)
        {
            _recordingService.Save(_recording);
        }

        RecordingCompleted?.Invoke(this, _recording);
        UpdateUI();
        RecordingStateChanged?.Invoke(this, EventArgs.Empty);

        _currentStep = null;
        HighlightCurrentStep();
    }

    private void RecordStep(ProcessStepDefinition step)
    {
        if (_recording == null || !_stopwatch.IsRunning) return;

        var elapsed = _stopwatch.Elapsed;

        _recordingService.AddEntry(_recording, step, elapsed);
        UpdateLastEntryDuration();
        AddEntryToGrid(_recording.Entries[^1]);

        _currentStep = step;
        HighlightCurrentStep();
        _lblCurrentStep.Text = $"{step.OrderNumber}: {TruncateText(step.Description, 20)}";
        UpdateEntryCount();
    }

    #endregion

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
        base.Dispose(disposing);
    }

    // Controls
    private Panel _panelTop = null!;
    private Button _btnStart = null!;
    private Button _btnStop = null!;
    private Button _btnPause = null!;
    private Panel _panelStatus = null!;
    private Label _lblTimeLabel = null!;
    private Label _lblTime = null!;
    private Label _lblCurrentStepLabel = null!;
    private Label _lblCurrentStep = null!;
    private Label _lblEntryCount = null!;
    private FlowLayoutPanel _panelSteps = null!;
    private Panel _panelEntries = null!;
    private DataGridView _dgvEntries = null!;
    private Splitter _splitter = null!;
}
