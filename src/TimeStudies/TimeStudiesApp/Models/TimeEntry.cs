namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a single time entry during a recording session.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Auto-incrementing sequence number for this entry.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// Order number of the process step for this entry.
    /// </summary>
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Description of the process step (copied for reference).
    /// </summary>
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when this entry was recorded.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Cumulative elapsed time from the start of the recording (Fortschrittszeit).
    /// </summary>
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    /// Duration of this entry. Calculated when the next entry is made.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Override dimension value for this specific entry.
    /// If null, uses the default from the process step definition.
    /// </summary>
    public decimal? DimensionValue { get; set; }
}
