namespace TimeStudiesWpf.Models;

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
    /// Reference to the definition used for this recording.
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Name of the definition (copied for reference).
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
    /// True if the recording has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// All time entries recorded in this session.
    /// </summary>
    public List<TimeEntry> Entries { get; set; } = new();

    /// <summary>
    /// Gets the total duration of the recording.
    /// </summary>
    public TimeSpan TotalDuration => Entries.Count > 0
        ? Entries[^1].ElapsedFromStart + Entries[^1].Duration
        : TimeSpan.Zero;
}
