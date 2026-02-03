namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a single recorded time entry during a time study recording.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Auto-incrementing sequence number within the recording.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// Order number of the process step that was active.
    /// </summary>
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Description of the process step (cached for export/display).
    /// </summary>
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    /// Absolute timestamp when this entry was recorded.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Cumulative time from study start (Fortschrittszeit / Progressive Time).
    /// </summary>
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    /// Duration of this step. Calculated when the next entry is made.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Optional override value for the dimension (e.g., piece count).
    /// If null, the default value from the process step definition is used.
    /// </summary>
    public decimal? DimensionValue { get; set; }

    /// <summary>
    /// Creates a copy of this time entry.
    /// </summary>
    public TimeEntry Clone()
    {
        return new TimeEntry
        {
            SequenceNumber = SequenceNumber,
            ProcessStepOrderNumber = ProcessStepOrderNumber,
            ProcessStepDescription = ProcessStepDescription,
            Timestamp = Timestamp,
            ElapsedFromStart = ElapsedFromStart,
            Duration = Duration,
            DimensionValue = DimensionValue
        };
    }
}
