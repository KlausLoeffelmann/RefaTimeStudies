namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a single time entry in a recording.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Auto-incrementing sequence number within the recording.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// The order number of the process step that was active.
    /// </summary>
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Description of the process step (denormalized for export convenience).
    /// </summary>
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp when this entry was recorded.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Cumulative time elapsed from the start of the study (Fortschrittszeit).
    /// </summary>
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    /// Duration of this process step (calculated when the next entry is made).
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Optional override for the dimension value for this specific entry.
    /// </summary>
    public decimal? DimensionValue { get; set; }
}
