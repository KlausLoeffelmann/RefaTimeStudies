using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study recordings.
/// </summary>
public class RecordingService
{
    private readonly SettingsService _settingsService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RecordingService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// Gets the directory where recordings are stored.
    /// </summary>
    private string RecordingsDirectory => _settingsService.CurrentSettings.RecordingsDirectory;

    /// <summary>
    /// Creates a new recording based on a definition.
    /// </summary>
    public TimeStudyRecording CreateNew(TimeStudyDefinition definition)
    {
        return new TimeStudyRecording
        {
            Id = Guid.NewGuid(),
            DefinitionId = definition.Id,
            DefinitionName = definition.Name,
            StartedAt = DateTime.Now,
            Entries = []
        };
    }

    /// <summary>
    /// Saves a recording to disk.
    /// </summary>
    public void Save(TimeStudyRecording recording, string? filePath = null)
    {
        _settingsService.CurrentSettings.EnsureDirectoriesExist();

        filePath ??= GetDefaultFilePath(recording);

        var json = JsonSerializer.Serialize(recording, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Loads a recording from a file.
    /// </summary>
    public TimeStudyRecording? Load(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<TimeStudyRecording>(json, JsonOptions);
    }

    /// <summary>
    /// Gets all recordings from the recordings directory.
    /// </summary>
    public List<TimeStudyRecording> GetAll()
    {
        var recordings = new List<TimeStudyRecording>();

        _settingsService.CurrentSettings.EnsureDirectoriesExist();

        var files = Directory.GetFiles(RecordingsDirectory, "*.json");
        foreach (var file in files)
        {
            try
            {
                var recording = Load(file);
                if (recording != null)
                {
                    recordings.Add(recording);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading recording {file}: {ex.Message}");
            }
        }

        return recordings.OrderByDescending(r => r.StartedAt).ToList();
    }

    /// <summary>
    /// Gets all recordings for a specific definition.
    /// </summary>
    public List<TimeStudyRecording> GetByDefinition(Guid definitionId)
    {
        return GetAll().Where(r => r.DefinitionId == definitionId).ToList();
    }

    /// <summary>
    /// Gets the default file path for a recording.
    /// </summary>
    public string GetDefaultFilePath(TimeStudyRecording recording)
    {
        var safeName = string.Join("_", recording.DefinitionName.Split(Path.GetInvalidFileNameChars()));
        var dateStr = recording.StartedAt.ToString("yyyy-MM-dd_HHmmss");
        var fileName = $"{safeName}_{recording.Id:N}_{dateStr}.json";
        return Path.Combine(RecordingsDirectory, fileName);
    }

    /// <summary>
    /// Deletes a recording file.
    /// </summary>
    public void Delete(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// Completes a recording.
    /// </summary>
    public void Complete(TimeStudyRecording recording, TimeSpan finalElapsed)
    {
        recording.IsCompleted = true;
        recording.CompletedAt = DateTime.Now;

        // Update the duration of the last entry
        if (recording.Entries.Count > 0)
        {
            var lastEntry = recording.Entries[^1];
            lastEntry.Duration = finalElapsed - lastEntry.ElapsedFromStart;
        }
    }

    /// <summary>
    /// Adds a time entry to a recording.
    /// </summary>
    public void AddEntry(TimeStudyRecording recording, ProcessStepDefinition step, TimeSpan elapsed, decimal? dimensionValue = null)
    {
        var entry = new TimeEntry
        {
            SequenceNumber = recording.Entries.Count + 1,
            ProcessStepOrderNumber = step.OrderNumber,
            ProcessStepDescription = step.Description,
            Timestamp = DateTime.Now,
            ElapsedFromStart = elapsed,
            DimensionValue = dimensionValue ?? step.DefaultDimensionValue
        };

        // Calculate the duration of the previous entry
        if (recording.Entries.Count > 0)
        {
            var previousEntry = recording.Entries[^1];
            previousEntry.Duration = elapsed - previousEntry.ElapsedFromStart;
        }

        recording.Entries.Add(entry);
    }
}
