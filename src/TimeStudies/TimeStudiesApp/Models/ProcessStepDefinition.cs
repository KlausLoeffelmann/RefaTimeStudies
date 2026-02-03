using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a single measurable operation element (Ablaufabschnitt) in a time study definition.
/// </summary>
public class ProcessStepDefinition : INotifyPropertyChanged
{
    private int _orderNumber;
    private string _description = string.Empty;
    private string _productDescription = string.Empty;
    private DimensionType _dimensionType = DimensionType.Count;
    private string _dimensionUnit = string.Empty;
    private decimal _defaultDimensionValue = 1;
    private bool _isDefaultStep;

    /// <summary>
    /// User-defined order number (e.g., 1, 2, 3 or 18, 19, 20). Gaps are allowed.
    /// </summary>
    public int OrderNumber
    {
        get => _orderNumber;
        set
        {
            if (_orderNumber != value)
            {
                _orderNumber = value;
                OnPropertyChanged(nameof(OrderNumber));
            }
        }
    }

    /// <summary>
    /// Description of the process step (Beschreibung).
    /// </summary>
    public string Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value ?? string.Empty;
                OnPropertyChanged(nameof(Description));
            }
        }
    }

    /// <summary>
    /// Description of what is being processed (Produkt).
    /// </summary>
    public string ProductDescription
    {
        get => _productDescription;
        set
        {
            if (_productDescription != value)
            {
                _productDescription = value ?? string.Empty;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }
    }

    /// <summary>
    /// Type of dimension measurement (Weight, Count, Length, etc.).
    /// </summary>
    public DimensionType DimensionType
    {
        get => _dimensionType;
        set
        {
            if (_dimensionType != value)
            {
                _dimensionType = value;
                OnPropertyChanged(nameof(DimensionType));
            }
        }
    }

    /// <summary>
    /// Unit of measurement (kg, pieces, meters, etc.).
    /// </summary>
    public string DimensionUnit
    {
        get => _dimensionUnit;
        set
        {
            if (_dimensionUnit != value)
            {
                _dimensionUnit = value ?? string.Empty;
                OnPropertyChanged(nameof(DimensionUnit));
            }
        }
    }

    /// <summary>
    /// Default value for the dimension measurement.
    /// </summary>
    public decimal DefaultDimensionValue
    {
        get => _defaultDimensionValue;
        set
        {
            if (_defaultDimensionValue != value)
            {
                _defaultDimensionValue = value;
                OnPropertyChanged(nameof(DefaultDimensionValue));
            }
        }
    }

    /// <summary>
    /// Indicates if this is the default/catch-all step (Standard-Ablaufabschnitt).
    /// </summary>
    public bool IsDefaultStep
    {
        get => _isDefaultStep;
        set
        {
            if (_isDefaultStep != value)
            {
                _isDefaultStep = value;
                OnPropertyChanged(nameof(IsDefaultStep));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Creates a deep copy of this process step definition.
    /// </summary>
    public ProcessStepDefinition Clone()
    {
        return new ProcessStepDefinition
        {
            OrderNumber = OrderNumber,
            Description = Description,
            ProductDescription = ProductDescription,
            DimensionType = DimensionType,
            DimensionUnit = DimensionUnit,
            DefaultDimensionValue = DefaultDimensionValue,
            IsDefaultStep = IsDefaultStep
        };
    }
}
