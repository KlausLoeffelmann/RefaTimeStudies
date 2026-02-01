using System.Text.Json.Serialization;
using TimeStudiesWpf.Helpers;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Represents a complete time study definition (Zeitaufnahme-Definition).
/// A definition is a template defining what process steps to measure during a time study.
/// Stellt eine vollständige Zeitaufnahme-Definition dar.
/// Eine Definition ist eine Vorlage, die definiert, welche Ablaufabschnitte während einer Zeitaufnahme gemessen werden.
/// </summary>
public class TimeStudyDefinition
{
    /// <summary>
    /// Unique identifier for this definition.
    /// Eindeutiger Identifikator für diese Definition.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the time study definition.
    /// Name der Zeitaufnahme-Definition.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of what this time study measures.
    /// Beschreibung dessen, was diese Zeitaufnahme misst.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when this definition was created.
    /// Zeitstempel, wann diese Definition erstellt wurde.
    /// </summary>
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this definition was last modified.
    /// Zeitstempel, wann diese Definition zuletzt geändert wurde.
    /// </summary>
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime ModifiedAt { get; set; }

    /// <summary>
    /// Indicates whether this definition is locked. A definition becomes locked
    /// when at least one recording has been created from it.
    /// Gibt an, ob diese Definition gesperrt ist. Eine Definition wird gesperrt,
    /// wenn mindestens eine Aufnahme daraus erstellt wurde.
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// List of process steps defined in this time study.
    /// Liste der in dieser Zeitaufnahme definierten Ablaufabschnitte.
    /// </summary>
    public List<ProcessStepDefinition> ProcessSteps { get; set; } = new();

    /// <summary>
    /// The order number of the default process step (the catch-all step).
    /// Die Nummer des Standard-Ablaufabschnitts (der "Auffang"-Schritt).
    /// </summary>
    public int DefaultProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Initializes a new instance of TimeStudyDefinition with default values.
    /// Initialisiert eine neue Instanz von TimeStudyDefinition mit Standardwerten.
    /// </summary>
    public TimeStudyDefinition()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
        IsLocked = false;
    }

    /// <summary>
    /// Gets the default process step from the ProcessSteps list.
    /// Gibt den Standard-Ablaufabschnitt aus der Liste der Ablaufabschnitte zurück.
    /// </summary>
    /// <returns>The default process step, or null if not found.</returns>
    public ProcessStepDefinition? GetDefaultProcessStep()
    {
        return ProcessSteps.FirstOrDefault(ps => ps.IsDefaultStep);
    }

    /// <summary>
    /// Gets a process step by its order number.
    /// Gibt einen Ablaufabschnitt anhand seiner Nummer zurück.
    /// </summary>
    /// <param name="orderNumber">The order number to search for.</param>
    /// <returns>The process step with the specified order number, or null if not found.</returns>
    public ProcessStepDefinition? GetProcessStepByOrderNumber(int orderNumber)
    {
        return ProcessSteps.FirstOrDefault(ps => ps.OrderNumber == orderNumber);
    }

    /// <summary>
    /// Updates the ModifiedAt timestamp to the current time.
    /// Aktualisiert den ModifiedAt-Zeitstempel auf die aktuelle Zeit.
    /// </summary>
    public void UpdateModifiedTimestamp()
    {
        ModifiedAt = DateTime.Now;
    }
}