using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
///  Service for managing time study recordings.
/// </summary>
public class RecordingService
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly SettingsService _settingsService;

    /// <summary>
    ///  Initializes a new instance of the <see cref="RecordingService"/> class.
    /// </summary>
    /// <param name="settingsService">The settings service instance.</param>
    public RecordingService(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
    }

    /// <summary>
    ///  Gets the directory where recordings are stored.
    /// </summary>
    private string RecordingsDirectory => _settingsService.Settings.RecordingsDirectory;

    /// <summary>
    ///  Creates a new recording for the specified definition.
    /// </summary>
    /// <param name="definition">The definition to use for the recording.</param>
    /// <returns>A new recording instance.</returns>
    public TimeStudyRecording CreateNew(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        return new TimeStudyRecording
        {
            DefinitionId = definition.Id,
            DefinitionName = definition.Name,
            StartedAt = DateTime.Now
        };
    }

    /// <summary>
    ///  Saves a recording to a JSON file.
    /// </summary>
    /// <param name="recording">The recording to save.</param>
    /// <param name="filePath">Optional specific file path. If null, uses default naming.</param>
    /// <returns>The path where the recording was saved.</returns>
    public string Save(TimeStudyRecording recording, string? filePath = null)
    {
        ArgumentNullException.ThrowIfNull(recording);

        if (string.IsNullOrEmpty(filePath))
        {
            string safeDefName = GetSafeFileName(recording.DefinitionName);
            string dateStr = recording.StartedAt.ToString("yyyy-MM-dd_HHmmss");
            filePath = Path.Combine(RecordingsDirectory, $"{safeDefName}_{recording.Id}_{dateStr}.json");
        }

        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(recording, s_jsonOptions);
        File.WriteAllText(filePath, json);

        return filePath;
    }

    /// <summary>
    ///  Loads a recording from a JSON file.
    /// </summary>
    /// <param name="filePath">The path to the recording file.</param>
    /// <returns>The loaded recording, or null if loading failed.</returns>
    public TimeStudyRecording? Load(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TimeStudyRecording>(json, s_jsonOptions);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading recording: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    ///  Gets all recording files in the recordings directory.
    /// </summary>
    /// <returns>A list of file paths to recording files.</returns>
    public List<string> GetAllRecordingFiles()
    {
        if (!Directory.Exists(RecordingsDirectory))
        {
            return [];
        }

        return Directory.GetFiles(RecordingsDirectory, "*.json").ToList();
    }

    /// <summary>
    ///  Loads all recordings from the recordings directory.
    /// </summary>
    /// <returns>A list of all loaded recordings.</returns>
    public List<TimeStudyRecording> LoadAll()
    {
        List<TimeStudyRecording> recordings = [];

        foreach (string filePath in GetAllRecordingFiles())
        {
            var recording = Load(filePath);
            if (recording is not null)
            {
                recordings.Add(recording);
            }
        }

        return recordings;
    }

    /// <summary>
    ///  Gets all recordings for a specific definition.
    /// </summary>
    /// <param name="definitionId">The ID of the definition.</param>
    /// <returns>A list of recordings for the definition.</returns>
    public List<TimeStudyRecording> GetRecordingsForDefinition(Guid definitionId)
    {
        return LoadAll().Where(r => r.DefinitionId == definitionId).ToList();
    }

    /// <summary>
    ///  Marks a recording as completed.
    /// </summary>
    /// <param name="recording">The recording to complete.</param>
    public void CompleteRecording(TimeStudyRecording recording)
    {
        ArgumentNullException.ThrowIfNull(recording);

        recording.IsCompleted = true;
        recording.CompletedAt = DateTime.Now;
    }

    /// <summary>
    ///  Deletes a recording file.
    /// </summary>
    /// <param name="filePath">The path to the recording file.</param>
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
            System.Diagnostics.Debug.WriteLine($"Error deleting recording: {ex.Message}");
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
        return string.IsNullOrWhiteSpace(safeName) ? "recording" : safeName;
    }
}
