using System.Text.Json.Serialization;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Defines a single process step (Ablaufabschnitt) within a time study definition.
/// </summary>
public class ProcessStepDefinition
{
    /// <summary>
    /// User-defined order number (e.g., 1, 2, 5, 18, 19 - gaps allowed).
    /// Used for traditional REFA numbering schemes.
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
    /// Type of dimension for measuring output (Weight, Count, Length, etc.).
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DimensionType DimensionType { get; set; } = DimensionType.Count;

    /// <summary>
    /// Unit of measurement (e.g., kg, pieces, meters).
    /// </summary>
    public string DimensionUnit { get; set; } = string.Empty;

    /// <summary>
    /// Default value for the dimension (e.g., 1 piece, 5 kg).
    /// </summary>
    public decimal DefaultDimensionValue { get; set; } = 1;

    /// <summary>
    /// Indicates if this is the default/catch-all step (Standard-Ablaufabschnitt).
    /// Time accumulates here when no specific step is selected.
    /// </summary>
    public bool IsDefaultStep { get; set; }
}
