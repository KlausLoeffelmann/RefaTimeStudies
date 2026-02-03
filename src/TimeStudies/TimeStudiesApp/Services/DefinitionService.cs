using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study definitions.
/// </summary>
public class DefinitionService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Creates a new time study definition with default values.
    /// </summary>
    public TimeStudyDefinition CreateNew()
    {
        var definition = new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = "New Definition",
            Description = string.Empty,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            ProcessSteps = new List<ProcessStepDefinition>
            {
                new ProcessStepDefinition
                {
                    OrderNumber = 40,
                    Description = "Default Step",
                    ProductDescription = string.Empty,
                    DimensionType = DimensionType.Count,
                    DimensionUnit = "pieces",
                    DefaultDimensionValue = 1,
                    IsDefaultStep = true
                }
            },
            DefaultProcessStepOrderNumber = 40
        };

        return definition;
    }

    /// <summary>
    /// Loads a time study definition from a JSON file.
    /// </summary>
    public TimeStudyDefinition? Load(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TimeStudyDefinition>(json, JsonOptions);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Saves a time study definition to a JSON file.
    /// </summary>
    public void Save(TimeStudyDefinition definition, string filePath)
    {
        definition.ModifiedAt = DateTime.Now;

        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(definition, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Creates a copy of an existing definition with a new ID.
    /// </summary>
    public TimeStudyDefinition CreateCopy(TimeStudyDefinition original)
    {
        return original.CreateCopy();
    }

    /// <summary>
    /// Gets the default file path for a definition based on its name and ID.
    /// </summary>
    public string GetDefaultFilePath(TimeStudyDefinition definition, string baseDirectory)
    {
        string safeName = GetSafeFileName(definition.Name);
        string fileName = $"{safeName}_{definition.Id:N}.json";
        return Path.Combine(baseDirectory, "Definitions", fileName);
    }

    /// <summary>
    /// Checks if any recordings exist for a given definition.
    /// </summary>
    public bool HasRecordings(Guid definitionId, string recordingsDirectory)
    {
        if (!Directory.Exists(recordingsDirectory))
            return false;

        string searchPattern = $"*_{definitionId:N}_*.json";
        return Directory.GetFiles(recordingsDirectory, searchPattern).Length > 0;
    }

    /// <summary>
    /// Gets all definition files from a directory.
    /// </summary>
    public IEnumerable<string> GetDefinitionFiles(string definitionsDirectory)
    {
        if (!Directory.Exists(definitionsDirectory))
            return Enumerable.Empty<string>();

        return Directory.GetFiles(definitionsDirectory, "*.json")
            .OrderByDescending(f => File.GetLastWriteTime(f));
    }

    /// <summary>
    /// Validates a definition before saving.
    /// </summary>
    public (bool IsValid, string? ErrorMessage) Validate(TimeStudyDefinition definition)
    {
        if (string.IsNullOrWhiteSpace(definition.Name))
            return (false, "Definition name is required.");

        if (definition.ProcessSteps.Count == 0)
            return (false, "At least one process step is required.");

        int defaultCount = definition.ProcessSteps.Count(s => s.IsDefaultStep);
        if (defaultCount == 0)
            return (false, "A default process step must be designated.");
        if (defaultCount > 1)
            return (false, "Only one process step can be the default.");

        var duplicateOrders = definition.ProcessSteps
            .GroupBy(s => s.OrderNumber)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateOrders.Count > 0)
            return (false, $"Duplicate order numbers found: {string.Join(", ", duplicateOrders)}");

        return (true, null);
    }

    private static string GetSafeFileName(string name)
    {
        char[] invalid = Path.GetInvalidFileNameChars();
        string safe = new string(name.Select(c => invalid.Contains(c) ? '_' : c).ToArray());
        return safe.Length > 50 ? safe[..50] : safe;
    }
}
