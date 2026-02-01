using System.Text.Json.Serialization;
using TimeStudiesWpf.Helpers;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Represents an actual time study recording session using a definition.
/// Stellt eine tatsächliche Zeitaufnahme-Sitzung unter Verwendung einer Definition dar.
/// </summary>
public class TimeStudyRecording
{
    /// <summary>
    /// Unique identifier for this recording.
    /// Eindeutiger Identifikator für diese Aufnahme.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the definition this recording is based on.
    /// Die ID der Definition, auf der diese Aufnahme basiert.
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// The name of the definition this recording is based on.
    /// Der Name der Definition, auf der diese Aufnahme basiert.
    /// </summary>
    public string DefinitionName { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the recording was started.
    /// Zeitstempel, wann die Aufnahme gestartet wurde.
    /// </summary>
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Timestamp when the recording was completed (null if still in progress).
    /// Zeitstempel, wann die Aufnahme abgeschlossen wurde (null, wenn noch im Gange).
    /// </summary>
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Indicates whether the recording has been completed.
    /// Gibt an, ob die Aufnahme abgeschlossen wurde.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// List of all time entries recorded during this session.
    /// Liste aller während dieser Sitzung aufgezeichneten Zeiteinträge.
    /// </summary>
    public List<TimeEntry> Entries { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of TimeStudyRecording.
    /// Initialisiert eine neue Instanz von TimeStudyRecording.
    /// </summary>
    public TimeStudyRecording()
    {
        Id = Guid.NewGuid();
        StartedAt = DateTime.Now;
        IsCompleted = false;
    }

    /// <summary>
    /// Adds a time entry to the recording and sets its sequence number.
    /// Fügt einen Zeiteintrag zur Aufnahme hinzu und setzt seine Sequenznummer.
    /// </summary>
    /// <param name="entry">The time entry to add.</param>
    public void AddEntry(TimeEntry entry)
    {
        entry.SequenceNumber = Entries.Count + 1;
        Entries.Add(entry);
    }

    /// <summary>
    /// Gets the most recent time entry, or null if no entries exist.
    /// Gibt den neuesten Zeiteintrag zurück oder null, wenn keine Einträge existieren.
    /// </summary>
    /// <returns>The most recent time entry, or null.</returns>
    public TimeEntry? GetLastEntry()
    {
        return Entries.LastOrDefault();
    }

    /// <summary>
    /// Gets the total duration of the recording (from start to last entry or completion).
    /// Gibt die Gesamtdauer der Aufnahme zurück (vom Start bis zum letzten Eintrag oder Abschluss).
    /// </summary>
    /// <returns>The total duration as a TimeSpan.</returns>
    public TimeSpan GetTotalDuration()
    {
        if (CompletedAt.HasValue)
        {
            return CompletedAt.Value - StartedAt;
        }
        else if (Entries.Count > 0)
        {
            return Entries.Last().ElapsedFromStart + Entries.Last().Duration;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }

    /// <summary>
    /// Marks the recording as completed and sets the completion timestamp.
    /// Markiert die Aufnahme als abgeschlossen und setzt den Abschluss-Zeitstempel.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletedAt = DateTime.Now;
    }
}