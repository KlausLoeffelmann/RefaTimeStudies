using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using WpfUserControl = System.Windows.Controls.UserControl;
using WpfMessageBox = System.Windows.MessageBox;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf.Controls;

/// <summary>
/// Interaction logic for DefinitionEditorControl.xaml
/// </summary>
public partial class DefinitionEditorControl : WpfUserControl, INotifyPropertyChanged
{
    private TimeStudyDefinition? _definition;
    private ProcessStepDefinition? _selectedProcessStep;
    private readonly DefinitionService _definitionService;
    private bool _hasUnsavedChanges;

    public DefinitionEditorControl()
    {
        InitializeComponent();
        DataContext = this;
        _definitionService = App.DefinitionService;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // Set localized strings
        HeaderLabel.Content = ResourceHelper.GetString("ViewDefinitionEditor");
        DefinitionInfoLabel.Content = ResourceHelper.GetString("DefinitionName");
        DefinitionNameLabel.Content = ResourceHelper.GetString("DefinitionName") + ":";
        DescriptionLabel.Content = ResourceHelper.GetString("Description") + ":";
        ProcessStepLabel.Content = ResourceHelper.GetString("ProcessStep");
        AddStepButton.Content = ResourceHelper.GetString("BtnAddStep");
        RemoveStepButton.Content = ResourceHelper.GetString("BtnRemoveStep");
        MarkDefaultButton.Content = ResourceHelper.GetString("BtnMarkDefault");
        CreateCopyButton.Content = ResourceHelper.GetString("BtnCreateCopy");
        SaveButton.Content = ResourceHelper.GetString("BtnSave");
        CancelButton.Content = ResourceHelper.GetString("BtnCancel");

        // Set DataGrid column headers
        OrderNumberColumn.Header = ResourceHelper.GetString("OrderNumber");
        DescriptionColumn.Header = ResourceHelper.GetString("Description");
        ProductColumn.Header = ResourceHelper.GetString("Product");
        DimensionTypeColumn.Header = ResourceHelper.GetString("DimensionType");
        DimensionUnitColumn.Header = ResourceHelper.GetString("DimensionUnit");
        DefaultValueColumn.Header = ResourceHelper.GetString("DefaultValue");
        DefaultStepColumn.Header = ResourceHelper.GetString("DefaultStep");

        // Set dimension type options
        // Note: ItemsSource for DimensionTypeColumn is set in XAML via a static resource
    }

    #region Properties

    /// <summary>
    /// Gets or sets the time study definition being edited.
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
                OnPropertyChanged(nameof(IsLocked));
                OnPropertyChanged(nameof(IsEditingEnabled));
                OnPropertyChanged(nameof(CanSave));
                HasUnsavedChanges = false;
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected process step in the grid.
    /// </summary>
    public ProcessStepDefinition? SelectedProcessStep
    {
        get => _selectedProcessStep;
        set
        {
            if (_selectedProcessStep != value)
            {
                _selectedProcessStep = value;
                OnPropertyChanged(nameof(SelectedProcessStep));
            }
        }
    }

    /// <summary>
    /// Gets whether the definition is locked.
    /// </summary>
    public bool IsLocked => Definition?.IsLocked ?? false;

    /// <summary>
    /// Gets whether editing is enabled.
    /// </summary>
    public bool IsEditingEnabled => !IsLocked;

    /// <summary>
    /// Gets whether the definition can be saved.
    /// </summary>
    public bool CanSave => Definition != null && HasUnsavedChanges && !IsLocked;

