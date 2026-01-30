---
name: WinForms Data Binding Expert
description: Comprehensive guide for WinForms data binding patterns and best practices.
---

# WinForms Data Binding Expert Guide

Data binding in WinForms connects UI controls to data sources, enabling automatic synchronization between view and data.

## Breaking Changes: .NET Framework vs .NET 8+

| Feature | .NET Framework <= 4.8.1 | .NET 8+ |
|---------|----------------------|---------|
| Typed DataSets | Designer supported | Code-only (legacy, not recommended) |
| Object Binding | Supported | Enhanced UI, fully supported |
| Data Sources Window | Available | Not available |
| ICommand Support | Not available | Built-in via `Command` property |
| DataContext | Not available | Ambient property for hierarchical binding |

## Core Requirements

**INotifyPropertyChanged** - Required for property change notifications:
```csharp
public class Person : INotifyPropertyChanged
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```

**BindingList<T>** - Required for collection change notifications:
```csharp
BindingList<Person> people = new BindingList<Person>();
dataGridView1.DataSource = people;
```

## BindingSource Component

Acts as mediator between data sources and controls:
```csharp
// In InitializeComponent (designer code)
personBindingSource = new BindingSource(components);
personBindingSource.DataSource = typeof(MyApp.Models.Person);
_txtName.DataBindings.Add(new Binding("Text", personBindingSource, "Name", true));

// In main code - set actual instance
personBindingSource.DataSource = new Person { Name = "John Doe" };
```

## DataSourceUpdateMode

| Mode | Behavior | Use Case |
|------|----------|----------|
| `OnValidation` | Updates when control loses focus | Default; most common |
| `OnPropertyChanged` | Updates immediately on every keystroke | Real-time validation |
| `Never` | One-way binding from source to control | Read-only displays |

## Adding Object DataSources

Create `.datasource` file in `Properties\DataSources\`:
```xml
<?xml version="1.0" encoding="utf-8"?>
<GenericObjectDataSource DisplayName="Person" Version="1.0" 
    xmlns="urn:schemas-microsoft-com:xml-msdatasource">
  <TypeInfo>MyApp.Models.Person, MyApp.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</TypeInfo>
</GenericObjectDataSource>
```

## ObservableCollection Adapter

**When needed:** Sharing ViewModels between WinForms and WPF/MAUI, or using MVVM Toolkit patterns.

```csharp
public class ObservableBindingList<T> : BindingList<T>
{
    private ObservableCollection<T>? _observableCollection;

    public ObservableBindingList(ObservableCollection<T> observableCollection)
    {
        _observableCollection = observableCollection;
        foreach (T item in _observableCollection) Add(item);
        _observableCollection.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems is not null)
                    foreach (T item in e.NewItems) Add(item);
                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems is not null)
                    foreach (T item in e.OldItems) Remove(item);
                break;
            case NotifyCollectionChangedAction.Reset:
                Clear();
                if (_observableCollection is not null)
                    foreach (T item in _observableCollection) Add(item);
                break;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && _observableCollection is not null)
        {
            _observableCollection.CollectionChanged -= OnCollectionChanged;
            _observableCollection = null;
        }
        base.Dispose(disposing);
    }
}
```

## Value Conversion (IValueConverter Workaround)

Use `Format` and `Parse` events:
```csharp
private void SetupCurrencyBinding()
{
    Binding binding = _txtPrice.DataBindings["Text"];
    binding.Format += (s, e) => { if (e.Value is decimal d) e.Value = d.ToString("C2"); };
    binding.Parse += (s, e) => { if (e.Value is string s && decimal.TryParse(s, NumberStyles.Currency, null, out decimal d)) e.Value = d; };
}
```

## Master-Detail Binding

```csharp
BindingSource customerBindingSource = new() { DataSource = typeof(Customer) };
BindingSource ordersBindingSource = new() { DataSource = customerBindingSource, DataMember = "Orders" };
dataGridView1.DataSource = customerBindingSource;
dataGridView2.DataSource = ordersBindingSource;
```

## Validation

**IDataErrorInfo Implementation:**
```csharp
public class Person : INotifyPropertyChanged, IDataErrorInfo
{
    public string Error => string.Empty;
    public string this[string columnName] => columnName switch
    {
        nameof(Name) when string.IsNullOrWhiteSpace(Name) => "Name is required",
        nameof(Age) when Age < 0 || Age > 120 => "Age must be between 0 and 120",
        _ => string.Empty
    };
}
```

**ErrorProvider Integration:**
```csharp
ErrorProvider errorProvider = new() { DataSource = personBindingSource };
// CRITICAL: Never set e.Cancel = true in validation events - breaks focus navigation
```

## DataGridView Patterns

**Basic:** `dataGridView1.DataSource = bindingList;`

**Custom Columns:**
```csharp
dataGridView1.AutoGenerateColumns = false;
dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Full Name" });
```

**Virtual Mode (large datasets):**
```csharp
dataGridView1.VirtualMode = true;
dataGridView1.CellValueNeeded += (s, e) => { e.Value = _dataCache[e.RowIndex].GetValue(e.ColumnIndex); };
```

## ComboBox/ListBox Binding

```csharp
comboBox1.DataSource = people;
comboBox1.DisplayMember = "Name";
comboBox1.ValueMember = "Id";
// Bind selection to property
_cmbCountry.DataBindings.Add(new Binding("SelectedValue", personBindingSource, "CountryCode", true));
```

## Best Practices

### DO:
✅ Use `BindingSource` as intermediary  
✅ Implement `INotifyPropertyChanged` on data objects  
✅ Use `BindingList<T>` for WinForms collections  
✅ Validate at form level  
✅ Use `.datasource` files for designer support  
✅ Dispose BindingSource properly  

### DON'T:
❌ Set `e.Cancel = true` in validation (breaks focus)  
❌ Use `ObservableCollection<T>` without adapter  
❌ Forget to call `WriteValue()` before reading bound data  
❌ Use DataSets for new development (legacy)  

## Troubleshooting

**Binding not updating:** Ensure `INotifyPropertyChanged` is implemented and `PropertyChanged` is raised.

**Performance issues:** Use virtual mode for DataGridView, implement paging, use `SuspendBinding()`/`ResumeBinding()`.

**Memory leaks:** Dispose BindingSource in form's Dispose method, clear event handlers, set DataSource to null.

---

For MVVM-specific patterns, see [WinForms-MVVM-Expert.md](WinForms-MVVM-Expert.md).
