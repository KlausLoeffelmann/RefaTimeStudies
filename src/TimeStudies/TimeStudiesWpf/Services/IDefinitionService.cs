using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Interface for managing time study definitions.
/// </summary>
public interface IDefinitionService
{
    /// <summary>
    /// Gets all available definitions.
    /// </summary>
    Task<IReadOnlyList<TimeStudyDefinition>> GetAllAsync();

    /// <summary>
    /// Loads a definition by its ID.
    /// </summary>
    Task<TimeStudyDefinition?> LoadAsync(Guid id);

    /// <summary>
    /// Saves a definition to disk.
    /// </summary>
    Task SaveAsync(TimeStudyDefinition definition);

    /// <summary>
    /// Deletes a definition from disk.
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Creates a copy of a definition (for locked definitions).
    /// </summary>
    TimeStudyDefinition CreateCopy(TimeStudyDefinition original);
}
