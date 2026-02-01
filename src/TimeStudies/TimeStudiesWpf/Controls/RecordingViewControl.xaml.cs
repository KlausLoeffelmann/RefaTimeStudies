using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfUserControl = System.Windows.Controls.UserControl;
using WpfButton = System.Windows.Controls.Button;
using WpfMessageBox = System.Windows.MessageBox;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf.Controls;

/// <summary>
/// Interaction logic for RecordingViewControl.xaml
/// </summary>
public partial class RecordingViewControl : WpfUserControl, INotifyPropertyChanged
{
    private readonly RecordingService _recordingService;
    private readonly DispatcherTimer _timer;
    private TimeStudyDefinition? _definition;
    private ProcessStepDefinition? _currentProcessStep;

    public RecordingViewControl()
    {
        InitializeComponent();
        _recordingService = App.RecordingService;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _timer.Tick += Timer_Tick;
    }

    #region Properties

    /// <summary>
    /// Gets or sets the time study definition for recording.
    /// </summary>
    public TimeStudyDefinition? Definition
    {
        get => _definition;
        set
        {
            if (_definition != value)
            {
                _definition = value;
                OnPropertyChanged(nameof(Definition));
                LoadDefinition();
            }
        }
    }

    /// <summary>
    /// Gets whether recording is currently in progress.
    /// </summary>
    public bool IsRecording => _recordingService.IsRecording;

    /// <summary>
    /// Gets the current process step during recording.
    /// </summary>
    public ProcessStepDefinition? CurrentProcessStep => _currentProcessStep;

    #endregion

