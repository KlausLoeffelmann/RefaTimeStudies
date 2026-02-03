using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.ViewModels;

/// <summary>
/// ViewModel for the definition editor.
/// </summary>
public partial class DefinitionEditorViewModel : ObservableObject
{
    [ObservableProperty]
    private TimeStudyDefinition? _definition;

    [ObservableProperty]
    private BindingList<ProcessStepDefinition> _processSteps = new();

    [ObservableProperty]
    private ProcessStepDefinition? _selectedStep;

    [ObservableProperty]
    private bool _isLocked;

    /// <summary>
    /// Event raised when the definition is modified.
    /// </summary>
    public event EventHandler? DefinitionModified;

    public DefinitionEditorViewModel()
    {
        ProcessSteps.ListChanged += OnProcessStepsListChanged;
    }

    private void OnProcessStepsListChanged(object? sender, ListChangedEventArgs e)
    {
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Loads a definition into the editor.
    /// </summary>
    public void LoadDefinition(TimeStudyDefinition? definition)
    {
        Definition = definition;

        ProcessSteps.RaiseListChangedEvents = false;
        ProcessSteps.Clear();

        if (definition != null)
        {
            IsLocked = definition.IsLocked;
            foreach (var step in definition.ProcessSteps)
            {
                ProcessSteps.Add(step);
            }
        }
        else
        {
            IsLocked = false;
        }

        ProcessSteps.RaiseListChangedEvents = true;
        ProcessSteps.ResetBindings();
    }

    /// <summary>
    /// Adds a new process step.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanModify))]
    public void AddStep()
    {
        int nextOrderNumber = ProcessSteps.Count > 0
            ? ProcessSteps.Max(s => s.OrderNumber) + 1
            : 1;

        var newStep = new ProcessStepDefinition
        {
            OrderNumber = nextOrderNumber,
            Description = "New Step",
            ProductDescription = string.Empty,
            DimensionType = DimensionType.Count,
            DimensionUnit = "pieces",
            DefaultDimensionValue = 1,
            IsDefaultStep = false
        };

        ProcessSteps.Add(newStep);
        SelectedStep = newStep;

        SyncToDefinition();
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Removes the selected process step.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanRemoveStep))]
    public void RemoveStep()
    {
        if (SelectedStep == null)
            return;

        ProcessSteps.Remove(SelectedStep);
        SelectedStep = ProcessSteps.FirstOrDefault();

        SyncToDefinition();
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    public bool CanRemoveStep()
    {
        return CanModify() && SelectedStep != null && ProcessSteps.Count > 1;
    }

    /// <summary>
    /// Sets the selected step as the default step.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSetAsDefault))]
    public void SetAsDefault()
    {
        if (SelectedStep == null || Definition == null)
            return;

        foreach (var step in ProcessSteps)
        {
            step.IsDefaultStep = step == SelectedStep;
        }

        Definition.DefaultProcessStepOrderNumber = SelectedStep.OrderNumber;
        ProcessSteps.ResetBindings();

        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    public bool CanSetAsDefault()
    {
        return CanModify() && SelectedStep != null;
    }

    /// <summary>
    /// Moves the selected step up in order.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanMoveUp))]
    public void MoveUp()
    {
        if (SelectedStep == null)
            return;

        int index = ProcessSteps.IndexOf(SelectedStep);
        if (index > 0)
        {
            ProcessSteps.RaiseListChangedEvents = false;
            var item = ProcessSteps[index];
            ProcessSteps.RemoveAt(index);
            ProcessSteps.Insert(index - 1, item);
            ProcessSteps.RaiseListChangedEvents = true;
            ProcessSteps.ResetBindings();

            SyncToDefinition();
            DefinitionModified?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool CanMoveUp()
    {
        return CanModify() && SelectedStep != null && ProcessSteps.IndexOf(SelectedStep) > 0;
    }

    /// <summary>
    /// Moves the selected step down in order.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanMoveDown))]
    public void MoveDown()
    {
        if (SelectedStep == null)
            return;

        int index = ProcessSteps.IndexOf(SelectedStep);
        if (index < ProcessSteps.Count - 1)
        {
            ProcessSteps.RaiseListChangedEvents = false;
            var item = ProcessSteps[index];
            ProcessSteps.RemoveAt(index);
            ProcessSteps.Insert(index + 1, item);
            ProcessSteps.RaiseListChangedEvents = true;
            ProcessSteps.ResetBindings();

            SyncToDefinition();
            DefinitionModified?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool CanMoveDown()
    {
        return CanModify() && SelectedStep != null &&
               ProcessSteps.IndexOf(SelectedStep) < ProcessSteps.Count - 1;
    }

    public bool CanModify()
    {
        return Definition != null && !IsLocked;
    }

    /// <summary>
    /// Synchronizes the process steps list back to the definition.
    /// </summary>
    public void SyncToDefinition()
    {
        if (Definition == null)
            return;

        Definition.ProcessSteps = ProcessSteps.ToList();

        // Update default step order number if needed
        var defaultStep = ProcessSteps.FirstOrDefault(s => s.IsDefaultStep);
        if (defaultStep != null)
        {
            Definition.DefaultProcessStepOrderNumber = defaultStep.OrderNumber;
        }
    }
}
