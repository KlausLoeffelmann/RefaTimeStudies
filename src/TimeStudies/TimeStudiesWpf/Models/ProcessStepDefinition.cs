using System.Text.Json.Serialization;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Represents a single process step definition within a time study.
/// Stellt einen einzelnen Ablaufabschnitt innerhalb einer Zeitaufnahme-Definition dar.
/// </summary>
public class ProcessStepDefinition
{
    /// <summary>
    /// User-defined order number for this process step (e.g., 1, 2, 3... or 18, 19, 20).
    /// Vom Benutzer definierte Nummerierung für diesen Ablaufabschnitt.
    /// </summary>
    public int OrderNumber { get; set; }

    /// <summary>
    /// Description of what the process step entails.
    /// Beschreibung dessen, was der Ablaufabschnitt beinhaltet.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Description of the product being processed in this step.
    /// Beschreibung des Produkts, das in diesem Schritt bearbeitet wird.
    /// </summary>
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// The type of dimension being measured.
    /// Die Art der Dimension, die gemessen wird.
    /// </summary>
    public DimensionType DimensionType { get; set; }

    /// <summary>
    /// The unit of measurement (e.g., kg, pieces, meters).
    /// Die Maßeinheit (z.B. kg, Stück, Meter).
    /// </summary>
    public string DimensionUnit { get; set; } = string.Empty;

    /// <summary>
    /// Default dimension value for this process step.
    /// Standard-Dimensionswert für diesen Ablaufabschnitt.
    /// </summary>
    public decimal DefaultDimensionValue { get; set; }

    /// <summary>
    /// Indicates whether this is the default/catch-all process step where time accumulates
    /// when no specific step is selected.
    /// Gibt an, ob dies der Standard-Ablaufabschnitt ist, in dem die Zeit akkumuliert,
    /// wenn kein spezifischer Schritt ausgewählt ist.
    /// </summary>
    public bool IsDefaultStep { get; set; }
}