    #region Event Handlers

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        SetLocalizedStrings();
        LoadDefinition();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        UpdateProgressiveTimeDisplay();
    }

    private void StartRecording_Click(object sender, RoutedEventArgs e)
    {
        if (_definition == null)
        {
            WpfMessageBox.Show(
                ResourceHelper.GetString("MsgSelectDefinition"),
                "Warning",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Warning);
            return;
        }

        try
        {
            _recordingService.StartRecording(_definition);
            _currentProcessStep = _definition.GetDefaultProcessStep();
            _timer.Start();
            UpdateUIState();
        }
        catch (Exception ex)
        {
            WpfMessageBox.Show(
                $"Failed to start recording: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
    }

    private void StopRecording_Click(object sender, RoutedEventArgs e)
    {
        if (!_recordingService.IsRecording)
        {
            return;
        }

        var result = WpfMessageBox.Show(
            ResourceHelper.GetString("MsgConfirmStop"),
            ResourceHelper.GetString("TitleRecording"),
            MessageBoxButton.YesNo,
            System.Windows.MessageBoxImage.Question);

        if (result == System.Windows.MessageBoxResult.Yes)
        {
            try
            {
                var recording = _recordingService.StopRecording();
                _timer.Stop();
                _currentProcessStep = null;
                UpdateUIState();

                WpfMessageBox.Show(
                    ResourceHelper.GetString("MsgExportSuccessful") + "\n" +
                    $"ID: {recording.Id}",
                    "Success",
                    MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);

                // Notify parent to show results
                RecordingCompleted?.Invoke(this, recording);
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(
                    $"Failed to stop recording: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }
    }

    private void Pause_Click(object sender, RoutedEventArgs e)
    {
        if (_definition == null || !_recordingService.IsRecording)
        {
            return;
        }

        try
        {
            var defaultStep = _definition.GetDefaultProcessStep();
            if (defaultStep != null)
            {
                _recordingService.PauseRecording(_definition);
                _currentProcessStep = defaultStep;
                UpdateCurrentStepDisplay();
                HighlightProcessStep(defaultStep.OrderNumber);
            }
        }
        catch (Exception ex)
        {
            WpfMessageBox.Show(
                $"Failed to pause recording: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
    }

    private void ProcessStepButton_Click(object sender, RoutedEventArgs e)
    {
        if (_definition == null || !_recordingService.IsRecording)
        {
            return;
        }

        if (sender is WpfButton button && button.Tag is ProcessStepDefinition processStep)
        {
            try
            {
                _recordingService.AddTimeEntry(processStep, _definition);
                _currentProcessStep = processStep;
                UpdateCurrentStepDisplay();
                HighlightProcessStep(processStep.OrderNumber);
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(
                    $"Failed to add time entry: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }
    }

    #endregion

    #region Private Methods

    private void SetLocalizedStrings()
    {
        HeaderLabel.Content = ResourceHelper.GetString("ViewRecording");
        DefinitionInfoLabel.Content = ResourceHelper.GetString("DefinitionName");
        ProcessStepsLabel.Content = ResourceHelper.GetString("ProcessStep");
        StatusLabel.Content = ResourceHelper.GetString("Status");
        ProgressiveTimeLabel.Content = ResourceHelper.GetString("ProgressiveTime");
        CurrentStepLabel.Content = ResourceHelper.GetString("CurrentStep");
        EntryCountLabel.Content = ResourceHelper.GetString("EntryCount");
        StartRecordingButton.Content = ResourceHelper.GetString("BtnStartRecording");
        StopRecordingButton.Content = ResourceHelper.GetString("BtnStopRecording");
        PauseButton.Content = ResourceHelper.GetString("BtnPause");
    }

    private void LoadDefinition()
    {
        if (_definition == null) return;

        DefinitionNameTextBlock.Text = $"{ResourceHelper.GetString("DefinitionName")}: {_definition.Name}";
        DefinitionDescriptionTextBlock.Text = _definition.Description;
        
        CreateProcessStepButtons();
        UpdateUIState();
    }

    private void CreateProcessStepButtons()
    {
        if (_definition == null) return;

        ProcessStepButtonsPanel.Items.Clear();

        foreach (var step in _definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            var button = new WpfButton
            {
                Tag = step,
                MinHeight = 80,
                MinWidth = 100,
                Margin = new Thickness(8),
                Style = (Style)FindResource("ProcessStepButtonStyle"),
                Content = step.Description
            };

            button.Click += ProcessStepButton_Click;
            ProcessStepButtonsPanel.Items.Add(button);
        }
    }

    private void UpdateProgressiveTimeDisplay()
    {
        var elapsed = _recordingService.ElapsedTime;
        ProgressiveTimeTextBlock.Text = elapsed.ToString(@"hh\:mm\:ss\.ff");

        if (_recordingService.CurrentRecording != null)
        {
            EntryCountTextBlock.Text = _recordingService.CurrentRecording.Entries.Count.ToString();
        }
    }

    private void UpdateCurrentStepDisplay()
    {
        if (_currentProcessStep != null)
        {
            CurrentStepTextBlock.Text = $"{_currentProcessStep.OrderNumber}: {_currentProcessStep.Description}";
        }
        else
        {
            CurrentStepTextBlock.Text = "-";
        }
    }

    private void HighlightProcessStep(int orderNumber)
    {
        foreach (var item in ProcessStepButtonsPanel.Items)
        {
            if (item is WpfButton button && button.Tag is ProcessStepDefinition step)
            {
                if (step.OrderNumber == orderNumber)
                {
                    button.Style = (Style)FindResource("ActiveProcessStepButtonStyle");
                }
                else
                {
                    button.Style = (Style)FindResource("ProcessStepButtonStyle");
                }
            }
        }
    }

    private void UpdateUIState()
    {
        StartRecordingButton.IsEnabled = !IsRecording && _definition != null;
        StopRecordingButton.IsEnabled = IsRecording;
        PauseButton.IsEnabled = IsRecording;

        RecordingIndicator.Visibility = IsRecording ? Visibility.Visible : Visibility.Collapsed;

        // Enable/disable process step buttons
        foreach (var item in ProcessStepButtonsPanel.Items)
        {
            if (item is WpfButton button)
            {
                button.IsEnabled = IsRecording;
            }
        }
    }

    #endregion

    #region Events

    public event EventHandler<TimeStudyRecording>? RecordingCompleted;

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}