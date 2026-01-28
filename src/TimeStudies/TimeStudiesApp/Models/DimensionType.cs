namespace TimeStudiesApp.Models;

/// <summary>
/// Represents the type of dimension used for measuring process steps.
/// </summary>
public enum DimensionType
{
    /// <summary>Weight measurement (Gewicht).</summary>
    Weight,

    /// <summary>Count measurement (Anzahl / Stück).</summary>
    Count,

    /// <summary>Length measurement (Länge).</summary>
    Length,

    /// <summary>Area measurement (Fläche).</summary>
    Area,

    /// <summary>Volume measurement (Volumen).</summary>
    Volume,

    /// <summary>Custom user-defined measurement (Benutzerdefiniert).</summary>
    Custom
}
