namespace TimeStudiesApp.Models;

/// <summary>
/// Represents the type of dimension/measurement for a process step.
/// </summary>
public enum DimensionType
{
    /// <summary>Weight (Gewicht)</summary>
    Weight,

    /// <summary>Count/Pieces (Anzahl/Stück)</summary>
    Count,

    /// <summary>Length (Länge)</summary>
    Length,

    /// <summary>Area (Fläche)</summary>
    Area,

    /// <summary>Volume (Volumen)</summary>
    Volume,

    /// <summary>Custom (Benutzerdefiniert)</summary>
    Custom
}
