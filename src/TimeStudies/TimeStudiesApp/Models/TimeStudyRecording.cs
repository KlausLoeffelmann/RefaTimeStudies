namespace TimeStudiesApp.Models;

/// <summary>
///  Represents an actual recording session (Zeitaufnahme) using a definition.
/// </summary>
public class TimeStudyRecording
{
    /// <summary>
    ///  Gets or sets the unique identifier for this recording.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///  Gets or sets the ID of the definition used for this recording.
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    ///  Gets or sets the name of the definition (stored for reference).
    /// </summary>
    public string DefinitionName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the timestamp when this recording was started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    ///  Gets or sets the timestamp when this recording was completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether this recording is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    ///  Gets or sets the list of time entries recorded during this session.
    /// </summary>
    public List<TimeEntry> Entries { get; set; } = [];

    /// <summary>
    ///  Gets the total duration of the recording.
    /// </summary>
    public TimeSpan TotalDuration =>
        Entries.Count > 0
            ? Entries.Max(e => e.ElapsedFromStart + e.Duration)
            : TimeSpan.Zero;

    /// <summary>
    ///  Gets a summary of time spent on each process step.
    /// </summary>
    /// <returns>Dictionary of order number to summary data.</returns>
    public Dictionary<int, ProcessStepSummary> GetSummaryByProcessStep()
    {
        return Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .ToDictionary(
                g => g.Key,
                g => new ProcessStepSummary
                {
                    OrderNumber = g.Key,
                    Description = g.First().ProcessStepDescription,
                    Count = g.Count(),
                    TotalDuration = TimeSpan.FromTicks(g.Sum(e => e.Duration.Ticks)),
                    AverageDuration = TimeSpan.FromTicks((long)g.Average(e => e.Duration.Ticks))
                });
    }
}

/// <summary>
///  Summary statistics for a single process step within a recording.
/// </summary>
public class ProcessStepSummary
{
    /// <summary>
    ///  Gets or sets the order number of the process step.
    /// </summary>
    public int OrderNumber { get; set; }

    /// <summary>
    ///  Gets or sets the description of the process step.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the number of times this step was recorded.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    ///  Gets or sets the total duration spent on this step.
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    ///  Gets or sets the average duration per occurrence.
    /// </summary>
    public TimeSpan AverageDuration { get; set; }
}
