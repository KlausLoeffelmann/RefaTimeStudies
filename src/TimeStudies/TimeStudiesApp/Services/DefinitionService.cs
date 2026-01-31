using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
///  Service for managing time study definitions.
/// </summary>
public class DefinitionService
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly SettingsService _settingsService;

    /// <summary>
    ///  Initializes a new instance of the <see cref="DefinitionService"/> class.
    /// </summary>
    /// <param name="settingsService">The settings service instance.</param>
    public DefinitionService(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
    }

    /// <summary>
    ///  Gets the directory where definitions are stored.
    /// </summary>
    private string DefinitionsDirectory => _settingsService.Settings.DefinitionsDirectory;

    /// <summary>
    ///  Creates a new time study definition with default process steps.
    /// </summary>
    /// <returns>A new definition instance.</returns>
    public TimeStudyDefinition CreateNew()
    {
        var definition = new TimeStudyDefinition
        {
            Name = "New Definition",
            ProcessSteps =
            [
                new ProcessStepDefinition
                {
                    OrderNumber = 1,
                    Description = "Process Step 1",
                    DimensionType = DimensionType.Count,
                    DimensionUnit = "pcs",
                    DefaultDimensionValue = 1
                },
                new ProcessStepDefinition
                {
                    OrderNumber = 40,
                    Description = "Default Step",
                    DimensionType = DimensionType.Count,
                    IsDefaultStep = true
                }
            ]
        };

        return definition;
    }

    /// <summary>
    ///  Saves a definition to a JSON file.
    /// </summary>
    /// <param name="definition">The definition to save.</param>
    /// <param name="filePath">Optional specific file path. If null, uses default naming.</param>
    /// <returns>The path where the definition was saved.</returns>
    public string Save(TimeStudyDefinition definition, string? filePath = null)
    {
        ArgumentNullException.ThrowIfNull(definition);

        definition.ModifiedAt = DateTime.Now;

        if (string.IsNullOrEmpty(filePath))
        {
            string safeFileName = GetSafeFileName(definition.Name);
            filePath = Path.Combine(DefinitionsDirectory, $"{safeFileName}_{definition.Id}.json");
        }

        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(definition, s_jsonOptions);
        File.WriteAllText(filePath, json);

        return filePath;
    }

    /// <summary>
    ///  Loads a definition from a JSON file.
    /// </summary>
    /// <param name="filePath">The path to the definition file.</param>
    /// <returns>The loaded definition, or null if loading failed.</returns>
    public TimeStudyDefinition? Load(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TimeStudyDefinition>(json, s_jsonOptions);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading definition: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    ///  Gets all definition files in the definitions directory.
    /// </summary>
    /// <returns>A list of file paths to definition files.</returns>
    public List<string> GetAllDefinitionFiles()
    {
        if (!Directory.Exists(DefinitionsDirectory))
        {
            return [];
        }

        return Directory.GetFiles(DefinitionsDirectory, "*.json").ToList();
    }

    /// <summary>
    ///  Loads all definitions from the definitions directory.
    /// </summary>
    /// <returns>A list of all loaded definitions.</returns>
    public List<TimeStudyDefinition> LoadAll()
    {
        List<TimeStudyDefinition> definitions = [];

        foreach (string filePath in GetAllDefinitionFiles())
        {
            var definition = Load(filePath);
            if (definition is not null)
            {
                definitions.Add(definition);
            }
        }

        return definitions;
    }

    /// <summary>
    ///  Checks if a definition is locked (has recordings).
    /// </summary>
    /// <param name="definition">The definition to check.</param>
    /// <param name="recordingService">The recording service to check for recordings.</param>
    /// <returns>True if the definition has recordings.</returns>
    public bool CheckIfLocked(TimeStudyDefinition definition, RecordingService recordingService)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(recordingService);

        var recordings = recordingService.GetRecordingsForDefinition(definition.Id);
        bool isLocked = recordings.Count > 0;
        definition.IsLocked = isLocked;
        return isLocked;
    }

    /// <summary>
    ///  Creates a copy of a definition for editing.
    /// </summary>
    /// <param name="original">The original definition to copy.</param>
    /// <param name="newName">The name for the copy.</param>
    /// <returns>The copied definition.</returns>
    public TimeStudyDefinition CreateCopy(TimeStudyDefinition original, string? newName = null)
    {
        ArgumentNullException.ThrowIfNull(original);
        return original.CreateCopy(newName);
    }

    /// <summary>
    ///  Deletes a definition file.
    /// </summary>
    /// <param name="filePath">The path to the definition file.</param>
    /// <returns>True if deletion was successful.</returns>
    public bool Delete(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting definition: {ex.Message}");
        }

        return false;
    }

    /// <summary>
    ///  Creates a safe file name from a string by removing invalid characters.
    /// </summary>
    private static string GetSafeFileName(string name)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string safeName = string.Join("_", name.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        return string.IsNullOrWhiteSpace(safeName) ? "definition" : safeName;
    }
}
