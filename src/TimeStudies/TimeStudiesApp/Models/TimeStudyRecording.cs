namespace TimeStudiesApp.Models;

/// <summary>
/// Represents an actual time study recording session (Zeitaufnahme).
/// </summary>
public class TimeStudyRecording
{
    /// <summary>
    /// Unique identifier for this recording.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The ID of the definition this recording is based on.
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Name of the definition (denormalized for convenience).
    /// </summary>
    public string DefinitionName { get; set; } = string.Empty;

    /// <summary>
    /// When the recording was started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// When the recording was completed (null if still in progress).
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Whether the recording has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// List of time entries in this recording.
    /// </summary>
    public List<TimeEntry> Entries { get; set; } = [];

    /// <summary>
    /// Gets the total elapsed time of the recording.
    /// </summary>
    public TimeSpan GetTotalDuration()
    {
        if (Entries.Count == 0)
            return TimeSpan.Zero;

        return Entries.Max(e => e.ElapsedFromStart + e.Duration);
    }

    /// <summary>
    /// Gets a summary of time spent on each process step.
    /// </summary>
    public Dictionary<int, TimeSpan> GetStepSummary()
    {
        return Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .ToDictionary(
                g => g.Key,
                g => TimeSpan.FromTicks(g.Sum(e => e.Duration.Ticks)));
    }

    /// <summary>
    /// Gets the count of entries for each process step.
    /// </summary>
    public Dictionary<int, int> GetStepCounts()
    {
        return Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .ToDictionary(
                g => g.Key,
                g => g.Count());
    }
}
