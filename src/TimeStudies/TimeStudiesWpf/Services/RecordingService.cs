using System.IO;
using System.Text.Json;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Manages time study recordings stored as JSON files.
/// </summary>
public class RecordingService : IRecordingService
{
    private readonly ISettingsService _settingsService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RecordingService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    private string RecordingsDirectory => Path.Combine(
        _settingsService.Current.BaseDirectory, "Recordings");

    /// <summary>
    /// Gets all recordings.
    /// </summary>
    public async Task<IReadOnlyList<TimeStudyRecording>> GetAllAsync()
    {
        var recordings = new List<TimeStudyRecording>();

        if (!Directory.Exists(RecordingsDirectory))
            return recordings;

        foreach (var file in Directory.GetFiles(RecordingsDirectory, "*.json"))
        {
            try
            {
                var json = await File.ReadAllTextAsync(file);
                var recording = JsonSerializer.Deserialize<TimeStudyRecording>(json, JsonOptions);
                if (recording is not null)
                {
                    recordings.Add(recording);
                }
            }
            catch
            {
                // Skip invalid files
            }
        }

        return recordings.OrderByDescending(r => r.StartedAt).ToList();
    }

    /// <summary>
    /// Gets all recordings for a definition.
    /// </summary>
    public async Task<IReadOnlyList<TimeStudyRecording>> GetForDefinitionAsync(Guid definitionId)
    {
        var all = await GetAllAsync();
        return all.Where(r => r.DefinitionId == definitionId).ToList();
    }

    /// <summary>
    /// Loads a recording by its ID.
    /// </summary>
    public async Task<TimeStudyRecording?> LoadAsync(Guid id)
    {
        if (!Directory.Exists(RecordingsDirectory))
            return null;

        foreach (var file in Directory.GetFiles(RecordingsDirectory, "*.json"))
        {
            try
            {
                var json = await File.ReadAllTextAsync(file);
                var recording = JsonSerializer.Deserialize<TimeStudyRecording>(json, JsonOptions);
                if (recording?.Id == id)
                {
                    return recording;
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
    /// Saves a recording to disk.
    /// </summary>
    public async Task SaveAsync(TimeStudyRecording recording)
    {
        Directory.CreateDirectory(RecordingsDirectory);

        // Clean filename
        var safeName = string.Join("_", recording.DefinitionName.Split(Path.GetInvalidFileNameChars()));
        var dateStr = recording.StartedAt.ToString("yyyy-MM-dd_HHmmss");
        var fileName = $"{safeName}_{recording.Id}_{dateStr}.json";
        var filePath = Path.Combine(RecordingsDirectory, fileName);

        var json = JsonSerializer.Serialize(recording, JsonOptions);
        await File.WriteAllTextAsync(filePath, json);
    }

    /// <summary>
    /// Deletes a recording from disk.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        if (!Directory.Exists(RecordingsDirectory))
            return;

        foreach (var file in Directory.GetFiles(RecordingsDirectory, $"*_{id}_*.json"))
        {
            File.Delete(file);
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Checks if any recordings exist for a definition.
    /// </summary>
    public async Task<bool> HasRecordingsAsync(Guid definitionId)
    {
        var recordings = await GetForDefinitionAsync(definitionId);
        return recordings.Count > 0;
    }
}
