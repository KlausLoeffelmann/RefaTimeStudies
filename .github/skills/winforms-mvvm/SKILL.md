---
name: WinForms MVVM Expert
description: Comprehensive guide for implementing MVVM pattern in WinForms applications (.NET 8+).
---

# WinForms MVVM Expert Guide

MVVM in WinForms (.NET 8+) separates business logic from UI, making applications testable, maintainable, and portable.

## Project Structure

**CRITICAL:** Use separate class library for ViewModels - code generators are NOT cascadable.

```
MySolution/
├── MyApp.WinForms/              # WinForms presentation layer
│   ├── Forms/
│   └── Properties/DataSources/  # .datasource files
├── MyApp.ViewModels/            # ViewModels class library
├── MyApp.Models/                # Domain models
└── MyApp.Services/              # Business logic
```

**ViewModels project:**
```xml
<PackageReference Include="CommunityToolkit.Mvvm" Version="[8.*,)" />
```

## .NET 8+ MVVM APIs

| API | Description | Cascading |
|-----|-------------|-----------|
| `Control.DataContext` | Ambient property for hierarchical binding | Yes |
| `ButtonBase.Command` | ICommand binding for buttons | No |
| `ToolStripItem.Command` | ICommand binding for menus/toolbars | No |
| `*.CommandParameter` | Parameter passed to command | No |

**Note:** `ToolStripItem` now derives from `BindableComponent` (.NET 8+).

## Basic ViewModel

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class PersonViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    [RelayCommand]
    private async Task SaveAsync(CancellationToken ct)
    {
        await _dataService.SaveAsync(FirstName, LastName, ct);
    }

    [RelayCommand(CanExecute = nameof(CanDelete))]
    private async Task DeleteAsync(CancellationToken ct) { }
    
    private bool CanDelete() => Age >= 18;

    partial void OnAgeChanged(int value)
    {
        DeleteCommand.NotifyCanExecuteChanged();
    }
}
```

## DataSource File

**File:** `Properties/DataSources/PersonViewModel.datasource`
```xml
<?xml version="1.0" encoding="utf-8"?>
<GenericObjectDataSource DisplayName="PersonViewModel" Version="1.0" 
    xmlns="urn:schemas-microsoft-com:xml-msdatasource">
  <TypeInfo>MyApp.ViewModels.PersonViewModel, MyApp.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</TypeInfo>
</GenericObjectDataSource>
```

## Form Design Pattern

**Designer (InitializeComponent):**
```csharp
personViewModelBindingSource = new BindingSource(components);
personViewModelBindingSource.DataSource = typeof(MyApp.ViewModels.PersonViewModel);

_txtFirstName.DataBindings.Add(new Binding("Text", personViewModelBindingSource, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged));
_btnSave.DataBindings.Add(new Binding("Command", personViewModelBindingSource, "SaveCommand", true));
```

**Code-Behind:**
```csharp
public partial class PersonEditForm : Form
{
    private readonly PersonViewModel _viewModel;

    public PersonEditForm(PersonViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        _viewModel = viewModel;
        InitializeComponent();
        personViewModelBindingSource.DataSource = _viewModel;
    }
}
```

## Command Binding

**Basic Command:**
```csharp
_btnSave.DataBindings.Add(new Binding("Command", viewModelBindingSource, "SaveCommand", true));
```

**With Parameter:**
```csharp
_btnApprove.DataBindings.Add(new Binding("Command", viewModelBindingSource, "ProcessCommand", true));
_btnApprove.CommandParameter = "Approve";
```

**Menu Commands:**
```csharp
_tsmFile.DataBindings.Add(new Binding("Command", mainViewModelBindingSource, "MenuCommand", true));
_tsmFile.CommandParameter = "File";
```

## ObservableCollection Support

**ViewModel:**
```csharp
public partial class PeopleListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people = new();

    [ObservableProperty]
    private PersonViewModel? _selectedPerson;

    [RelayCommand]
    private void AddPerson() => People.Add(new PersonViewModel());

    partial void OnSelectedPersonChanged(PersonViewModel? value)
    {
        RemovePersonCommand.NotifyCanExecuteChanged();
    }
}
```

**Form with Adapter:**
```csharp
public partial class PeopleListForm : Form
{
    private ObservableBindingList<PersonViewModel>? _bindingAdapter;

