using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study definitions.
/// </summary>
public class DefinitionService
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true
    };

    private readonly SettingsService _settingsService;

    public DefinitionService(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
    }

    /// <summary>
    /// Gets the definitions directory path.
    /// </summary>
    private string DefinitionsDirectory => _settingsService.Settings.DefinitionsDirectory;

    /// <summary>
    /// Creates a new time study definition with default values.
    /// </summary>
    public TimeStudyDefinition CreateNew()
    {
        TimeStudyDefinition definition = new()
        {
            Id = Guid.NewGuid(),
            Name = "New Time Study",
            Description = string.Empty,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            ProcessSteps =
            [
                new ProcessStepDefinition
                {
                    OrderNumber = 1,
                    Description = "Default Step",
                    ProductDescription = string.Empty,
                    DimensionType = DimensionType.Count,
                    DimensionUnit = "pcs",
                    DefaultDimensionValue = 1,
                    IsDefaultStep = true
                }
            ],
            DefaultProcessStepOrderNumber = 1
        };

        return definition;
    }

    /// <summary>
    /// Saves a definition to disk.
    /// </summary>
    /// <param name="definition">The definition to save.</param>
    /// <param name="filePath">Optional specific file path. If null, uses default naming.</param>
    /// <returns>The path where the definition was saved.</returns>
    public string Save(TimeStudyDefinition definition, string? filePath = null)
    {
        ArgumentNullException.ThrowIfNull(definition);

        definition.ModifiedAt = DateTime.Now;

        _settingsService.Settings.EnsureDirectoriesExist();

        if (string.IsNullOrEmpty(filePath))
        {
            string safeName = GetSafeFileName(definition.Name);
            filePath = Path.Combine(
                DefinitionsDirectory,
                $"{safeName}_{definition.Id:N}.json");
        }

        string json = JsonSerializer.Serialize(definition, s_jsonOptions);
        File.WriteAllText(filePath, json);

        return filePath;
    }

    /// <summary>
    /// Loads a definition from a file.
    /// </summary>
    /// <param name="filePath">The file path to load from.</param>
    /// <returns>The loaded definition.</returns>
    public TimeStudyDefinition Load(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path is required.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Definition file not found.", filePath);
        }

        string json = File.ReadAllText(filePath);
        TimeStudyDefinition? definition = JsonSerializer.Deserialize<TimeStudyDefinition>(json, s_jsonOptions);

        return definition ?? throw new InvalidOperationException("Failed to deserialize definition.");
    }

    /// <summary>
    /// Gets all definitions in the definitions directory.
    /// </summary>
    /// <returns>List of definition summaries.</returns>
    public List<DefinitionSummary> GetAllDefinitions()
    {
        _settingsService.Settings.EnsureDirectoriesExist();

        List<DefinitionSummary> summaries = [];

        foreach (string file in Directory.GetFiles(DefinitionsDirectory, "*.json"))
        {
            try
            {
                TimeStudyDefinition definition = Load(file);
                summaries.Add(new DefinitionSummary
                {
                    Id = definition.Id,
                    Name = definition.Name,
                    Description = definition.Description,
                    IsLocked = definition.IsLocked,
                    ProcessStepCount = definition.ProcessSteps.Count,
                    ModifiedAt = definition.ModifiedAt,
                    FilePath = file
                });
            }
            catch
            {
                // Skip invalid files
            }
        }

        return summaries.OrderByDescending(s => s.ModifiedAt).ToList();
    }

    /// <summary>
    /// Deletes a definition file.
    /// </summary>
    /// <param name="filePath">The file path to delete.</param>
    public void Delete(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// Locks a definition (called when a recording is created).
    /// </summary>
    /// <param name="definition">The definition to lock.</param>
    /// <param name="filePath">The file path where the definition is stored.</param>
    public void LockDefinition(TimeStudyDefinition definition, string filePath)
    {
        ArgumentNullException.ThrowIfNull(definition);

        definition.IsLocked = true;
        Save(definition, filePath);
    }

    /// <summary>
    /// Creates a safe file name from a string.
    /// </summary>
    private static string GetSafeFileName(string name)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string safeName = new(name.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());

        return string.IsNullOrWhiteSpace(safeName) ? "Definition" : safeName;
    }
}

/// <summary>
/// Summary information about a definition for display in lists.
/// </summary>
public class DefinitionSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
    public int ProcessStepCount { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string FilePath { get; set; } = string.Empty;
}
