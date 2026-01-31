using System.Diagnostics;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
///  UserControl for conducting time study recordings with touch-friendly buttons.
/// </summary>
public partial class RecordingControl : UserControl
{
    private readonly SettingsService _settingsService;
    private readonly Stopwatch _stopwatch;
    private readonly System.Windows.Forms.Timer _displayTimer;

    private TimeStudyDefinition? _definition;
    private TimeStudyRecording? _recording;
    private ProcessStepDefinition? _currentStep;
    private int _sequenceNumber;
    private readonly Dictionary<int, Button> _stepButtons;

    /// <summary>
    ///  Occurs when recording has started.
    /// </summary>
    public event EventHandler? RecordingStarted;

    /// <summary>
    ///  Occurs when recording has stopped.
    /// </summary>
    public event EventHandler<TimeStudyRecording>? RecordingStopped;

    /// <summary>
    ///  Gets a value indicating whether recording is active.
    /// </summary>
    public bool IsRecording => _stopwatch.IsRunning;

    /// <summary>
    ///  Initializes a new instance of the <see cref="RecordingControl"/> class.
    /// </summary>
    /// <param name="settingsService">The settings service.</param>
    public RecordingControl(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);

        _settingsService = settingsService;
        _stopwatch = new Stopwatch();
        _displayTimer = new System.Windows.Forms.Timer { Interval = 100 };
        _displayTimer.Tick += DisplayTimer_Tick;
        _stepButtons = [];

