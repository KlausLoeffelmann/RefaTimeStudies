using System.Diagnostics;
using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study recordings.
/// </summary>
public sealed class RecordingService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly SettingsService _settingsService;
    private readonly Stopwatch _stopwatch = new();

    private TimeStudyRecording? _currentRecording;
    private TimeStudyDefinition? _currentDefinition;
    private int _currentStepOrderNumber;

    public RecordingService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// Gets the current recording, if any.
    /// </summary>
    public TimeStudyRecording? CurrentRecording => _currentRecording;

    /// <summary>
    /// Gets whether a recording is currently in progress.
    /// </summary>
    public bool IsRecording => _currentRecording is not null && !_currentRecording.IsCompleted;

    /// <summary>
    /// Gets the current elapsed time from the start of the recording.
    /// </summary>
    public TimeSpan ElapsedTime => _stopwatch.Elapsed;

    /// <summary>
    /// Gets the current active process step order number.
    /// </summary>
    public int CurrentStepOrderNumber => _currentStepOrderNumber;

    /// <summary>
    /// Event raised when the recording state changes.
    /// </summary>
    public event EventHandler? RecordingStateChanged;

    /// <summary>
    /// Event raised when a time entry is recorded.
    /// </summary>
    public event EventHandler<TimeEntry>? EntryRecorded;

    /// <summary>
    /// Starts a new recording session.
    /// </summary>
    public void StartRecording(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        ProcessStepDefinition? defaultStep = definition.GetDefaultStep();

        if (defaultStep is null)
        {
            throw new InvalidOperationException("Definition must have a default process step.");
        }

        _currentDefinition = definition;
        _currentStepOrderNumber = defaultStep.OrderNumber;

        _currentRecording = new TimeStudyRecording
        {
            DefinitionId = definition.Id,
            DefinitionName = definition.Name,
            StartedAt = DateTime.Now,
            IsCompleted = false
        };

        // Record the initial entry on the default step
        TimeEntry initialEntry = new()
        {
            SequenceNumber = 1,
            ProcessStepOrderNumber = defaultStep.OrderNumber,
            ProcessStepDescription = defaultStep.Description,
            Timestamp = _currentRecording.StartedAt,
            ElapsedFromStart = TimeSpan.Zero,
            DimensionValue = defaultStep.DefaultDimensionValue
        };

        _currentRecording.Entries.Add(initialEntry);
        _stopwatch.Restart();

        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
        EntryRecorded?.Invoke(this, initialEntry);
    }

    /// <summary>
    /// Records a step change to a specific process step.
    /// </summary>
    public TimeEntry? RecordStepChange(int orderNumber)
    {
        if (!IsRecording || _currentRecording is null || _currentDefinition is null)
        {
            return null;
        }

        // Find the step definition
        ProcessStepDefinition? step = _currentDefinition.ProcessSteps
            .FirstOrDefault(s => s.OrderNumber == orderNumber);

        if (step is null)
        {
            return null;
        }

        // Don't record duplicate consecutive entries for the same step
        if (_currentStepOrderNumber == orderNumber)
        {
            return null;
        }

        // Calculate duration for the previous entry
        TimeEntry? previousEntry = _currentRecording.Entries.LastOrDefault();

        if (previousEntry is not null)
        {
            previousEntry.Duration = _stopwatch.Elapsed - previousEntry.ElapsedFromStart;
        }

        // Create new entry
        TimeEntry entry = new()
        {
            SequenceNumber = _currentRecording.Entries.Count + 1,
            ProcessStepOrderNumber = orderNumber,
            ProcessStepDescription = step.Description,
            Timestamp = DateTime.Now,
            ElapsedFromStart = _stopwatch.Elapsed,
            DimensionValue = step.DefaultDimensionValue
        };

        _currentRecording.Entries.Add(entry);
        _currentStepOrderNumber = orderNumber;

        EntryRecorded?.Invoke(this, entry);

        // Auto-save if enabled
        if (_settingsService.Settings.AutoSaveRecordings)
        {
            _ = SaveCurrentAsync();
        }

        return entry;
    }

    /// <summary>
    /// Pauses the recording by switching to the default step.
    /// </summary>
    public TimeEntry? Pause()
    {
        if (!IsRecording || _currentDefinition is null)
        {
            return null;
        }

        ProcessStepDefinition? defaultStep = _currentDefinition.GetDefaultStep();

        if (defaultStep is null)
        {
            return null;
        }

        return RecordStepChange(defaultStep.OrderNumber);
    }

    /// <summary>
    /// Stops and completes the current recording.
    /// </summary>
    public async Task<TimeStudyRecording?> StopRecordingAsync(CancellationToken cancellationToken = default)
    {
        if (!IsRecording || _currentRecording is null)
        {
            return null;
        }

        // Calculate duration for the final entry
        TimeEntry? lastEntry = _currentRecording.Entries.LastOrDefault();

        if (lastEntry is not null)
        {
            lastEntry.Duration = _stopwatch.Elapsed - lastEntry.ElapsedFromStart;
        }

        _stopwatch.Stop();
        _currentRecording.CompletedAt = DateTime.Now;
        _currentRecording.IsCompleted = true;

        await SaveAsync(_currentRecording, cancellationToken);

        TimeStudyRecording completedRecording = _currentRecording;
        _currentRecording = null;
        _currentDefinition = null;

        RecordingStateChanged?.Invoke(this, EventArgs.Empty);

        return completedRecording;
    }

    /// <summary>
    /// Saves the current recording to disk.
    /// </summary>
    public async Task SaveCurrentAsync(CancellationToken cancellationToken = default)
    {
        if (_currentRecording is null)
        {
            return;
        }

        await SaveAsync(_currentRecording, cancellationToken);
    }

    /// <summary>
    /// Saves a recording to disk.
    /// </summary>
    public async Task SaveAsync(TimeStudyRecording recording, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(recording);

        string directory = _settingsService.Settings.GetRecordingsDirectory();
        Directory.CreateDirectory(directory);

        string filePath = Path.Combine(directory, recording.GenerateFileName());

        try
        {
            await using FileStream stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, recording, JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving recording: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Loads a recording from disk.
    /// </summary>
    public async Task<TimeStudyRecording?> LoadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            await using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<TimeStudyRecording>(stream, JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading recording: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Lists all recordings for a specific definition.
    /// </summary>
    public async Task<IReadOnlyList<TimeStudyRecording>> ListByDefinitionAsync(
        Guid definitionId,
        CancellationToken cancellationToken = default)
    {
        string directory = _settingsService.Settings.GetRecordingsDirectory();

        if (!Directory.Exists(directory))
        {
            return [];
        }

        List<TimeStudyRecording> recordings = [];
        string[] files = Directory.GetFiles(directory, "*.json");

        foreach (string file in files)
        {
            try
            {
                TimeStudyRecording? recording = await LoadAsync(file, cancellationToken);

                if (recording?.DefinitionId == definitionId)
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
    /// Lists all completed recordings.
    /// </summary>
    public async Task<IReadOnlyList<TimeStudyRecording>> ListCompletedAsync(
        CancellationToken cancellationToken = default)
    {
        string directory = _settingsService.Settings.GetRecordingsDirectory();

        if (!Directory.Exists(directory))
        {
            return [];
        }

        List<TimeStudyRecording> recordings = [];
        string[] files = Directory.GetFiles(directory, "*.json");

        foreach (string file in files)
        {
            try
            {
                TimeStudyRecording? recording = await LoadAsync(file, cancellationToken);

                if (recording?.IsCompleted == true)
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
}
