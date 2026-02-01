using System.Text.Json.Serialization;
using TimeStudiesWpf.Helpers;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Represents a single time entry recorded during a time study.
/// Stellt einen einzelnen Zeiteintrag dar, der während einer Zeitaufnahme aufgezeichnet wurde.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Auto-incrementing sequence number for the entry.
    /// Automatisch inkrementierende Sequenznummer für den Eintrag.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// The order number of the process step this entry belongs to.
    /// Die Nummer des Ablaufabschnitts, zu dem dieser Eintrag gehört.
    /// </summary>
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Description of the process step at the time of recording.
    /// Beschreibung des Ablaufabschnitts zum Zeitpunkt der Aufzeichnung.
    /// </summary>
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp when this entry was recorded.
    /// Der Zeitstempel, zu dem dieser Eintrag aufgezeichnet wurde.
    /// </summary>
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// The elapsed time from the start of the recording (Fortschrittszeit).
    /// Die verstrichene Zeit seit dem Start der Aufzeichnung.
    /// </summary>
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    /// The duration of this time entry (calculated when the next entry is made).
    /// Die Dauer dieses Zeiteintrags (berechnet, wenn der nächste Eintrag erstellt wird).
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Optional override for the dimension value for this specific entry.
    /// Optionale Überschreibung des Dimensionswerts für diesen spezifischen Eintrag.
    /// </summary>
    public decimal? DimensionValue { get; set; }
}