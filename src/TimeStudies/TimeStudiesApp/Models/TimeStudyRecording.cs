using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Represents an actual time study recording session (Zeitaufnahme).
/// </summary>
public class TimeStudyRecording
{
    /// <summary>
    /// Unique identifier for this recording.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Reference to the definition used for this recording.
    /// </summary>
    [JsonPropertyName("definitionId")]
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Name of the definition (denormalized for display).
    /// </summary>
    [JsonPropertyName("definitionName")]
    public string DefinitionName { get; set; } = string.Empty;

    /// <summary>
    /// When the recording was started.
    /// </summary>
    [JsonPropertyName("startedAt")]
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// When the recording was completed (null if still in progress).
    /// </summary>
    [JsonPropertyName("completedAt")]
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// True if the recording has been completed.
    /// </summary>
    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }

    /// <summary>
    /// List of time entries in this recording.
    /// </summary>
    [JsonPropertyName("entries")]
    public List<TimeEntry> Entries { get; set; } = [];

    /// <summary>
    /// Gets the total duration of the recording.
    /// </summary>
    [JsonIgnore]
    public TimeSpan TotalDuration
    {
        get
        {
            if (Entries.Count == 0)
            {
                return TimeSpan.Zero;
            }

            return Entries[^1].ElapsedFromStart;
        }
    }

    /// <summary>
    /// Gets the next sequence number for a new entry.
    /// </summary>
    [JsonIgnore]
    public int NextSequenceNumber => Entries.Count + 1;

    /// <summary>
    /// Adds a new time entry to the recording.
    /// </summary>
    /// <param name="processStep">The process step for this entry.</param>
    /// <param name="elapsedFromStart">Time elapsed since recording start.</param>
    /// <param name="dimensionValue">Optional dimension value override.</param>
    public void AddEntry(ProcessStepDefinition processStep, TimeSpan elapsedFromStart, decimal? dimensionValue = null)
    {
        // Calculate duration of previous entry
        if (Entries.Count > 0)
        {
            TimeEntry previousEntry = Entries[^1];
            previousEntry.Duration = elapsedFromStart - previousEntry.ElapsedFromStart;
        }

        TimeEntry entry = new()
        {
            SequenceNumber = NextSequenceNumber,
            ProcessStepOrderNumber = processStep.OrderNumber,
            ProcessStepDescription = processStep.Description,
            Timestamp = DateTime.Now,
            ElapsedFromStart = elapsedFromStart,
            DimensionValue = dimensionValue ?? processStep.DefaultDimensionValue
        };

        Entries.Add(entry);
    }

    /// <summary>
    /// Completes the recording.
    /// </summary>
    public void Complete()
    {
        CompletedAt = DateTime.Now;
        IsCompleted = true;
    }

    /// <summary>
    /// Gets a summary of time entries grouped by process step.
    /// </summary>
    public IEnumerable<ProcessStepSummary> GetSummary()
    {
        return Entries
            .GroupBy(e => new { e.ProcessStepOrderNumber, e.ProcessStepDescription })
            .Select(g => new ProcessStepSummary
            {
                OrderNumber = g.Key.ProcessStepOrderNumber,
                Description = g.Key.ProcessStepDescription,
                EntryCount = g.Count(),
                TotalDuration = TimeSpan.FromTicks(g.Sum(e => e.Duration.Ticks)),
                AverageDuration = TimeSpan.FromTicks((long)g.Average(e => e.Duration.Ticks))
            })
            .OrderBy(s => s.OrderNumber);
    }
}

/// <summary>
/// Summary of time entries for a single process step.
/// </summary>
public class ProcessStepSummary
{
    public int OrderNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public int EntryCount { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public TimeSpan AverageDuration { get; set; }
}
