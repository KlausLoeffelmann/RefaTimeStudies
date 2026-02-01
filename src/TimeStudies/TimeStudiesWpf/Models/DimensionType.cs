namespace TimeStudiesWpf.Models;

/// <summary>
/// Represents the type of dimension for a process step.
/// Stellt die Art der Dimension für einen Ablaufabschnitt dar.
/// </summary>
public enum DimensionType
{
    /// <summary>
    /// Weight measurement (Gewicht)
    /// </summary>
    Weight,

    /// <summary>
    /// Count or pieces (Anzahl / Stück)
    /// </summary>
    Count,

    /// <summary>
    /// Length measurement (Länge)
    /// </summary>
    Length,

    /// <summary>
    /// Area measurement (Fläche)
    /// </summary>
    Area,

    /// <summary>
    /// Volume measurement (Volumen)
    /// </summary>
    Volume,

    /// <summary>
    /// Custom dimension type (Benutzerdefiniert)
    /// </summary>
    Custom
}