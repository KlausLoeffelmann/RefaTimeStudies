using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a single measurable operation element (Ablaufabschnitt).
/// </summary>
public class ProcessStepDefinition
{
    /// <summary>
    /// User-defined order number (e.g., 1, 2, 3 or 18, 19, 20).
    /// Gaps in numbering are allowed.
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public int OrderNumber { get; set; }

    /// <summary>
    /// Description of the process step.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Description of what is being processed/produced.
    /// </summary>
    [JsonPropertyName("productDescription")]
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// Type of dimension (Weight, Count, Length, etc.).
    /// </summary>
    [JsonPropertyName("dimensionType")]
    public DimensionType DimensionType { get; set; } = DimensionType.Count;

    /// <summary>
    /// Unit of measurement (kg, pieces, meters, etc.).
    /// </summary>
    [JsonPropertyName("dimensionUnit")]
    public string DimensionUnit { get; set; } = string.Empty;

    /// <summary>
    /// Default value for the dimension.
    /// </summary>
    [JsonPropertyName("defaultDimensionValue")]
    public decimal DefaultDimensionValue { get; set; } = 1;

    /// <summary>
    /// Marks this step as the "catch-all" default step.
    /// Time flows here when no specific step is selected.
    /// </summary>
    [JsonPropertyName("isDefaultStep")]
    public bool IsDefaultStep { get; set; }

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
