namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a time study definition template that defines what process steps to measure.
/// </summary>
public class TimeStudyDefinition
{
    /// <summary>
    /// Unique identifier for this definition.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the time study definition.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the time study.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// When this definition was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// When this definition was last modified.
    /// </summary>
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Indicates whether this definition is locked because recordings exist.
    /// Locked definitions cannot be edited but can be copied.
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// The order number of the default process step.
    /// </summary>
    public int DefaultProcessStepOrderNumber { get; set; }

    /// <summary>
    /// List of process steps in this definition.
    /// </summary>
    public List<ProcessStepDefinition> ProcessSteps { get; set; } = [];

    /// <summary>
    /// Gets the default process step.
    /// </summary>
    public ProcessStepDefinition? GetDefaultStep() =>
        ProcessSteps.FirstOrDefault(s => s.IsDefaultStep) ??
        ProcessSteps.FirstOrDefault(s => s.OrderNumber == DefaultProcessStepOrderNumber);

    /// <summary>
    /// Creates a deep copy of this definition with a new ID.
    /// The copy is not locked.
    /// </summary>
    public TimeStudyDefinition CreateCopy()
    {
        var copy = new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = Name + " (Copy)",
            Description = Description,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            DefaultProcessStepOrderNumber = DefaultProcessStepOrderNumber,
            ProcessSteps = ProcessSteps.Select(s => s.Clone()).ToList()
        };

        return copy;
    }

    /// <summary>
    /// Generates a filename for this definition.
    /// </summary>
    public string GenerateFileName() =>
        $"{SanitizeFileName(Name)}_{Id:N}.json";

    private static string SanitizeFileName(string name)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string sanitized = new(name.Where(c => !invalidChars.Contains(c)).ToArray());
        return string.IsNullOrWhiteSpace(sanitized) ? "Definition" : sanitized;
    }
}
