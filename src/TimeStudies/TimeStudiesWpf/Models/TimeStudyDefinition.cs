namespace TimeStudiesWpf.Models;

/// <summary>
/// Defines a time study template (Zeitaufnahme-Definition) containing process steps.
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
    /// Description of what this time study measures.
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
    /// True if recordings exist for this definition (cannot be edited).
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// The process steps that make up this time study.
    /// </summary>
    public List<ProcessStepDefinition> ProcessSteps { get; set; } = new();

    /// <summary>
    /// Order number of the default process step.
    /// </summary>
    public int DefaultProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Gets the default process step, or null if not found.
    /// </summary>
    public ProcessStepDefinition? GetDefaultStep()
    {
        return ProcessSteps.FirstOrDefault(ps => ps.IsDefaultStep)
            ?? ProcessSteps.FirstOrDefault(ps => ps.OrderNumber == DefaultProcessStepOrderNumber);
    }
}
