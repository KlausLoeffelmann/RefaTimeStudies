namespace TimeStudiesApp.Models;

/// <summary>
///  Represents a time study definition (Zeitaufnahme-Definition) that defines
///  what process steps to measure during a recording session.
/// </summary>
public class TimeStudyDefinition
{
    /// <summary>
    ///  Gets or sets the unique identifier for this definition.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///  Gets or sets the name of this time study definition.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the description of this time study definition.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the date and time when this definition was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    ///  Gets or sets the date and time when this definition was last modified.
    /// </summary>
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    /// <summary>
    ///  Gets or sets a value indicating whether this definition is locked.
    ///  A definition becomes locked when recordings exist for it.
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    ///  Gets or sets the list of process steps in this definition.
    /// </summary>
    public List<ProcessStepDefinition> ProcessSteps { get; set; } = [];

    /// <summary>
    ///  Gets the order number of the default process step.
    /// </summary>
    public int? DefaultProcessStepOrderNumber =>
        ProcessSteps.FirstOrDefault(p => p.IsDefaultStep)?.OrderNumber;

    /// <summary>
    ///  Gets the default process step definition.
    /// </summary>
    public ProcessStepDefinition? DefaultProcessStep =>
        ProcessSteps.FirstOrDefault(p => p.IsDefaultStep);

    /// <summary>
    ///  Creates a deep copy of this definition with a new ID.
    /// </summary>
    /// <param name="newName">Optional new name for the copy.</param>
    /// <returns>A new unlocked definition with copied data.</returns>
    public TimeStudyDefinition CreateCopy(string? newName = null)
    {
        return new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = newName ?? $"{Name} (Copy)",
            Description = Description,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            ProcessSteps = ProcessSteps.Select(p => p.Clone()).ToList()
        };
    }

    /// <summary>
    ///  Validates the definition and returns any validation errors.
    /// </summary>
    /// <returns>A list of validation error messages, empty if valid.</returns>
    public List<string> Validate()
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add("Definition name is required.");
        }

        if (ProcessSteps.Count == 0)
        {
            errors.Add("At least one process step is required.");
        }

        int defaultStepCount = ProcessSteps.Count(p => p.IsDefaultStep);
        if (defaultStepCount == 0)
        {
            errors.Add("Exactly one default process step is required.");
        }
        else if (defaultStepCount > 1)
        {
            errors.Add("Only one process step can be marked as default.");
        }

        var duplicateOrderNumbers = ProcessSteps
            .GroupBy(p => p.OrderNumber)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateOrderNumbers.Count > 0)
        {
            errors.Add($"Duplicate order numbers: {string.Join(", ", duplicateOrderNumbers)}");
        }

        return errors;
    }
}
