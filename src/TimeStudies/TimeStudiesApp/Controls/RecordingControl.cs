using TimeStudiesApp.Models;
using TimeStudiesApp.Services;
using TimeStudiesApp.ViewModels;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for conducting time study recordings with touch-friendly UI.
/// </summary>
public partial class RecordingControl : UserControl
{
    private readonly RecordingViewModel _viewModel;
    private readonly System.Windows.Forms.Timer _displayTimer;
    private readonly List<Button> _stepButtons = new();
    private int _buttonSize = 60;
    private Button? _activeButton;

    /// <summary>
    /// Event raised when recording starts.
    /// </summary>
    public event EventHandler? RecordingStarted;

    /// <summary>
    /// Event raised when recording stops.
    /// </summary>
    public event EventHandler<TimeStudyRecording>? RecordingStopped;

    /// <summary>
    /// Event raised when a step is recorded.
    /// </summary>
    public event EventHandler<TimeEntry>? StepRecorded;

    public RecordingControl()
    {
        InitializeComponent();

        var recordingService = new RecordingService();
        _viewModel = new RecordingViewModel(recordingService);

        _displayTimer = new System.Windows.Forms.Timer
        {
            Interval = 100
        };
        _displayTimer.Tick += OnDisplayTimerTick;

        SetupEventHandlers();
    }

    /// <summary>
    /// Initializes the control with an existing recording service.
    /// </summary>
    public void Initialize(RecordingService recordingService)
    {
        // Re-create view model with the shared service
        var vm = new RecordingViewModel(recordingService);
        vm.StepRecorded += OnStepRecorded;
    }

    private void SetupEventHandlers()
    {
        _btnStart.Click += OnStartClick;
        _btnStop.Click += OnStopClick;
        _btnPause.Click += OnPauseClick;

        _viewModel.StepRecorded += OnStepRecorded;
    }

    /// <summary>
    /// Loads a definition and creates step buttons.
    /// </summary>
    public void LoadDefinition(TimeStudyDefinition? definition)
    {
        _viewModel.LoadDefinition(definition);
        CreateStepButtons(definition);
        UpdateControlState(false);
    }

    /// <summary>
    /// Sets the button size for process step buttons.
    /// </summary>
    public void SetButtonSize(int size)
    {
        _buttonSize = Math.Max(48, size);
        if (_viewModel.Definition != null)
        {
            CreateStepButtons(_viewModel.Definition);
        }
    }

    private void CreateStepButtons(TimeStudyDefinition? definition)
    {
        _flowLayoutPanel.SuspendLayout();

        // Clear existing buttons
        foreach (var btn in _stepButtons)
        {
            btn.Click -= OnStepButtonClick;
            _flowLayoutPanel.Controls.Remove(btn);
            btn.Dispose();
        }
        _stepButtons.Clear();
        _activeButton = null;

        if (definition == null)
        {
            _flowLayoutPanel.ResumeLayout(true);
            return;
        }

        // Create buttons for each step
        foreach (var step in definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            var btn = new Button
            {
                Size = new Size(_buttonSize, _buttonSize),
                MinimumSize = new Size(60, 60),
                Margin = new Padding(5),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = $"{step.OrderNumber}\n{Truncate(step.Description, 10)}",
                Tag = step.OrderNumber,
                BackColor = step.IsDefaultStep ? Color.LightBlue : SystemColors.Control,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };

            btn.FlatAppearance.BorderSize = 2;
            btn.Click += OnStepButtonClick;

            _stepButtons.Add(btn);
            _flowLayoutPanel.Controls.Add(btn);
        }

        _flowLayoutPanel.ResumeLayout(true);
    }

    private void UpdateControlState(bool isRecording)
    {
        _btnStart.Enabled = !isRecording && _viewModel.Definition != null;
        _btnStop.Enabled = isRecording;
        _btnPause.Enabled = isRecording;

        _lblStatus.Text = isRecording ? "Recording..." : "Not Recording";
        _lblStatus.ForeColor = isRecording ? Color.Green : Color.Gray;

        foreach (var btn in _stepButtons)
        {
            btn.Enabled = isRecording;
        }
    }