    public PeopleListForm(PeopleListViewModel viewModel)
    {
        InitializeComponent();
        _bindingAdapter = new ObservableBindingList<PersonViewModel>(viewModel.People);
        dataGridView1.DataSource = _bindingAdapter;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _bindingAdapter?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

**Adapter implementation:** See [WinForms-Binding-Expert.md](WinForms-Binding-Expert.md#observablecollection-adapter).

## DataContext Property

**Parent Form:**
```csharp
protected override void OnLoad(EventArgs e)
{
    base.OnLoad(e);
    DataContext = _viewModel;  // Flows to child controls
}
```

**Child UserControl:**
```csharp
protected override void OnDataContextChanged(EventArgs e)
{
    base.OnDataContextChanged(e);
    if (DataContext is PersonViewModel viewModel)
        personViewModelBindingSource.DataSource = viewModel;
}
```

## Migration from Event-Handlers

**Before (Traditional):**
- Business logic in Form
- Manual synchronization
- Cannot unit test
- Exception handling mixed with UI

**After (MVVM):**

**ViewModel (testable):**
```csharp
public partial class PersonViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string? _errorMessage;

    [RelayCommand]
    private async Task LoadAsync(CancellationToken ct)
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;
            Person? person = await _dataService.LoadPersonAsync(ct);
            if (person is not null)
            {
                FirstName = person.FirstName;
                LastName = person.LastName;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
```

**Form (pure view):**
```csharp
public partial class PersonForm : Form
{
    private readonly PersonViewModel _viewModel;

    public PersonForm(PersonViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        personViewModelBindingSource.DataSource = _viewModel;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        try { await _viewModel.LoadCommand.ExecuteAsync(null); }
        catch (Exception ex) { Application.OnThreadException(ex); }
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PersonViewModel.ErrorMessage) && !string.IsNullOrEmpty(_viewModel.ErrorMessage))
            MessageBox.Show(_viewModel.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        else if (e.PropertyName == nameof(PersonViewModel.IsLoading))
            Cursor = _viewModel.IsLoading ? Cursors.WaitCursor : Cursors.Default;
    }
}
```

**Unit Tests:**
```csharp
[Fact]
public void FullName_CombinesFirstAndLastName()
{
    PersonViewModel vm = new(Mock.Of<IDataService>()) { FirstName = "John", LastName = "Doe" };
    Assert.Equal("John Doe", vm.FullName);
}

[Theory]
[InlineData(17, false)]
[InlineData(18, true)]
public void DeleteCommand_CanExecute_BasedOnAge(int age, bool expected)
{
    PersonViewModel vm = new(Mock.Of<IDataService>()) { Age = age };
    Assert.Equal(expected, vm.DeleteCommand.CanExecute(null));
}
```

## Best Practices

### DO:
✅ Use separate class library for ViewModels  
✅ Use MVVM Community Toolkit  
✅ Create `.datasource` files for designer  
✅ Keep forms as pure views  
✅ Unit test ViewModels extensively  
✅ Use `DataContext` for nested controls  
✅ Handle UI concerns (dialogs, cursors) in code-behind  

### DON'T:
❌ Put business logic in Forms  
❌ Use lambdas in `InitializeComponent`  
❌ Reference WinForms types from ViewModel project  
❌ Bind `ObservableCollection<T>` without adapter  
❌ Create ViewModels in WinForms project (breaks generation)  
❌ Test Forms directly (test ViewModels)  

## Summary

MVVM in WinForms (.NET 8+) enables:
- **Testability:** Business logic unit tested without UI
- **Portability:** ViewModels shared across frameworks
- **Maintainability:** Clear separation of concerns
- **Designer compatibility:** Forms editable in Designer

Key enablers: `DataContext`, `Command` properties, `BindingSource`, MVVM Community Toolkit.

For data binding details, see [WinForms-Binding-Expert.md](WinForms-Binding-Expert.md).
