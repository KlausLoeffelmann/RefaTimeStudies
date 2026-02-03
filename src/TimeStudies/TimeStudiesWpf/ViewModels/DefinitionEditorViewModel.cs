using System.Collections.ObjectModel;
using System.Windows.Input;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.ViewModels;

/// <summary>
/// ViewModel for the process step edit row in the definition editor.
/// </summary>
public class ProcessStepViewModel : ViewModelBase
{
    private readonly ProcessStepDefinition _model;
    private readonly Action _onChanged;

    public ProcessStepViewModel(ProcessStepDefinition model, Action onChanged)
    {
        _model = model;
        _onChanged = onChanged;
    }

    public ProcessStepDefinition Model => _model;

    public int OrderNumber
    {
        get => _model.OrderNumber;
        set { _model.OrderNumber = value; OnPropertyChanged(); _onChanged(); }
    }

    public string Description
    {
        get => _model.Description;
        set { _model.Description = value; OnPropertyChanged(); _onChanged(); }
    }

    public string ProductDescription
    {
        get => _model.ProductDescription;
        set { _model.ProductDescription = value; OnPropertyChanged(); _onChanged(); }
    }

    public DimensionType DimensionType
    {
        get => _model.DimensionType;
        set { _model.DimensionType = value; OnPropertyChanged(); _onChanged(); }
    }

    public string DimensionUnit
    {
        get => _model.DimensionUnit;
        set { _model.DimensionUnit = value; OnPropertyChanged(); _onChanged(); }
    }

    public decimal DefaultDimensionValue
    {
        get => _model.DefaultDimensionValue;
        set { _model.DefaultDimensionValue = value; OnPropertyChanged(); _onChanged(); }
    }

    public bool IsDefaultStep
    {
        get => _model.IsDefaultStep;
        set { _model.IsDefaultStep = value; OnPropertyChanged(); _onChanged(); }
    }
}

/// <summary>
/// ViewModel for the Definition Editor view.
/// </summary>
public class DefinitionEditorViewModel : ViewModelBase
{
    private readonly TimeStudyDefinition _definition;
    private ProcessStepViewModel? _selectedStep;

    public event Action? DefinitionChanged;
    public event Action? RequestStartRecording;

    public DefinitionEditorViewModel(TimeStudyDefinition definition)
    {
        _definition = definition;

        // Initialize process steps collection
        ProcessSteps = new ObservableCollection<ProcessStepViewModel>(
            definition.ProcessSteps.Select(ps => new ProcessStepViewModel(ps, OnStepChanged)));

        // Initialize commands
        AddStepCommand = new RelayCommand(ExecuteAddStep, () => !IsLocked);
        RemoveStepCommand = new RelayCommand(ExecuteRemoveStep, () => !IsLocked && SelectedStep != null);
        SetDefaultStepCommand = new RelayCommand(ExecuteSetDefaultStep, () => !IsLocked && SelectedStep != null);
        CreateCopyCommand = new RelayCommand(ExecuteCreateCopy, () => IsLocked);
        StartRecordingCommand = new RelayCommand(ExecuteStartRecording, CanStartRecording);

        // Available dimension types for combo box
        DimensionTypes = Enum.GetValues<DimensionType>();
    }

    #region Properties

    public string Name
    {
        get => _definition.Name;
        set
        {
            _definition.Name = value;
            OnPropertyChanged();
            DefinitionChanged?.Invoke();
        }
    }

    public string Description
    {
        get => _definition.Description;
        set
        {
            _definition.Description = value;
            OnPropertyChanged();
            DefinitionChanged?.Invoke();
        }
    }

    public bool IsLocked => _definition.IsLocked;

    public ObservableCollection<ProcessStepViewModel> ProcessSteps { get; }

    public ProcessStepViewModel? SelectedStep
    {
        get => _selectedStep;
        set => SetProperty(ref _selectedStep, value);
    }

    public DimensionType[] DimensionTypes { get; }

    public TimeStudyDefinition Definition => _definition;

    #endregion

    #region Commands

    public ICommand AddStepCommand { get; }
    public ICommand RemoveStepCommand { get; }
    public ICommand SetDefaultStepCommand { get; }
    public ICommand CreateCopyCommand { get; }
    public ICommand StartRecordingCommand { get; }

    #endregion

    #region Command Implementations

    private void ExecuteAddStep()
    {
        // Find next available order number
        var maxOrder = ProcessSteps.Any() ? ProcessSteps.Max(ps => ps.OrderNumber) : 0;
        var newStep = new ProcessStepDefinition
        {
            OrderNumber = maxOrder + 1,
            Description = "New Step",
            DimensionType = DimensionType.Count,
            DimensionUnit = "pcs",
            DefaultDimensionValue = 1
        };

        _definition.ProcessSteps.Add(newStep);
        var vm = new ProcessStepViewModel(newStep, OnStepChanged);
        ProcessSteps.Add(vm);
        SelectedStep = vm;
        DefinitionChanged?.Invoke();
    }

    private void ExecuteRemoveStep()
    {
        if (SelectedStep == null)
            return;

        var step = SelectedStep;
        _definition.ProcessSteps.Remove(step.Model);
        ProcessSteps.Remove(step);

        // If we removed the default step, clear the default
        if (step.IsDefaultStep && _definition.ProcessSteps.Count > 0)
        {
            var firstStep = ProcessSteps.First();
            firstStep.IsDefaultStep = true;
            _definition.DefaultProcessStepOrderNumber = firstStep.OrderNumber;
        }

        SelectedStep = ProcessSteps.FirstOrDefault();
        DefinitionChanged?.Invoke();
    }

    private void ExecuteSetDefaultStep()
    {
        if (SelectedStep == null)
            return;

        // Clear previous default
        foreach (var step in ProcessSteps)
        {
            step.IsDefaultStep = false;
        }

        // Set new default
        SelectedStep.IsDefaultStep = true;
        _definition.DefaultProcessStepOrderNumber = SelectedStep.OrderNumber;
        DefinitionChanged?.Invoke();
    }

    private async void ExecuteCreateCopy()
    {
        var copy = App.DefinitionService.CreateCopy(_definition);
        await App.DefinitionService.SaveAsync(copy);

        // Reload the editor with the copy
        // This would typically be handled by the MainWindowViewModel
    }

    private bool CanStartRecording()
    {
        return ProcessSteps.Count > 0 && ProcessSteps.Any(ps => ps.IsDefaultStep);
    }

    private void ExecuteStartRecording()
    {
        RequestStartRecording?.Invoke();
    }

    #endregion

    private void OnStepChanged()
    {
        DefinitionChanged?.Invoke();
    }
}
