using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Defines a single process step within a time study definition.
/// </summary>
public class ProcessStepDefinition
{
    /// <summary>
    /// User-defined order number for this step (e.g., 1, 2, 3... or 18, 19, 40).
    /// Gaps in numbering are allowed.
    /// </summary>
    public int OrderNumber { get; set; }

    /// <summary>
    /// Description of the process step.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Description of the product being processed in this step.
    /// </summary>
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// Type of dimension used for measurement (Weight, Count, Length, etc.).
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DimensionType DimensionType { get; set; } = DimensionType.Count;

    /// <summary>
    /// Unit of measurement (kg, pieces, meters, etc.).
    /// </summary>
    public string DimensionUnit { get; set; } = string.Empty;

    /// <summary>
    /// Default value for the dimension.
    /// </summary>
    public decimal DefaultDimensionValue { get; set; } = 1;

    /// <summary>
    /// Indicates whether this is the default "catch-all" step.
    /// Time flows to this step when no specific step is selected.
    /// </summary>
    public bool IsDefaultStep { get; set; }

    /// <summary>
    /// Creates a deep copy of this process step definition.
    /// </summary>
    public ProcessStepDefinition Clone() => new()
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
