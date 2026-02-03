using System.IO;
using System.Text.Json;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Manages time study definitions stored as JSON files.
/// </summary>
public class DefinitionService : IDefinitionService
{
    private readonly ISettingsService _settingsService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
    };

    public DefinitionService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    private string DefinitionsDirectory => Path.Combine(
        _settingsService.Current.BaseDirectory, "Definitions");

    /// <summary>
    /// Gets all available definitions.
    /// </summary>
    public async Task<IReadOnlyList<TimeStudyDefinition>> GetAllAsync()
    {
        var definitions = new List<TimeStudyDefinition>();

        if (!Directory.Exists(DefinitionsDirectory))
            return definitions;

        foreach (var file in Directory.GetFiles(DefinitionsDirectory, "*.json"))
        {
            try
            {
                var json = await File.ReadAllTextAsync(file);
                var definition = JsonSerializer.Deserialize<TimeStudyDefinition>(json, JsonOptions);
                if (definition is not null)
                {
                    definitions.Add(definition);
                }
            }
            catch
            {
                // Skip invalid files
            }
        }

        return definitions.OrderByDescending(d => d.ModifiedAt).ToList();
    }

    /// <summary>
    /// Loads a definition by its ID.
    /// </summary>
    public async Task<TimeStudyDefinition?> LoadAsync(Guid id)
    {
        if (!Directory.Exists(DefinitionsDirectory))
            return null;

        foreach (var file in Directory.GetFiles(DefinitionsDirectory, "*.json"))
        {
            try
            {
                var json = await File.ReadAllTextAsync(file);
                var definition = JsonSerializer.Deserialize<TimeStudyDefinition>(json, JsonOptions);
                if (definition?.Id == id)
                {
                    return definition;
                }
            }
            catch
            {
                // Skip invalid files
            }
        }

        return null;
    }

    /// <summary>
    /// Saves a definition to disk.
    /// </summary>
    public async Task SaveAsync(TimeStudyDefinition definition)
    {
        Directory.CreateDirectory(DefinitionsDirectory);

        definition.ModifiedAt = DateTime.Now;

        // Clean filename
        var safeName = string.Join("_", definition.Name.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{safeName}_{definition.Id}.json";
        var filePath = Path.Combine(DefinitionsDirectory, fileName);

        // Remove old file if name changed
        var existingFiles = Directory.GetFiles(DefinitionsDirectory, $"*_{definition.Id}.json");
        foreach (var oldFile in existingFiles)
        {
            if (!oldFile.Equals(filePath, StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(oldFile);
            }
        }

        var json = JsonSerializer.Serialize(definition, JsonOptions);
        await File.WriteAllTextAsync(filePath, json);
    }

    /// <summary>
    /// Deletes a definition from disk.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        if (!Directory.Exists(DefinitionsDirectory))
            return;

        var files = Directory.GetFiles(DefinitionsDirectory, $"*_{id}.json");
        foreach (var file in files)
        {
            File.Delete(file);
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Creates a copy of a definition (for locked definitions).
    /// </summary>
    public TimeStudyDefinition CreateCopy(TimeStudyDefinition original)
    {
        return new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = $"{original.Name} (Copy)",
            Description = original.Description,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            DefaultProcessStepOrderNumber = original.DefaultProcessStepOrderNumber,
            ProcessSteps = original.ProcessSteps.Select(ps => new ProcessStepDefinition
            {
                OrderNumber = ps.OrderNumber,
                Description = ps.Description,
                ProductDescription = ps.ProductDescription,
                DimensionType = ps.DimensionType,
                DimensionUnit = ps.DimensionUnit,
                DefaultDimensionValue = ps.DefaultDimensionValue,
                IsDefaultStep = ps.IsDefaultStep
            }).ToList()
        };
    }
}