    private void UpdateActiveButton(int orderNumber)
    {
        // Reset previous active button
        if (_activeButton != null)
        {
            var prevStep = _viewModel.Definition?.GetProcessStep((int)_activeButton.Tag!);
            _activeButton.BackColor = prevStep?.IsDefaultStep == true ? Color.LightBlue : SystemColors.Control;
            _activeButton.FlatAppearance.BorderColor = SystemColors.ControlDark;
        }

        // Highlight new active button
        _activeButton = _stepButtons.FirstOrDefault(b => (int)b.Tag! == orderNumber);
        if (_activeButton != null)
        {
            _activeButton.BackColor = Color.LightGreen;
            _activeButton.FlatAppearance.BorderColor = Color.DarkGreen;
        }
    }

    private void OnStartClick(object? sender, EventArgs e)
    {
        if (_viewModel.Definition == null)
            return;

        var recordingService = _viewModel.GetRecordingService();
        recordingService.StartRecording(_viewModel.Definition);

        _displayTimer.Start();
        UpdateControlState(true);

        // Highlight default step
        UpdateActiveButton(_viewModel.Definition.DefaultProcessStepOrderNumber);

        RecordingStarted?.Invoke(this, EventArgs.Empty);
    }

    private void OnStopClick(object? sender, EventArgs e)
    {
        var recordingService = _viewModel.GetRecordingService();
        var recording = recordingService.StopRecording();

        _displayTimer.Stop();
        UpdateControlState(false);

        RecordingStopped?.Invoke(this, recording);
    }

    private void OnPauseClick(object? sender, EventArgs e)
    {
        _viewModel.Pause();
    }

    private void OnStepButtonClick(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.Tag is int orderNumber)
        {
            _viewModel.RecordStep(orderNumber);
        }
    }

    private void OnStepRecorded(object? sender, TimeEntry entry)
    {
        if (InvokeRequired)
        {
            Invoke(() => OnStepRecorded(sender, entry));
            return;
        }

        UpdateActiveButton(entry.ProcessStepOrderNumber);
        _lblCurrentStep.Text = $"{entry.ProcessStepOrderNumber}: {entry.ProcessStepDescription}";
        _lblEntryCount.Text = _viewModel.EntryCount.ToString();

        StepRecorded?.Invoke(this, entry);
    }

    private void OnDisplayTimerTick(object? sender, EventArgs e)
    {
        _viewModel.UpdateElapsedTime();
        _lblProgressiveTime.Text = FormatTimeSpan(_viewModel.ElapsedTime);
    }

    private static string FormatTimeSpan(TimeSpan ts)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            (int)ts.TotalHours,
            ts.Minutes,
            ts.Seconds);
    }

    private static string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        return text.Length <= maxLength ? text : text[..maxLength] + "...";
    }

    /// <summary>
    /// Gets the recording service for external access.
    /// </summary>
    public RecordingService GetRecordingService() => _viewModel.GetRecordingService();

    /// <summary>
    /// Applies localization to the control.
    /// </summary>
    public void ApplyLocalization(
        string btnStart,
        string btnStop,
        string btnPause,
        string lblProgressiveTime,
        string lblCurrentStep,
        string lblEntries,
        string lblNotRecording,
        string lblRecording)
    {
        _btnStart.Text = btnStart;
        _btnStop.Text = btnStop;
        _btnPause.Text = btnPause;
        _lblProgressiveTimeLabel.Text = lblProgressiveTime + ":";
        _lblCurrentStepLabel.Text = lblCurrentStep + ":";
        _lblEntryCountLabel.Text = lblEntries + ":";
        // Store for status updates
        Tag = new[] { lblNotRecording, lblRecording };
    }
}
