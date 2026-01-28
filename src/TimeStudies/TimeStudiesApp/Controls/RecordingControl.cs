using System.ComponentModel;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
/// UserControl for conducting time study recordings.
/// Touch-optimized with large buttons for process step selection.
/// </summary>
public partial class RecordingControl : UserControl
{
    private readonly RecordingService _recordingService;
    private readonly int _buttonSize;
    private TimeStudyDefinition? _definition;
    private System.Windows.Forms.Timer? _updateTimer;
    private readonly Dictionary<int, Button> _stepButtons = [];

    /// <summary>
    /// Gets or sets the definition to use for recording.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeStudyDefinition? Definition
    {
        get => _definition;
        set
        {
            _definition = value;
            BuildProcessStepButtons();
            UpdateDisplay();
        }
    }

    /// <summary>
    /// Event raised when a recording is completed.
    /// </summary>
    public event EventHandler<TimeStudyRecording>? RecordingCompleted;

    public RecordingControl(RecordingService recordingService, int buttonSize)
    {
        ArgumentNullException.ThrowIfNull(recordingService);
        _recordingService = recordingService;
        _buttonSize = Math.Max(buttonSize, 60);

        InitializeComponent();
        ApplyLocalization();
        SetupTimer();
        SetupEventHandlers();
        SetupControlButtons();
        UpdateDisplay();
    }

    private void ApplyLocalization()
    {
        _btnStart.Text = Resources.BtnStartRecording;
        _btnStop.Text = Resources.BtnStopRecording;
        _btnPause.Text = Resources.BtnPause;

        _lblProgressiveTimeCaption.Text = Resources.LblProgressiveTime;
        _lblCurrentStepCaption.Text = Resources.LblCurrentStep;
        _lblEntriesCaption.Text = Resources.LblEntries;

        _btnStart.AccessibleName = Resources.AccStartRecording;
        _btnStart.AccessibleDescription = Resources.AccStartRecording;
        _btnStop.AccessibleName = Resources.AccStopRecording;
        _btnStop.AccessibleDescription = Resources.AccStopRecording;
        _btnPause.AccessibleName = Resources.AccPause;
        _btnPause.AccessibleDescription = Resources.AccPause;
    }

    private void SetupTimer()
    {
        _updateTimer = new System.Windows.Forms.Timer
        {
            Interval = 100 // 100ms for smooth time display
        };
        _updateTimer.Tick += UpdateTimer_Tick;
    }

    private void SetupEventHandlers()
    {
        _recordingService.CurrentStepChanged += RecordingService_CurrentStepChanged;
        _recordingService.RecordingStarted += RecordingService_RecordingStarted;
        _recordingService.RecordingStopped += RecordingService_RecordingStopped;
    }

    private void SetupControlButtons()
    {
        int iconSize = 24;
        Color foreColor = SystemColors.ControlText;

        _btnStart.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Play, iconSize, foreColor);
        _btnStart.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnStart.MinimumSize = new Size(_buttonSize, _buttonSize);

