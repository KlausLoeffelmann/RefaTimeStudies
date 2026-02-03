using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf.ViewModels;

/// <summary>
/// ViewModel for the main window, handling navigation and top-level commands.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private object? _currentView;
    private TimeStudyDefinition? _currentDefinition;
    private TimeStudyRecording? _currentRecording;
    private string _statusMessage = "Ready";
    private bool _isRecording;
    private bool _isDirty;

    public MainWindowViewModel()
    {
        // Initialize commands
        NewDefinitionCommand = new RelayCommand(ExecuteNewDefinition);
        OpenDefinitionCommand = new RelayCommand(ExecuteOpenDefinition);
        SaveDefinitionCommand = new RelayCommand(ExecuteSaveDefinition, () => _currentDefinition != null && _isDirty);
        SaveDefinitionAsCommand = new RelayCommand(ExecuteSaveDefinitionAs, () => _currentDefinition != null);
        ExportCsvCommand = new RelayCommand(ExecuteExportCsv, () => _currentRecording != null && _currentRecording.IsCompleted);
        ExitCommand = new RelayCommand(ExecuteExit);
        SettingsCommand = new RelayCommand(ExecuteSettings);
        AboutCommand = new RelayCommand(ExecuteAbout);
        StartRecordingCommand = new RelayCommand(ExecuteStartRecording, CanStartRecording);
        StopRecordingCommand = new RelayCommand(ExecuteStopRecording, () => _isRecording);
        ViewResultsCommand = new RelayCommand(ExecuteViewResults, () => _currentDefinition != null);

        // Show welcome view or create new definition
        ExecuteNewDefinition();
    }

    #region Properties

    public object? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public string WindowTitle => _currentDefinition != null
        ? $"REFA Time Study - {_currentDefinition.Name}{(_isDirty ? " *" : "")}"
        : "REFA Time Study";

    public string CurrentDefinitionName => _currentDefinition?.Name ?? "(No definition loaded)";

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public bool IsRecording
    {
        get => _isRecording;
        set
        {
            if (SetProperty(ref _isRecording, value))
            {
                OnPropertyChanged(nameof(WindowTitle));
            }
        }
    }

    public bool IsDirty
    {
        get => _isDirty;
        set
        {
            if (SetProperty(ref _isDirty, value))
            {
                OnPropertyChanged(nameof(WindowTitle));
            }
        }
    }

    #endregion

    #region Commands

    public ICommand NewDefinitionCommand { get; }
    public ICommand OpenDefinitionCommand { get; }
    public ICommand SaveDefinitionCommand { get; }
    public ICommand SaveDefinitionAsCommand { get; }
    public ICommand ExportCsvCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand StartRecordingCommand { get; }
    public ICommand StopRecordingCommand { get; }
    public ICommand ViewResultsCommand { get; }

    #endregion

    #region Command Implementations

    private void ExecuteNewDefinition()
    {
        if (_isDirty && !ConfirmDiscardChanges())
            return;

        _currentDefinition = new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = "New Time Study",
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            ProcessSteps = new List<ProcessStepDefinition>
            {
                new() { OrderNumber = 40, Description = "Default Step", IsDefaultStep = true }
            },
            DefaultProcessStepOrderNumber = 40
        };

        var editorVm = new DefinitionEditorViewModel(_currentDefinition);
        editorVm.DefinitionChanged += OnDefinitionChanged;
        editorVm.RequestStartRecording += OnRequestStartRecording;
        CurrentView = editorVm;

        IsDirty = true;
        StatusMessage = "New definition created";
        OnPropertyChanged(nameof(WindowTitle));
        OnPropertyChanged(nameof(CurrentDefinitionName));
    }

    private async void ExecuteOpenDefinition()
    {
        if (_isDirty && !ConfirmDiscardChanges())
            return;

        var definitions = await App.DefinitionService.GetAllAsync();
        if (definitions.Count == 0)
        {
            System.Windows.MessageBox.Show("No definitions found. Create a new definition first.",
                "No Definitions", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return;
        }

        var dialog = new Dialogs.SelectDefinitionDialog(definitions);
        if (dialog.ShowDialog() == true && dialog.SelectedDefinition != null)
        {
            _currentDefinition = dialog.SelectedDefinition;

            var editorVm = new DefinitionEditorViewModel(_currentDefinition);
            editorVm.DefinitionChanged += OnDefinitionChanged;
            editorVm.RequestStartRecording += OnRequestStartRecording;
            CurrentView = editorVm;

            IsDirty = false;
            StatusMessage = $"Loaded: {_currentDefinition.Name}";
            OnPropertyChanged(nameof(WindowTitle));
            OnPropertyChanged(nameof(CurrentDefinitionName));
        }
    }

    private async void ExecuteSaveDefinition()
    {
        if (_currentDefinition == null)
            return;

        try
        {
            await App.DefinitionService.SaveAsync(_currentDefinition);
            IsDirty = false;
            StatusMessage = $"Saved: {_currentDefinition.Name}";
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Error saving definition: {ex.Message}",
                "Save Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    private async void ExecuteSaveDefinitionAs()
    {
        if (_currentDefinition == null)
            return;

        // Create a copy with a new ID
        _currentDefinition = App.DefinitionService.CreateCopy(_currentDefinition);
        _currentDefinition.Name = _currentDefinition.Name.Replace(" (Copy)", "");

        await App.DefinitionService.SaveAsync(_currentDefinition);
        IsDirty = false;
        StatusMessage = $"Saved as: {_currentDefinition.Name}";
        OnPropertyChanged(nameof(WindowTitle));
        OnPropertyChanged(nameof(CurrentDefinitionName));
    }

    private async void ExecuteExportCsv()
    {
        if (_currentRecording == null || !_currentRecording.IsCompleted)
            return;

        var saveDialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
            DefaultExt = ".csv",
            FileName = $"{_currentRecording.DefinitionName}_{_currentRecording.StartedAt:yyyy-MM-dd_HHmmss}.csv"
        };

        if (saveDialog.ShowDialog() == true)
        {
            try
            {
                await App.CsvExportService.ExportAsync(_currentRecording, saveDialog.FileName);
                StatusMessage = $"Exported to: {saveDialog.FileName}";
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error exporting CSV: {ex.Message}",
                    "Export Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }

    private void ExecuteExit()
    {
        if (_isDirty && !ConfirmDiscardChanges())
            return;

        System.Windows.Application.Current.Shutdown();
    }

    private void ExecuteSettings()
    {
        var dialog = new Dialogs.SettingsDialog();
        dialog.ShowDialog();
    }

    private void ExecuteAbout()
    {
        var dialog = new Dialogs.AboutDialog();
        dialog.ShowDialog();
    }

    private bool CanStartRecording()
    {
        return _currentDefinition != null
            && !_isRecording
            && _currentDefinition.ProcessSteps.Count > 0
            && _currentDefinition.ProcessSteps.Any(ps => ps.IsDefaultStep);
    }

    private void ExecuteStartRecording()
    {
        if (_currentDefinition == null)
            return;

        // Save definition if dirty before starting recording
        if (_isDirty)
        {
            ExecuteSaveDefinition();
        }

        var recordingVm = new RecordingViewModel(_currentDefinition);
        recordingVm.RecordingCompleted += OnRecordingCompleted;
        recordingVm.RecordingStarted += () => { IsRecording = true; StatusMessage = "Recording..."; };
        recordingVm.RecordingStopped += () => { IsRecording = false; };
        CurrentView = recordingVm;

        StatusMessage = "Ready to start recording";
    }

    private void ExecuteStopRecording()
    {
        if (CurrentView is RecordingViewModel recordingVm)
        {
            recordingVm.StopRecording();
        }
    }

    private void ExecuteViewResults()
    {
        if (_currentDefinition == null)
            return;

        var resultsVm = new ResultsViewModel(_currentDefinition.Id);
        resultsVm.RequestExport += (recording) =>
        {
            _currentRecording = recording;
            ExecuteExportCsv();
        };
        CurrentView = resultsVm;
        StatusMessage = "Viewing results";
    }

    #endregion

    #region Event Handlers

    private void OnDefinitionChanged()
    {
        IsDirty = true;
        OnPropertyChanged(nameof(WindowTitle));
    }

    private void OnRequestStartRecording()
    {
        if (CanStartRecording())
        {
            ExecuteStartRecording();
        }
    }

    private async void OnRecordingCompleted(TimeStudyRecording recording)
    {
        _currentRecording = recording;
        IsRecording = false;

        // Lock the definition if not already locked
        if (_currentDefinition != null && !_currentDefinition.IsLocked)
        {
            _currentDefinition.IsLocked = true;
            await App.DefinitionService.SaveAsync(_currentDefinition);
        }

        StatusMessage = $"Recording completed - {recording.Entries.Count} entries";

        // Show results view
        var resultsVm = new ResultsViewModel(_currentDefinition!.Id, recording);
        resultsVm.RequestExport += (rec) =>
        {
            _currentRecording = rec;
            ExecuteExportCsv();
        };
        CurrentView = resultsVm;
    }

    #endregion

    #region Helpers

    private bool ConfirmDiscardChanges()
    {
        var result = System.Windows.MessageBox.Show(
            "You have unsaved changes. Do you want to discard them?",
            "Unsaved Changes",
            System.Windows.MessageBoxButton.YesNo,
            System.Windows.MessageBoxImage.Warning);

        return result == System.Windows.MessageBoxResult.Yes;
    }

    #endregion
}
