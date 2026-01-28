using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a time study definition template (Zeitaufnahme-Definition).
/// Defines what process steps to measure.
/// </summary>
public class TimeStudyDefinition
{
    /// <summary>
    /// Unique identifier for this definition.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the time study definition.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the time study.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// When this definition was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// When this definition was last modified.
    /// </summary>
    [JsonPropertyName("modifiedAt")]
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// True if recordings exist for this definition.
    /// Locked definitions cannot be edited.
    /// </summary>
    [JsonPropertyName("isLocked")]
    public bool IsLocked { get; set; }

    /// <summary>
    /// List of process steps in this definition.
    /// </summary>
    [JsonPropertyName("processSteps")]
    public List<ProcessStepDefinition> ProcessSteps { get; set; } = [];

    /// <summary>
    /// Order number of the default (catch-all) process step.
    /// </summary>
    [JsonPropertyName("defaultProcessStepOrderNumber")]
    public int DefaultProcessStepOrderNumber { get; set; }

    /// <summary>
    /// Gets the default process step.
    /// </summary>
    [JsonIgnore]
    public ProcessStepDefinition? DefaultProcessStep =>
        ProcessSteps.FirstOrDefault(s => s.IsDefaultStep);

    /// <summary>
    /// Creates a deep copy of this definition with a new ID.
    /// The copy is unlocked and can be edited.
    /// </summary>
    public TimeStudyDefinition CreateCopy()
    {
        return new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = $"{Name} (Copy)",
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
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add("Definition name is required.");
        }

        if (ProcessSteps.Count == 0)
        {
            errors.Add("At least one process step is required.");
        }

        int defaultStepCount = ProcessSteps.Count(s => s.IsDefaultStep);
        if (defaultStepCount == 0)
        {
            errors.Add("Exactly one default process step must be designated.");
        }
        else if (defaultStepCount > 1)
        {
            errors.Add("Only one process step can be the default step.");
        }

        HashSet<int> orderNumbers = [];
        foreach (ProcessStepDefinition step in ProcessSteps)
        {
            if (!orderNumbers.Add(step.OrderNumber))
            {
                errors.Add($"Duplicate order number: {step.OrderNumber}");
            }
        }

        return errors;
    }
}