        _btnStop.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Stop, iconSize, foreColor);
        _btnStop.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnStop.MinimumSize = new Size(_buttonSize, _buttonSize);

        _btnPause.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Pause, iconSize, foreColor);
        _btnPause.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnPause.MinimumSize = new Size(_buttonSize, _buttonSize);
    }

    private void BuildProcessStepButtons()
    {
        // Clear existing buttons
        foreach (Button btn in _stepButtons.Values)
        {
            btn.Dispose();
        }

        _stepButtons.Clear();
        _flpSteps.Controls.Clear();

        if (_definition is null)
        {
            return;
        }

        foreach (ProcessStepDefinition step in _definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            Button btn = CreateStepButton(step);
            _stepButtons[step.OrderNumber] = btn;
            _flpSteps.Controls.Add(btn);
        }
    }

    private Button CreateStepButton(ProcessStepDefinition step)
    {
        Button btn = new()
        {
            Size = new Size(_buttonSize + 40, _buttonSize),
            MinimumSize = new Size(_buttonSize, _buttonSize),
            Text = $"{step.OrderNumber}\n{TruncateText(step.Description, 15)}",
            Tag = step,
            Font = new Font(Font.FontFamily, 10f, FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleCenter,
            Enabled = false,
            Margin = new Padding(4),
            FlatStyle = FlatStyle.Flat
        };

        btn.FlatAppearance.BorderSize = 2;

        if (step.IsDefaultStep)
        {
            btn.BackColor = Color.FromArgb(255, 245, 220); // Light yellow for default step
        }

        btn.Click += StepButton_Click;

        btn.AccessibleName = $"{step.OrderNumber} - {step.Description}";
        btn.AccessibleDescription = Resources.AccProcessStep;

        return btn;
    }

    private static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
        {
            return text;
        }

        return text[..(maxLength - 3)] + "...";
    }

    private void UpdateDisplay()
    {
        bool isRecording = _recordingService.IsRecording;

        _btnStart.Enabled = _definition is not null && !isRecording;
        _btnStop.Enabled = isRecording;
        _btnPause.Enabled = isRecording;

        foreach (Button btn in _stepButtons.Values)
        {
            btn.Enabled = isRecording;
        }

        if (isRecording)
        {
            TimeSpan elapsed = _recordingService.ElapsedTime;
            _lblProgressiveTime.Text = FormatTimeSpan(elapsed);

            ProcessStepDefinition? currentStep = _recordingService.CurrentStep;
            _lblCurrentStep.Text = currentStep is not null
                ? $"{currentStep.OrderNumber} - {currentStep.Description}"
                : "-";

            _lblEntries.Text = _recordingService.ActiveRecording?.Entries.Count.ToString() ?? "0";

            // Highlight current step button
            UpdateButtonHighlight();
        }
        else
        {
            _lblProgressiveTime.Text = "00:00:00.0";
            _lblCurrentStep.Text = "-";
            _lblEntries.Text = "0";
        }
    }

    private void UpdateButtonHighlight()
    {
        ProcessStepDefinition? currentStep = _recordingService.CurrentStep;

        foreach (KeyValuePair<int, Button> kvp in _stepButtons)
        {
            Button btn = kvp.Value;
            ProcessStepDefinition? step = btn.Tag as ProcessStepDefinition;

            if (step?.OrderNumber == currentStep?.OrderNumber)
            {
                btn.BackColor = Color.LightGreen;
                btn.FlatAppearance.BorderColor = Color.Green;
            }
            else if (step?.IsDefaultStep == true)
            {
                btn.BackColor = Color.FromArgb(255, 245, 220);
                btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
            }
            else
            {
                btn.BackColor = SystemColors.Control;
                btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
            }
        }
    }

    private static string FormatTimeSpan(TimeSpan ts)
    {
        return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds / 100}";
    }

    private void UpdateTimer_Tick(object? sender, EventArgs e)
    {
        if (_recordingService.IsRecording)
        {
            TimeSpan elapsed = _recordingService.ElapsedTime;
            _lblProgressiveTime.Text = FormatTimeSpan(elapsed);
        }
    }

    private void RecordingService_CurrentStepChanged(object? sender, ProcessStepDefinition? step)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_CurrentStepChanged(sender, step));
            return;
        }

        UpdateButtonHighlight();

        if (step is not null)
        {
            _lblCurrentStep.Text = $"{step.OrderNumber} - {step.Description}";
        }

        _lblEntries.Text = _recordingService.ActiveRecording?.Entries.Count.ToString() ?? "0";
    }

    private void RecordingService_RecordingStarted(object? sender, TimeStudyRecording recording)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_RecordingStarted(sender, recording));
            return;
        }

        _updateTimer?.Start();
        UpdateDisplay();
    }

    private void RecordingService_RecordingStopped(object? sender, TimeStudyRecording recording)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_RecordingStopped(sender, recording));
            return;
        }

        _updateTimer?.Stop();
        UpdateDisplay();
        RecordingCompleted?.Invoke(this, recording);
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        if (_definition is null)
        {
            return;
        }

        try
        {
            _recordingService.StartRecording(_definition);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show(
            this,
            Resources.MsgConfirmStop,
            Resources.TitleConfirm,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                _recordingService.StopRecording();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void BtnPause_Click(object? sender, EventArgs e)
    {
        try
        {
            _recordingService.Pause();
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void StepButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not ProcessStepDefinition step)
        {
            return;
        }

        try
        {
            _recordingService.SwitchToStep(step);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _updateTimer?.Stop();
            _updateTimer?.Dispose();

            _recordingService.CurrentStepChanged -= RecordingService_CurrentStepChanged;
            _recordingService.RecordingStarted -= RecordingService_RecordingStarted;
            _recordingService.RecordingStopped -= RecordingService_RecordingStopped;

            foreach (Button btn in _stepButtons.Values)
            {
                btn.Dispose();
            }

            _stepButtons.Clear();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}
