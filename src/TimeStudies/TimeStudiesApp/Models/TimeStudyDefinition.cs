namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a time study definition (Zeitaufnahme-Definition) - a template defining what process steps to measure.
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
    /// Optional description of what this time study measures.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when this definition was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Date and time when this definition was last modified.
    /// </summary>
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// True if recordings exist for this definition. Locked definitions cannot be edited.
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// List of process steps in this definition.
    /// </summary>
    public List<ProcessStepDefinition> ProcessSteps { get; set; } = [];

    /// <summary>
    /// The order number of the default process step.
    /// </summary>
    public int DefaultProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Gets the default process step, or null if not set.
    /// </summary>
    public ProcessStepDefinition? GetDefaultProcessStep()
    {
        return ProcessSteps.FirstOrDefault(s => s.IsDefaultStep)
            ?? ProcessSteps.FirstOrDefault(s => s.OrderNumber == DefaultProcessStepOrderNumber);
    }

    /// <summary>
    /// Sets the specified step as the default and clears the flag from all others.
    /// </summary>
    public void SetDefaultProcessStep(int orderNumber)
    {
        foreach (var step in ProcessSteps)
        {
            step.IsDefaultStep = step.OrderNumber == orderNumber;
        }
        DefaultProcessStepOrderNumber = orderNumber;
    }

    /// <summary>
    /// Creates a deep copy of this definition with a new ID.
    /// The copy is not locked.
    /// </summary>
    public TimeStudyDefinition CreateCopy()
    {
        return new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = Name + " (Copy)",
            Description = Description,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            ProcessSteps = ProcessSteps.Select(s => s.Clone()).ToList(),
            DefaultProcessStepOrderNumber = DefaultProcessStepOrderNumber
        };
    }

    /// <summary>
    /// Validates the definition and returns any validation errors.
    /// </summary>
    public List<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add("Definition name is required.");
        }

        if (ProcessSteps.Count == 0)
        {
            errors.Add("At least one process step is required.");
        }

        var duplicateOrderNumbers = ProcessSteps
            .GroupBy(s => s.OrderNumber)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateOrderNumbers.Count > 0)
        {
            errors.Add($"Duplicate order numbers: {string.Join(", ", duplicateOrderNumbers)}");
        }

        var defaultSteps = ProcessSteps.Count(s => s.IsDefaultStep);
        if (defaultSteps == 0)
        {
            errors.Add("A default process step must be designated.");
        }
        else if (defaultSteps > 1)
        {
            errors.Add("Only one process step can be the default.");
        }

        return errors;
    }
}
