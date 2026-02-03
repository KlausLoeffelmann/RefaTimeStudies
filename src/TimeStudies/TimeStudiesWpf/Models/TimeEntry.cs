namespace TimeStudiesWpf.Models;

/// <summary>
/// A single time entry recorded during a time study session.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Auto-incrementing sequence number within the recording.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// Order number of the process step this entry belongs to.
    /// </summary>
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Description of the process step (copied for reference).
    /// </summary>
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    /// Actual wall-clock time when this entry was recorded.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Progressive time (Fortschrittszeit) - elapsed time from study start.
    /// </summary>
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    /// Duration of this entry (calculated when the next entry is made).
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Optional override of the dimension value for this specific entry.
    /// </summary>
    public decimal? DimensionValue { get; set; }
}