    /// <summary>
    /// Gets or sets whether there are unsaved changes.
    /// </summary>
    public bool HasUnsavedChanges
    {
        get => _hasUnsavedChanges;
        set
        {
            if (_hasUnsavedChanges != value)
            {
                _hasUnsavedChanges = value;
                OnPropertyChanged(nameof(HasUnsavedChanges));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    #endregion

    #region Commands

    public ICommand AddStepCommand => new RelayCommand(AddStep);
    public ICommand RemoveStepCommand => new RelayCommand(RemoveStep, CanRemoveStep);
    public ICommand MarkAsDefaultCommand => new RelayCommand(MarkAsDefault, CanMarkAsDefault);
    public ICommand SaveCommand => new RelayCommand(Save, CanSaveExecute);
    public ICommand CancelCommand => new RelayCommand(Cancel);
    public ICommand CreateCopyCommand => new RelayCommand(CreateCopy);

    private void AddStep()
    {
        if (Definition == null) return;

        var newStep = new ProcessStepDefinition
        {
            OrderNumber = Definition.ProcessSteps.Count > 0
                ? Definition.ProcessSteps.Max(s => s.OrderNumber) + 1
                : 1,
            Description = "New Step",
            DimensionType = DimensionType.Count,
            DimensionUnit = "pcs",
            DefaultDimensionValue = 1,
            IsDefaultStep = false
        };

        Definition.ProcessSteps.Add(newStep);
        Definition.UpdateModifiedTimestamp();
        HasUnsavedChanges = true;
        SelectedProcessStep = newStep;
    }

    private void RemoveStep()
    {
        if (Definition == null || SelectedProcessStep == null) return;

        if (SelectedProcessStep.IsDefaultStep)
        {
            WpfMessageBox.Show(
                "You cannot remove the default process step.",
                "Warning",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Warning);
            return;
        }

        Definition.ProcessSteps.Remove(SelectedProcessStep);
        Definition.UpdateModifiedTimestamp();
        HasUnsavedChanges = true;
        SelectedProcessStep = null;
    }

    private bool CanRemoveStep() => SelectedProcessStep != null && IsEditingEnabled;

    private void MarkAsDefault()
    {
        if (Definition == null || SelectedProcessStep == null) return;

        foreach (var step in Definition.ProcessSteps)
        {
            step.IsDefaultStep = false;
        }

        SelectedProcessStep.IsDefaultStep = true;
        Definition.DefaultProcessStepOrderNumber = SelectedProcessStep.OrderNumber;
        Definition.UpdateModifiedTimestamp();
        HasUnsavedChanges = true;
    }

    private bool CanMarkAsDefault() => SelectedProcessStep != null && IsEditingEnabled;

    private void Save()
    {
        if (Definition == null) return;

        try
        {
            _definitionService.UpdateDefinition(Definition);
            HasUnsavedChanges = false;

            WpfMessageBox.Show(
                ResourceHelper.GetString("MsgSaveSuccessful"),
                "Success",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information);

            // Refresh the definition to get updated timestamp
            var refreshed = _definitionService.GetDefinition(Definition.Id);
            if (refreshed != null)
            {
                Definition = refreshed;
            }
        }
        catch (Exception ex)
        {
            WpfMessageBox.Show(
                $"Failed to save definition: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
    }

    private bool CanSaveExecute() => CanSave;

    public void Cancel()
    {
        if (HasUnsavedChanges)
        {
            var result = WpfMessageBox.Show(
                ResourceHelper.GetString("MsgUnsavedChanges"),
                "Warning",
                MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Warning);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                Save();
                return;
            }
        }

        // Reset to original state if we had a definition
        if (_definition != null && _definition.Id != Guid.Empty)
        {
            var original = _definitionService.GetDefinition(_definition.Id);
            if (original != null)
            {
                Definition = original;
            }
        }
    }

    private void CreateCopy()
    {
        if (Definition == null) return;

        try
        {
            var copy = _definitionService.CreateCopy(Definition);
            Definition = copy;
            HasUnsavedChanges = false;
        }
        catch (Exception ex)
        {
            WpfMessageBox.Show(
                $"Failed to create copy: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Helper Classes

    private class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();
    }

    #endregion
}

/// <summary>
/// Simple value converter for localization.
/// </summary>
public class LocalizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string key)
        {
            return ResourceHelper.GetString(key);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Boolean to visibility converter.
/// </summary>
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }
        return false;
    }
}