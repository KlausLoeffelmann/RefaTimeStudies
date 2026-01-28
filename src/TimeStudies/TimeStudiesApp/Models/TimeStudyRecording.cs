namespace TimeStudiesApp.Models;

/// <summary>
/// Represents an actual time study recording session.
/// </summary>
public class TimeStudyRecording
{
    /// <summary>
    /// Unique identifier for this recording.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// ID of the definition used for this recording.
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Name of the definition (copied for reference).
    /// </summary>
    public string DefinitionName { get; set; } = string.Empty;

    /// <summary>
    /// When this recording was started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// When this recording was completed (null if still in progress).
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Indicates whether this recording is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// List of time entries in this recording.
    /// </summary>
    public List<TimeEntry> Entries { get; set; } = [];

    /// <summary>
    /// Generates a filename for this recording.
    /// </summary>
    public string GenerateFileName() =>
        $"{SanitizeFileName(DefinitionName)}_{Id:N}_{StartedAt:yyyyMMdd_HHmmss}.json";

    private static string SanitizeFileName(string name)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string sanitized = new(name.Where(c => !invalidChars.Contains(c)).ToArray());
        return string.IsNullOrWhiteSpace(sanitized) ? "Recording" : sanitized;
    }

    /// <summary>
    /// Gets summary statistics grouped by process step.
    /// </summary>
    public IEnumerable<ProcessStepSummary> GetSummary()
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
/// Summary statistics for a single process step.
/// </summary>
public class ProcessStepSummary
{
    public int OrderNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Count { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public TimeSpan AverageDuration { get; set; }
}