        InitializeComponent();
        ApplyLocalization();
        UpdateUIState();
    }

    /// <summary>
    ///  Applies localization to all UI elements.
    /// </summary>
    private void ApplyLocalization()
    {
        _lblProgressiveTimeCaption.Text = Resources.LblProgressiveTime;
        _lblCurrentStepCaption.Text = Resources.LblCurrentStep;
        _lblEntryCountCaption.Text = Resources.LblEntryCount;
        _btnStart.Text = Resources.BtnStartRecording;
        _btnStop.Text = Resources.BtnStopRecording;
        _btnPause.Text = Resources.BtnPause;
    }

    /// <summary>
    ///  Loads a definition and creates process step buttons.
    /// </summary>
    /// <param name="definition">The definition to use for recording.</param>
    public void LoadDefinition(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        _definition = definition;
        CreateStepButtons();
        UpdateUIState();
    }

    /// <summary>
    ///  Creates touch-friendly buttons for each process step.
    /// </summary>
    private void CreateStepButtons()
    {
        _pnlStepButtons.Controls.Clear();
        _stepButtons.Clear();

        if (_definition is null)
        {
            return;
        }

        int buttonSize = _settingsService.Settings.ButtonSize;

        foreach (var step in _definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            Button btn = new()
            {
                Text = $"{step.OrderNumber}\n{step.Description}",
                Tag = step,
                Size = new Size(buttonSize * 2, buttonSize),
                MinimumSize = new Size(buttonSize, buttonSize),
                Margin = new Padding(4),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };

            // Style default step differently
            if (step.IsDefaultStep)
            {
                btn.BackColor = Color.FromArgb(255, 193, 7); // Amber
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.FromArgb(0, 120, 215); // Blue
                btn.ForeColor = Color.White;
            }

            btn.FlatAppearance.BorderSize = 2;
            btn.Click += StepButton_Click;

            _pnlStepButtons.Controls.Add(btn);
            _stepButtons[step.OrderNumber] = btn;
        }
    }

    /// <summary>
    ///  Starts the recording session.
    /// </summary>
    public void StartRecording()
    {
        if (_definition is null)
        {
            return;
        }

        // Create new recording
        _recording = new TimeStudyRecording
        {
            DefinitionId = _definition.Id,
            DefinitionName = _definition.Name,
            StartedAt = DateTime.Now
        };

        _sequenceNumber = 0;
        _stopwatch.Reset();

        // Start on default step
        _currentStep = _definition.DefaultProcessStep;

        if (_currentStep is not null)
        {
            RecordTimeEntry(_currentStep);
        }

        _stopwatch.Start();
        _displayTimer.Start();

        UpdateUIState();
        UpdateStepButtonHighlight();

        RecordingStarted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///  Stops the recording session.
    /// </summary>
    public void StopRecording()
    {
        if (_recording is null)
        {
            return;
        }

        _stopwatch.Stop();
        _displayTimer.Stop();

        // Finalize the last entry's duration
        if (_recording.Entries.Count > 0)
        {
            var lastEntry = _recording.Entries[^1];
            lastEntry.Duration = _stopwatch.Elapsed - lastEntry.ElapsedFromStart;
        }

        _recording.IsCompleted = true;
        _recording.CompletedAt = DateTime.Now;

        UpdateUIState();
        ClearStepButtonHighlight();

        RecordingStopped?.Invoke(this, _recording);
    }

    /// <summary>
    ///  Records a time entry for switching to a new step.
    /// </summary>
    private void RecordTimeEntry(ProcessStepDefinition step)
    {
        if (_recording is null)
        {
            return;
        }

        TimeSpan elapsed = _stopwatch.Elapsed;

        // Calculate duration of previous entry
        if (_recording.Entries.Count > 0)
        {
            var previousEntry = _recording.Entries[^1];
            previousEntry.Duration = elapsed - previousEntry.ElapsedFromStart;
        }

        // Create new entry
        _sequenceNumber++;
        var entry = new TimeEntry
        {
            SequenceNumber = _sequenceNumber,
            ProcessStepOrderNumber = step.OrderNumber,
            ProcessStepDescription = step.Description,
            Timestamp = DateTime.Now,
            ElapsedFromStart = elapsed,
            DimensionValue = step.DefaultDimensionValue
        };

        _recording.Entries.Add(entry);
        _currentStep = step;

        UpdateEntryCount();
        UpdateCurrentStepDisplay();
        UpdateStepButtonHighlight();
    }

    /// <summary>
    ///  Updates the progressive time display.
    /// </summary>
    private void UpdateTimeDisplay()
    {
        TimeSpan elapsed = _stopwatch.Elapsed;
        _lblProgressiveTime.Text = $"{(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds / 100}";
    }

    /// <summary>
    ///  Updates the current step display.
    /// </summary>
    private void UpdateCurrentStepDisplay()
    {
        if (_currentStep is not null)
        {
            _lblCurrentStep.Text = $"{_currentStep.OrderNumber} - {_currentStep.Description}";
        }
        else
        {
            _lblCurrentStep.Text = "-";
        }
    }

    /// <summary>
    ///  Updates the entry count display.
    /// </summary>
    private void UpdateEntryCount()
    {
        _lblEntryCount.Text = _recording?.Entries.Count.ToString() ?? "0";
    }

    /// <summary>
    ///  Updates button enabled/disabled states.
    /// </summary>
    private void UpdateUIState()
    {
        bool isRecording = IsRecording;
        bool hasDefinition = _definition is not null;

        _btnStart.Enabled = hasDefinition && !isRecording;
        _btnStart.Visible = !isRecording;

        _btnStop.Enabled = isRecording;
        _btnStop.Visible = isRecording;

        _btnPause.Enabled = isRecording;
        _btnPause.Visible = isRecording;

        // Enable/disable step buttons
        foreach (var btn in _stepButtons.Values)
        {
            btn.Enabled = isRecording;
        }

        if (!isRecording)
        {
            _lblProgressiveTime.Text = "00:00:00.0";
            _lblCurrentStep.Text = "-";
            _lblEntryCount.Text = "0";
        }
    }

    /// <summary>
    ///  Highlights the currently active step button.
    /// </summary>
    private void UpdateStepButtonHighlight()
    {
        foreach (var kvp in _stepButtons)
        {
            var step = kvp.Value.Tag as ProcessStepDefinition;
            bool isActive = step?.OrderNumber == _currentStep?.OrderNumber;

            if (isActive)
            {
                kvp.Value.FlatAppearance.BorderColor = Color.Lime;
                kvp.Value.FlatAppearance.BorderSize = 4;
            }
            else
            {
                kvp.Value.FlatAppearance.BorderColor = SystemColors.ControlDark;
                kvp.Value.FlatAppearance.BorderSize = 2;
            }
        }
    }

    /// <summary>
    ///  Clears all step button highlighting.
    /// </summary>
    private void ClearStepButtonHighlight()
    {
        foreach (var btn in _stepButtons.Values)
        {
            btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
            btn.FlatAppearance.BorderSize = 2;
        }
    }

    #region Event Handlers

    private void DisplayTimer_Tick(object? sender, EventArgs e)
    {
        UpdateTimeDisplay();
    }

    private void StepButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not ProcessStepDefinition step)
        {
            return;
        }

        if (!IsRecording)
        {
            return;
        }

        // Don't record if clicking the already-active step
        if (step.OrderNumber == _currentStep?.OrderNumber)
        {
            return;
        }

        RecordTimeEntry(step);
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        StartRecording();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        StopRecording();
    }

    private void BtnPause_Click(object? sender, EventArgs e)
    {
        // Pause switches to the default step
        if (_definition?.DefaultProcessStep is not null)
        {
            if (_currentStep?.OrderNumber != _definition.DefaultProcessStep.OrderNumber)
            {
                RecordTimeEntry(_definition.DefaultProcessStep);
            }
        }
    }

    #endregion

    /// <summary>
    ///  Clean up resources.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _displayTimer.Stop();
            _displayTimer.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
