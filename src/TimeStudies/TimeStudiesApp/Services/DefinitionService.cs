using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study definitions.
/// </summary>
public class DefinitionService
{
    private readonly SettingsService _settingsService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public DefinitionService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// Gets the directory where definitions are stored.
    /// </summary>
    private string DefinitionsDirectory => _settingsService.CurrentSettings.DefinitionsDirectory;

    /// <summary>
    /// Creates a new time study definition with default values.
    /// </summary>
    public TimeStudyDefinition CreateNew()
    {
        var definition = new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = "New Time Study",
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            ProcessSteps =
            [
                new ProcessStepDefinition
                {
                    OrderNumber = 40,
                    Description = "Default Step",
                    DimensionType = DimensionType.Count,
                    DimensionUnit = "pcs",
                    DefaultDimensionValue = 1,
                    IsDefaultStep = true
                }
            ],
            DefaultProcessStepOrderNumber = 40
        };

        return definition;
    }

    /// <summary>
    /// Saves a definition to disk.
    /// </summary>
    public void Save(TimeStudyDefinition definition, string? filePath = null)
    {
        definition.ModifiedAt = DateTime.Now;

        _settingsService.CurrentSettings.EnsureDirectoriesExist();

        filePath ??= GetDefaultFilePath(definition);

        var json = JsonSerializer.Serialize(definition, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Loads a definition from a file.
    /// </summary>
    public TimeStudyDefinition? Load(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<TimeStudyDefinition>(json, JsonOptions);
    }

    /// <summary>
    /// Gets all definitions from the definitions directory.
    /// </summary>
    public List<TimeStudyDefinition> GetAll()
    {
        var definitions = new List<TimeStudyDefinition>();

        _settingsService.CurrentSettings.EnsureDirectoriesExist();

        var files = Directory.GetFiles(DefinitionsDirectory, "*.json");
        foreach (var file in files)
        {
            try
            {
                var definition = Load(file);
                if (definition != null)
                {
                    definitions.Add(definition);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading definition {file}: {ex.Message}");
            }
        }

        return definitions.OrderBy(d => d.Name).ToList();
    }

    /// <summary>
    /// Gets the default file path for a definition.
    /// </summary>
    public string GetDefaultFilePath(TimeStudyDefinition definition)
    {
        var safeName = string.Join("_", definition.Name.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{safeName}_{definition.Id:N}.json";
        return Path.Combine(DefinitionsDirectory, fileName);
    }

    /// <summary>
    /// Deletes a definition file.
    /// </summary>
    public void Delete(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// Creates a copy of a definition (useful for locked definitions).
    /// </summary>
    public TimeStudyDefinition CreateCopy(TimeStudyDefinition original)
    {
        return original.CreateCopy();
    }

    /// <summary>
    /// Checks if any recordings exist for the given definition.
    /// </summary>
    public bool HasRecordings(Guid definitionId)
    {
        var recordingsDir = _settingsService.CurrentSettings.RecordingsDirectory;
        if (!Directory.Exists(recordingsDir))
            return false;

        var files = Directory.GetFiles(recordingsDir, "*.json");
        foreach (var file in files)
        {
            try
            {
                var json = File.ReadAllText(file);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("definitionId", out var defIdElement))
                {
                    if (Guid.TryParse(defIdElement.GetString(), out var defId) && defId == definitionId)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // Ignore invalid files
            }
        }

        return false;
    }

    /// <summary>
    /// Locks a definition (sets IsLocked = true and saves).
    /// </summary>
    public void Lock(TimeStudyDefinition definition, string? filePath = null)
    {
        definition.IsLocked = true;
        Save(definition, filePath);
    }
}
