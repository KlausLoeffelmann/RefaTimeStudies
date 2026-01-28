using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a single time entry in a recording.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Auto-incrementing sequence number within the recording.
    /// </summary>
    [JsonPropertyName("sequenceNumber")]
    public int SequenceNumber { get; set; }

    /// <summary>
    /// The order number of the process step.
    /// </summary>
    [JsonPropertyName("processStepOrderNumber")]
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Description of the process step (denormalized for export).
    /// </summary>
    [JsonPropertyName("processStepDescription")]
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    /// When this entry was recorded.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Cumulative time from study start (Fortschrittszeit).
    /// </summary>
    [JsonPropertyName("elapsedFromStart")]
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    /// Duration of this step (calculated when next entry is made).
    /// </summary>
    [JsonPropertyName("duration")]
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Optional override for the dimension value for this entry.
    /// </summary>
    [JsonPropertyName("dimensionValue")]
    public decimal? DimensionValue { get; set; }
}
