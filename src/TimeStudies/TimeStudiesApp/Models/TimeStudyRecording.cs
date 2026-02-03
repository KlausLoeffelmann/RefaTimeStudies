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
    /// Reference to the definition this recording is based on.
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Cached name of the definition (for display/export without loading the definition).
    /// </summary>
    public string DefinitionName { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the recording was started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Date and time when the recording was completed. Null if still in progress.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// True if the recording has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// List of time entries recorded during this session.
    /// </summary>
    public List<TimeEntry> Entries { get; set; } = new();

    /// <summary>
    /// Gets the total duration of the recording.
    /// </summary>
    public TimeSpan TotalDuration
    {
        get
        {
            if (Entries.Count == 0)
                return TimeSpan.Zero;

            return Entries[^1].ElapsedFromStart;
        }
    }

    /// <summary>
    /// Gets the total number of entries.
    /// </summary>
    public int EntryCount => Entries.Count;

    /// <summary>
    /// Gets summary statistics grouped by process step.
    /// </summary>
    public IEnumerable<ProcessStepSummary> GetSummaryByStep()
    {
        return Entries
            .GroupBy(e => new { e.ProcessStepOrderNumber, e.ProcessStepDescription })
            .Select(g => new ProcessStepSummary
            {
                OrderNumber = g.Key.ProcessStepOrderNumber,
                Description = g.Key.ProcessStepDescription,
                Count = g.Count(),
                TotalDuration = TimeSpan.FromTicks(g.Sum(e => e.Duration.Ticks)),
                AverageDuration = TimeSpan.FromTicks((long)g.Average(e => e.Duration.Ticks))
            })
            .OrderBy(s => s.OrderNumber);
    }
}

/// <summary>
/// Summary statistics for a single process step in a recording.
/// </summary>
public class ProcessStepSummary
{
    /// <summary>
    /// Order number of the process step.
    /// </summary>
    public int OrderNumber { get; set; }

    /// <summary>
    /// Description of the process step.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Number of times this step was recorded.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Total duration spent on this step.
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    /// Average duration per occurrence.
    /// </summary>
    public TimeSpan AverageDuration { get; set; }
}
