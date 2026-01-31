namespace TimeStudiesApp.Models;

/// <summary>
///  Represents a single process step definition (Ablaufabschnitt) within a time study.
/// </summary>
public class ProcessStepDefinition
{
    /// <summary>
    ///  Gets or sets the user-defined order number for this process step.
    ///  Order numbers can have gaps (e.g., 1, 2, 5, 18, 19).
    /// </summary>
    public int OrderNumber { get; set; }

    /// <summary>
    ///  Gets or sets the description of this process step.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the description of what is being processed/produced.
    /// </summary>
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the type of dimension used for measurement.
    /// </summary>
    public DimensionType DimensionType { get; set; } = DimensionType.Count;

    /// <summary>
    ///  Gets or sets the unit of measurement (e.g., kg, pieces, meters).
    /// </summary>
    public string DimensionUnit { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the default dimension value for this step.
    /// </summary>
    public decimal DefaultDimensionValue { get; set; } = 1;

    /// <summary>
    ///  Gets or sets a value indicating whether this is the default (catch-all) step.
    ///  Time flows to this step when no specific step is selected.
    /// </summary>
    public bool IsDefaultStep { get; set; }

    /// <summary>
    ///  Creates a deep copy of this process step definition.
    /// </summary>
    /// <returns>A new instance with copied values.</returns>
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
