using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study definitions.
/// </summary>
public sealed class DefinitionService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly SettingsService _settingsService;

    public DefinitionService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// Creates a new time study definition with default process steps.
    /// </summary>
    public TimeStudyDefinition CreateNew()
    {
        TimeStudyDefinition definition = new()
        {
            Name = "New Time Study",
            Description = string.Empty,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            ProcessSteps =
            [
                new ProcessStepDefinition
                {
                    OrderNumber = 1,
                    Description = "Process Step 1",
                    DimensionType = DimensionType.Count,
                    DimensionUnit = "pcs",
                    DefaultDimensionValue = 1,
                    IsDefaultStep = false
                },
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
    /// Loads a time study definition from a file.
    /// </summary>
    public async Task<TimeStudyDefinition?> LoadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            await using FileStream stream = File.OpenRead(filePath);
            TimeStudyDefinition? definition = await JsonSerializer.DeserializeAsync<TimeStudyDefinition>(
                stream, JsonOptions, cancellationToken);

            return definition;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading definition: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Saves a time study definition to a file.
    /// </summary>
    public async Task SaveAsync(
        TimeStudyDefinition definition,
        string? filePath = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);

        definition.ModifiedAt = DateTime.Now;

        string targetPath = filePath ?? GetDefaultFilePath(definition);
        string directory = Path.GetDirectoryName(targetPath) ?? _settingsService.Settings.GetDefinitionsDirectory();
        Directory.CreateDirectory(directory);

        try
        {
            await using FileStream stream = File.Create(targetPath);
            await JsonSerializer.SerializeAsync(stream, definition, JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving definition: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Gets the default file path for a definition.
    /// </summary>
    public string GetDefaultFilePath(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        string directory = _settingsService.Settings.GetDefinitionsDirectory();
        return Path.Combine(directory, definition.GenerateFileName());
    }

    /// <summary>
    /// Lists all available definitions in the base directory.
    /// </summary>
    public async Task<IReadOnlyList<TimeStudyDefinition>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        string directory = _settingsService.Settings.GetDefinitionsDirectory();

        if (!Directory.Exists(directory))
        {
            return [];
        }

        List<TimeStudyDefinition> definitions = [];
        string[] files = Directory.GetFiles(directory, "*.json");

        foreach (string file in files)
        {
            try
            {
                TimeStudyDefinition? definition = await LoadAsync(file, cancellationToken);

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
    /// Checks if any recordings exist for a definition.
    /// </summary>
    public bool HasRecordings(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        string recordingsDir = _settingsService.Settings.GetRecordingsDirectory();

        if (!Directory.Exists(recordingsDir))
        {
            return false;
        }

        // Check if any recording files reference this definition
        string[] files = Directory.GetFiles(recordingsDir, "*.json");

        foreach (string file in files)
        {
            try
            {
                string content = File.ReadAllText(file);

                if (content.Contains(definition.Id.ToString()))
                {
                    return true;
                }
            }
            catch
            {
                // Skip files that can't be read
            }
        }

        return false;
    }

    /// <summary>
    /// Updates the locked status of a definition based on existing recordings.
    /// </summary>
    public void UpdateLockedStatus(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        definition.IsLocked = HasRecordings(definition);
    }

    /// <summary>
    /// Creates a copy of a definition for editing.
    /// </summary>
    public TimeStudyDefinition CreateCopy(TimeStudyDefinition original)
    {
        ArgumentNullException.ThrowIfNull(original);
        return original.CreateCopy();
    }

    /// <summary>
    /// Validates a definition before saving.
    /// </summary>
    public IReadOnlyList<string> Validate(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(definition.Name))
        {
            errors.Add("Definition name is required.");
        }

        if (definition.ProcessSteps.Count == 0)
        {
            errors.Add("At least one process step is required.");
        }

        int defaultStepCount = definition.ProcessSteps.Count(s => s.IsDefaultStep);

        if (defaultStepCount == 0)
        {
            errors.Add("A default process step must be designated.");
        }
        else if (defaultStepCount > 1)
        {
            errors.Add("Only one default process step can be designated.");
        }

        // Check for duplicate order numbers
        IEnumerable<int> duplicateOrderNumbers = definition.ProcessSteps
            .GroupBy(s => s.OrderNumber)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        foreach (int orderNum in duplicateOrderNumbers)
        {
            errors.Add($"Duplicate order number: {orderNum}");
        }

        return errors;
    }
}
