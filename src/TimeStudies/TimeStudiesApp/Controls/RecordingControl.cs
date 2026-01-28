using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for conducting time study recordings with touch-friendly interface.
/// </summary>
public partial class RecordingControl : UserControl
{
    private readonly RecordingService _recordingService;
    private readonly SettingsService _settingsService;
    private TimeStudyDefinition? _definition;
    private readonly Dictionary<int, Button> _stepButtons = [];
    private int _activeStepOrderNumber;

    public RecordingControl(RecordingService recordingService, SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(recordingService);
        ArgumentNullException.ThrowIfNull(settingsService);

        _recordingService = recordingService;
        _settingsService = settingsService;

        InitializeComponent();
        ApplyLocalization();

        _recordingService.RecordingStateChanged += RecordingService_RecordingStateChanged;
        _recordingService.EntryRecorded += RecordingService_EntryRecorded;
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
            BuildStepButtons();
            UpdateUIState();
        }
    }

    /// <summary>
    /// Gets whether a recording is in progress.
    /// </summary>
    public bool IsRecording => _recordingService.IsRecording;

    /// <summary>
    /// Event raised when recording state changes.
    /// </summary>
    public event EventHandler? RecordingStateChanged;

    private void ApplyLocalization()
    {
        _btnStartRecording.Text = Resources.BtnStartRecording;
        _btnStopRecording.Text = Resources.BtnStopRecording;
        _btnPause.Text = Resources.BtnPause;

        _lblProgressiveTimeCaption.Text = Resources.LblProgressiveTime + ":";
        _lblCurrentStepCaption.Text = Resources.LblCurrentStep + ":";
        _lblEntryCountCaption.Text = Resources.LblEntryCount + ":";

        _grpProcessSteps.Text = Resources.LblProcessStep + "s";
        _grpEntries.Text = Resources.LblEntryCount;

        _colSeq.HeaderText = "#";
        _colOrderNumber.HeaderText = Resources.LblOrderNumber;
        _colDescription.HeaderText = Resources.LblDescription;
        _colTimestamp.HeaderText = Resources.CsvHeaderTimestamp;
        _colElapsed.HeaderText = Resources.LblProgressiveTime;
        _colDuration.HeaderText = Resources.CsvHeaderDurationSec;
    }

    private void BuildStepButtons()
    {
        // Clear existing buttons
        _flowLayoutSteps.Controls.Clear();
        _stepButtons.Clear();

        if (_definition is null)
        {
            return;
        }

        int buttonSize = _settingsService.Settings.ButtonSize;

        foreach (ProcessStepDefinition step in _definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            Button button = new()
            {
                Size = new Size(buttonSize + 40, buttonSize),
                MinimumSize = new Size(buttonSize + 40, buttonSize),
                Margin = new Padding(3),
                Text = $"{step.OrderNumber}\n{TruncateText(step.Description, 12)}",
                Tag = step.OrderNumber,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false,
                BackColor = step.IsDefaultStep ? Color.LightBlue : SystemColors.Control
            };

            button.FlatAppearance.BorderSize = 2;
            button.Click += StepButton_Click;

            if (step.IsDefaultStep)
            {
                button.AccessibleDescription = Resources.LblDefaultStep;
            }

            button.AccessibleName = $"{step.OrderNumber}: {step.Description}";

            _flowLayoutSteps.Controls.Add(button);
            _stepButtons[step.OrderNumber] = button;
        }
    }

    private static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        if (text.Length <= maxLength)
        {
            return text;
        }

        return text[..(maxLength - 1)] + "…";
    }

    private void UpdateUIState()
    {
        bool hasDefinition = _definition is not null;
        bool isRecording = _recordingService.IsRecording;

        _btnStartRecording.Enabled = hasDefinition && !isRecording;
        _btnStopRecording.Enabled = isRecording;
        _btnPause.Enabled = isRecording;

        foreach (Button button in _stepButtons.Values)
        {
            button.Enabled = isRecording;
        }

        if (!isRecording)
        {
            _lblProgressiveTime.Text = "00:00:00";
            _lblCurrentStep.Text = "-";
            _lblEntryCount.Text = "0";
        }
    }

    private void UpdateActiveStepHighlight(int activeOrderNumber)
    {
        foreach (KeyValuePair<int, Button> kvp in _stepButtons)
        {
            ProcessStepDefinition? step = _definition?.ProcessSteps.FirstOrDefault(s => s.OrderNumber == kvp.Key);
            bool isDefault = step?.IsDefaultStep ?? false;
            bool isActive = kvp.Key == activeOrderNumber;

            if (isActive)
            {
                kvp.Value.BackColor = Color.FromArgb(144, 238, 144); // Light green
                kvp.Value.FlatAppearance.BorderColor = Color.DarkGreen;
            }
            else if (isDefault)
            {
                kvp.Value.BackColor = Color.LightBlue;
                kvp.Value.FlatAppearance.BorderColor = SystemColors.ControlDark;
            }
            else
            {
                kvp.Value.BackColor = SystemColors.Control;
                kvp.Value.FlatAppearance.BorderColor = SystemColors.ControlDark;
            }
        }

        _activeStepOrderNumber = activeOrderNumber;
    }

    private void AddEntryToGrid(TimeEntry entry)
    {
        int rowIndex = _dataGridEntries.Rows.Add();
        DataGridViewRow row = _dataGridEntries.Rows[rowIndex];

        row.Cells[_colSeq.Index].Value = entry.SequenceNumber;
        row.Cells[_colOrderNumber.Index].Value = entry.ProcessStepOrderNumber;
        row.Cells[_colDescription.Index].Value = entry.ProcessStepDescription;
        row.Cells[_colTimestamp.Index].Value = entry.Timestamp.ToString("HH:mm:ss.fff");
        row.Cells[_colElapsed.Index].Value = FormatTimeSpan(entry.ElapsedFromStart);
        row.Cells[_colDuration.Index].Value = FormatTimeSpan(entry.Duration);

        // Scroll to bottom
        _dataGridEntries.FirstDisplayedScrollingRowIndex = rowIndex;
    }

    private void UpdateLastEntryDuration()
    {
        if (_dataGridEntries.Rows.Count == 0)
        {
            return;
        }

        TimeStudyRecording? recording = _recordingService.CurrentRecording;

        if (recording is null || recording.Entries.Count == 0)
        {
            return;
        }

        // Update the second-to-last row's duration (the previous entry)
        if (_dataGridEntries.Rows.Count >= 2)
        {
            int previousRowIndex = _dataGridEntries.Rows.Count - 2;
            TimeEntry previousEntry = recording.Entries[^2];
            _dataGridEntries.Rows[previousRowIndex].Cells[_colDuration.Index].Value =
                FormatTimeSpan(previousEntry.Duration);
        }
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

    private void BtnStartRecording_Click(object? sender, EventArgs e)
    {
        if (_definition is null)
        {
            return;
        }

        _dataGridEntries.Rows.Clear();
        _recordingService.StartRecording(_definition);
        _timerUpdate.Start();
        UpdateUIState();

        // Highlight the default step
        ProcessStepDefinition? defaultStep = _definition.GetDefaultStep();

        if (defaultStep is not null)
        {
            UpdateActiveStepHighlight(defaultStep.OrderNumber);
            _lblCurrentStep.Text = $"{defaultStep.OrderNumber}: {defaultStep.Description}";
        }
    }

    private async void BtnStopRecording_Click(object? sender, EventArgs e)
    {
        if (!_recordingService.IsRecording)
        {
            return;
        }

        if (_settingsService.Settings.ConfirmBeforeClosingRecording)
        {
            DialogResult result = MessageBox.Show(
                this,
                Resources.MsgConfirmStop,
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }
        }

        try
        {
            _timerUpdate.Stop();
            await _recordingService.StopRecordingAsync();
            UpdateUIState();
            UpdateLastEntryDuration();

            // Update final entry in grid
            if (_dataGridEntries.Rows.Count > 0)
            {
                TimeStudyRecording? recording = _recordingService.CurrentRecording;

                if (recording?.Entries.Count > 0)
                {
                    int lastRowIndex = _dataGridEntries.Rows.Count - 1;
                    TimeEntry lastEntry = recording.Entries[^1];
                    _dataGridEntries.Rows[lastRowIndex].Cells[_colDuration.Index].Value =
                        FormatTimeSpan(lastEntry.Duration);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Error stopping recording: {ex.Message}",
                Resources.TitleError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void BtnPause_Click(object? sender, EventArgs e)
    {
        TimeEntry? entry = _recordingService.Pause();

        if (entry is not null)
        {
            UpdateActiveStepHighlight(entry.ProcessStepOrderNumber);
            _lblCurrentStep.Text = $"{entry.ProcessStepOrderNumber}: {entry.ProcessStepDescription}";
        }
    }

    private void StepButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not int orderNumber)
        {
            return;
        }

        TimeEntry? entry = _recordingService.RecordStepChange(orderNumber);

        if (entry is not null)
        {
            UpdateActiveStepHighlight(orderNumber);
            _lblCurrentStep.Text = $"{entry.ProcessStepOrderNumber}: {entry.ProcessStepDescription}";
        }
    }

    private void TimerUpdate_Tick(object? sender, EventArgs e)
    {
        if (!_recordingService.IsRecording)
        {
            return;
        }

        TimeSpan elapsed = _recordingService.ElapsedTime;
        _lblProgressiveTime.Text = FormatTimeSpan(elapsed);

        int entryCount = _recordingService.CurrentRecording?.Entries.Count ?? 0;
        _lblEntryCount.Text = entryCount.ToString();
    }

    private void RecordingService_RecordingStateChanged(object? sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_RecordingStateChanged(sender, e));
            return;
        }

        UpdateUIState();
        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void RecordingService_EntryRecorded(object? sender, TimeEntry entry)
    {
        if (InvokeRequired)
        {
            Invoke(() => RecordingService_EntryRecorded(sender, entry));
            return;
        }

        UpdateLastEntryDuration();
        AddEntryToGrid(entry);
    }

    /// <summary>
    /// Stops the recording if in progress.
    /// </summary>
    public async Task<TimeStudyRecording?> StopRecordingAsync()
    {
        if (_recordingService.IsRecording)
        {
            _timerUpdate.Stop();
            return await _recordingService.StopRecordingAsync();
        }

        return null;
    }
}
