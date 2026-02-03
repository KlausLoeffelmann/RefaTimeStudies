using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Interface for managing time study recordings.
/// </summary>
public interface IRecordingService
{
    /// <summary>
    /// Gets all recordings for a definition.
    /// </summary>
    Task<IReadOnlyList<TimeStudyRecording>> GetForDefinitionAsync(Guid definitionId);

    /// <summary>
    /// Gets all recordings.
    /// </summary>
    Task<IReadOnlyList<TimeStudyRecording>> GetAllAsync();

    /// <summary>
    /// Loads a recording by its ID.
    /// </summary>
    Task<TimeStudyRecording?> LoadAsync(Guid id);

    /// <summary>
    /// Saves a recording to disk.
    /// </summary>
    Task SaveAsync(TimeStudyRecording recording);

    /// <summary>
    /// Deletes a recording from disk.
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Checks if any recordings exist for a definition.
    /// </summary>
    Task<bool> HasRecordingsAsync(Guid definitionId);
}
