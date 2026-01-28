using System.Diagnostics;
using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study recordings.
/// </summary>
public class RecordingService
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true
    };

    private readonly SettingsService _settingsService;

    private TimeStudyRecording? _activeRecording;
    private TimeStudyDefinition? _activeDefinition;
    private Stopwatch? _stopwatch;
    private ProcessStepDefinition? _currentStep;

    public RecordingService(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
    }

    /// <summary>
    /// Gets the recordings directory path.
    /// </summary>
    private string RecordingsDirectory => _settingsService.Settings.RecordingsDirectory;

    /// <summary>
    /// Gets the currently active recording, if any.
    /// </summary>
    public TimeStudyRecording? ActiveRecording => _activeRecording;

    /// <summary>
    /// Gets the currently active definition, if any.
    /// </summary>
    public TimeStudyDefinition? ActiveDefinition => _activeDefinition;

    /// <summary>
    /// Gets the current process step being timed.
    /// </summary>
    public ProcessStepDefinition? CurrentStep => _currentStep;

    /// <summary>
    /// Gets whether a recording is currently in progress.
    /// </summary>
    public bool IsRecording => _activeRecording is not null && !_activeRecording.IsCompleted;

    /// <summary>
    /// Gets the elapsed time since recording started.
    /// </summary>
    public TimeSpan ElapsedTime => _stopwatch?.Elapsed ?? TimeSpan.Zero;

    /// <summary>
    /// Event raised when the current step changes.
    /// </summary>
    public event EventHandler<ProcessStepDefinition?>? CurrentStepChanged;

    /// <summary>
    /// Event raised when a recording is started.
    /// </summary>
    public event EventHandler<TimeStudyRecording>? RecordingStarted;

    /// <summary>
    /// Event raised when a recording is stopped.
    /// </summary>
    public event EventHandler<TimeStudyRecording>? RecordingStopped;

    /// <summary>
    /// Starts a new recording session.
    /// </summary>
    /// <param name="definition">The definition to record against.</param>
    public void StartRecording(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (_activeRecording is not null && !_activeRecording.IsCompleted)
        {
            throw new InvalidOperationException("A recording is already in progress.");
        }

        ProcessStepDefinition? defaultStep = definition.DefaultProcessStep;

        if (defaultStep is null)
        {
            throw new InvalidOperationException("Definition must have a default process step.");
        }

        _activeDefinition = definition;
        _activeRecording = new TimeStudyRecording
        {
            Id = Guid.NewGuid(),
            DefinitionId = definition.Id,
            DefinitionName = definition.Name,
            StartedAt = DateTime.Now
        };

        _stopwatch = Stopwatch.StartNew();
        _currentStep = defaultStep;

        // Add the first entry for the default step
        _activeRecording.AddEntry(defaultStep, TimeSpan.Zero);

        RecordingStarted?.Invoke(this, _activeRecording);
        CurrentStepChanged?.Invoke(this, _currentStep);
    }

    /// <summary>
    /// Switches to a different process step.
    /// </summary>
    /// <param name="step">The step to switch to.</param>
    /// <param name="dimensionValue">Optional dimension value override.</param>
    public void SwitchToStep(ProcessStepDefinition step, decimal? dimensionValue = null)
    {
        ArgumentNullException.ThrowIfNull(step);

        if (_activeRecording is null || _stopwatch is null)
        {
            throw new InvalidOperationException("No recording is in progress.");
        }

        if (_activeRecording.IsCompleted)
        {
            throw new InvalidOperationException("Recording has been completed.");
        }

        // Don't add duplicate entries for the same step if clicked multiple times
        if (_currentStep?.OrderNumber == step.OrderNumber)
        {
            return;
        }

        TimeSpan elapsed = _stopwatch.Elapsed;
        _activeRecording.AddEntry(step, elapsed, dimensionValue);

        _currentStep = step;
        CurrentStepChanged?.Invoke(this, _currentStep);

        // Auto-save if enabled
        if (_settingsService.Settings.AutoSaveRecordings)
        {
            AutoSave();
        }
    }

    /// <summary>
    /// Pauses the recording by switching to the default step.
    /// </summary>
    public void Pause()
    {
        if (_activeDefinition?.DefaultProcessStep is ProcessStepDefinition defaultStep)
        {
            SwitchToStep(defaultStep);
        }
    }

    /// <summary>
    /// Stops the current recording.
    /// </summary>
    /// <returns>The completed recording.</returns>
    public TimeStudyRecording StopRecording()
    {
        if (_activeRecording is null || _stopwatch is null)
        {
            throw new InvalidOperationException("No recording is in progress.");
        }

        _stopwatch.Stop();
        _activeRecording.Complete();

        TimeStudyRecording completed = _activeRecording;
        RecordingStopped?.Invoke(this, completed);

        // Save the completed recording
        Save(completed);

        // Clear active state
        _activeRecording = null;
        _activeDefinition = null;
        _stopwatch = null;
        _currentStep = null;

        return completed;
    }

    /// <summary>
    /// Saves a recording to disk.
    /// </summary>
    /// <param name="recording">The recording to save.</param>
    /// <param name="filePath">Optional specific file path.</param>
    /// <returns>The path where the recording was saved.</returns>
    public string Save(TimeStudyRecording recording, string? filePath = null)
    {
        ArgumentNullException.ThrowIfNull(recording);

        _settingsService.Settings.EnsureDirectoriesExist();

        if (string.IsNullOrEmpty(filePath))
        {
            string safeName = GetSafeFileName(recording.DefinitionName);
            string dateStr = recording.StartedAt.ToString("yyyy-MM-dd_HHmmss");
            filePath = Path.Combine(
                RecordingsDirectory,
                $"{safeName}_{recording.Id:N}_{dateStr}.json");
        }

        string json = JsonSerializer.Serialize(recording, s_jsonOptions);
        File.WriteAllText(filePath, json);

        return filePath;
    }

    /// <summary>
    /// Loads a recording from a file.
    /// </summary>
    /// <param name="filePath">The file path to load from.</param>
    /// <returns>The loaded recording.</returns>
    public TimeStudyRecording Load(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path is required.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Recording file not found.", filePath);
        }

        string json = File.ReadAllText(filePath);
        TimeStudyRecording? recording = JsonSerializer.Deserialize<TimeStudyRecording>(json, s_jsonOptions);

        return recording ?? throw new InvalidOperationException("Failed to deserialize recording.");
    }

    /// <summary>
    /// Gets all recordings for a specific definition.
    /// </summary>
    /// <param name="definitionId">The definition ID to filter by.</param>
    /// <returns>List of recordings for the definition.</returns>
    public List<TimeStudyRecording> GetRecordingsForDefinition(Guid definitionId)
    {
        _settingsService.Settings.EnsureDirectoriesExist();

        List<TimeStudyRecording> recordings = [];

        foreach (string file in Directory.GetFiles(RecordingsDirectory, "*.json"))
        {
            try
            {
                TimeStudyRecording recording = Load(file);

                if (recording.DefinitionId == definitionId)
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
    /// Gets all recordings in the recordings directory.
    /// </summary>
    /// <returns>List of all recordings.</returns>
    public List<RecordingSummary> GetAllRecordings()
    {
        _settingsService.Settings.EnsureDirectoriesExist();

        List<RecordingSummary> summaries = [];

        foreach (string file in Directory.GetFiles(RecordingsDirectory, "*.json"))
        {
            try
            {
                TimeStudyRecording recording = Load(file);
                summaries.Add(new RecordingSummary
                {
                    Id = recording.Id,
                    DefinitionId = recording.DefinitionId,
                    DefinitionName = recording.DefinitionName,
                    StartedAt = recording.StartedAt,
                    CompletedAt = recording.CompletedAt,
                    IsCompleted = recording.IsCompleted,
                    EntryCount = recording.Entries.Count,
                    TotalDuration = recording.TotalDuration,
                    FilePath = file
                });
            }
            catch
            {
                // Skip invalid files
            }
        }

        return summaries.OrderByDescending(s => s.StartedAt).ToList();
    }

    /// <summary>
    /// Auto-saves the current recording in progress.
    /// </summary>
    private void AutoSave()
    {
        if (_activeRecording is null)
        {
            return;
        }

        try
        {
            string safeName = GetSafeFileName(_activeRecording.DefinitionName);
            string filePath = Path.Combine(
                RecordingsDirectory,
                $"{safeName}_{_activeRecording.Id:N}_autosave.json");

            _settingsService.Settings.EnsureDirectoriesExist();
            string json = JsonSerializer.Serialize(_activeRecording, s_jsonOptions);
            File.WriteAllText(filePath, json);
        }
        catch
        {
            // Ignore auto-save failures
        }
    }

    /// <summary>
    /// Creates a safe file name from a string.
    /// </summary>
    private static string GetSafeFileName(string name)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string safeName = new(name.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());

        return string.IsNullOrWhiteSpace(safeName) ? "Recording" : safeName;
    }
}

/// <summary>
/// Summary information about a recording for display in lists.
/// </summary>
public class RecordingSummary
{
    public Guid Id { get; set; }
    public Guid DefinitionId { get; set; }
    public string DefinitionName { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; }
    public int EntryCount { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public string FilePath { get; set; } = string.Empty;
}
