namespace TimeStudiesApp.Models;

/// <summary>
///  Represents a single time entry recorded during a time study session.
/// </summary>
public class TimeEntry
{
    /// <summary>
    ///  Gets or sets the auto-incrementing sequence number for this entry.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    ///  Gets or sets the order number of the process step for this entry.
    /// </summary>
    public int ProcessStepOrderNumber { get; set; }

    /// <summary>
    ///  Gets or sets the description of the process step (stored for reference).
    /// </summary>
    public string ProcessStepDescription { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the timestamp when this entry was recorded.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///  Gets or sets the elapsed time from study start (Fortschrittszeit/Progressive Time).
    /// </summary>
    public TimeSpan ElapsedFromStart { get; set; }

    /// <summary>
    ///  Gets or sets the duration of this entry.
    ///  This is calculated when the next entry is made.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    ///  Gets or sets the dimension value for this entry.
    ///  Can override the default value from the process step definition.
    /// </summary>
    public decimal? DimensionValue { get; set; }
}
