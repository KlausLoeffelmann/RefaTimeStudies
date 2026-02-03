using System.Diagnostics;
using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing time study recordings with accurate timing.
/// </summary>
public class RecordingService
{
    private readonly Stopwatch _stopwatch = new();
    private TimeStudyRecording? _currentRecording;
    private TimeStudyDefinition? _currentDefinition;
    private int _currentStepOrderNumber;
    private int _nextSequenceNumber;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Gets whether a recording is currently active.
    /// </summary>
    public bool IsRecording => _stopwatch.IsRunning;

    /// <summary>
    /// Gets the current recording, if any.
    /// </summary>
    public TimeStudyRecording? CurrentRecording => _currentRecording;

    /// <summary>
    /// Gets the current active process step order number.
    /// </summary>
    public int CurrentStepOrderNumber => _currentStepOrderNumber;

    /// <summary>
    /// Event raised when a new entry is recorded.
    /// </summary>
    public event EventHandler<TimeEntry>? EntryRecorded;

    /// <summary>
    /// Starts a new recording session based on the given definition.
    /// </summary>
    public TimeStudyRecording StartRecording(TimeStudyDefinition definition)
    {
        if (IsRecording)
            throw new InvalidOperationException("A recording is already in progress.");

        _currentDefinition = definition;
        _nextSequenceNumber = 1;

        _currentRecording = new TimeStudyRecording
        {
            Id = Guid.NewGuid(),
            DefinitionId = definition.Id,
            DefinitionName = definition.Name,
            StartedAt = DateTime.Now,
            IsCompleted = false,
            Entries = new List<TimeEntry>()
        };

        // Start on the default step
        _currentStepOrderNumber = definition.DefaultProcessStepOrderNumber;

        // Record the initial entry (start on default step)
        var defaultStep = definition.GetDefaultProcessStep();
        var initialEntry = new TimeEntry
        {
            SequenceNumber = _nextSequenceNumber++,
            ProcessStepOrderNumber = _currentStepOrderNumber,
            ProcessStepDescription = defaultStep?.Description ?? "Default",
            Timestamp = DateTime.Now,
            ElapsedFromStart = TimeSpan.Zero,
            Duration = TimeSpan.Zero,
            DimensionValue = null
        };

        _currentRecording.Entries.Add(initialEntry);
        _stopwatch.Restart();

        EntryRecorded?.Invoke(this, initialEntry);

        return _currentRecording;
    }

    /// <summary>
    /// Records a step change during an active recording.
    /// </summary>
    public TimeEntry RecordStep(int processStepOrderNumber, decimal? dimensionValue = null)
    {
        if (!IsRecording || _currentRecording == null || _currentDefinition == null)
            throw new InvalidOperationException("No recording is in progress.");

        TimeSpan elapsed = _stopwatch.Elapsed;
        DateTime timestamp = DateTime.Now;

        // Update the duration of the previous entry
        if (_currentRecording.Entries.Count > 0)
        {
            var previousEntry = _currentRecording.Entries[^1];
            previousEntry.Duration = elapsed - previousEntry.ElapsedFromStart;
        }

        // Get step description
        var step = _currentDefinition.GetProcessStep(processStepOrderNumber);
        string description = step?.Description ?? $"Step {processStepOrderNumber}";

        // Create new entry
        var entry = new TimeEntry
        {
            SequenceNumber = _nextSequenceNumber++,
            ProcessStepOrderNumber = processStepOrderNumber,
            ProcessStepDescription = description,
            Timestamp = timestamp,
            ElapsedFromStart = elapsed,
            Duration = TimeSpan.Zero, // Will be calculated when next entry is made
            DimensionValue = dimensionValue
        };

        _currentRecording.Entries.Add(entry);
        _currentStepOrderNumber = processStepOrderNumber;

        EntryRecorded?.Invoke(this, entry);

        return entry;
    }

    /// <summary>
    /// Pauses (switches to default step) during an active recording.
    /// </summary>
    public TimeEntry Pause()
    {
        if (_currentDefinition == null)
            throw new InvalidOperationException("No recording is in progress.");

        return RecordStep(_currentDefinition.DefaultProcessStepOrderNumber);
    }

    /// <summary>
    /// Stops the current recording.
    /// </summary>
    public TimeStudyRecording StopRecording()
    {
        if (!IsRecording || _currentRecording == null)
            throw new InvalidOperationException("No recording is in progress.");

        TimeSpan elapsed = _stopwatch.Elapsed;
        _stopwatch.Stop();

        // Update the duration of the last entry
        if (_currentRecording.Entries.Count > 0)
        {
            var lastEntry = _currentRecording.Entries[^1];
            lastEntry.Duration = elapsed - lastEntry.ElapsedFromStart;
        }

        _currentRecording.CompletedAt = DateTime.Now;
        _currentRecording.IsCompleted = true;

        var result = _currentRecording;
        _currentRecording = null;
        _currentDefinition = null;

        return result;
    }

    /// <summary>
    /// Gets the current elapsed time since recording started.
    /// </summary>
    public TimeSpan GetElapsedTime()
    {
        return _stopwatch.Elapsed;
    }

    /// <summary>
    /// Saves a recording to a JSON file.
    /// </summary>
    public void Save(TimeStudyRecording recording, string recordingsDirectory)
    {
        Directory.CreateDirectory(recordingsDirectory);

        string safeName = GetSafeFileName(recording.DefinitionName);
        string dateStr = recording.StartedAt.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{safeName}_{recording.DefinitionId:N}_{dateStr}.json";
        string filePath = Path.Combine(recordingsDirectory, fileName);

        string json = JsonSerializer.Serialize(recording, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Loads a recording from a JSON file.
    /// </summary>
    public TimeStudyRecording? Load(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TimeStudyRecording>(json, JsonOptions);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets all recording files for a specific definition.
    /// </summary>
    public IEnumerable<string> GetRecordingFiles(Guid definitionId, string recordingsDirectory)
    {
        if (!Directory.Exists(recordingsDirectory))
            return Enumerable.Empty<string>();

        string searchPattern = $"*_{definitionId:N}_*.json";
        return Directory.GetFiles(recordingsDirectory, searchPattern)
            .OrderByDescending(f => File.GetLastWriteTime(f));
    }

    /// <summary>
    /// Gets all recording files from a directory.
    /// </summary>
    public IEnumerable<string> GetAllRecordingFiles(string recordingsDirectory)
    {
        if (!Directory.Exists(recordingsDirectory))
            return Enumerable.Empty<string>();

        return Directory.GetFiles(recordingsDirectory, "*.json")
            .OrderByDescending(f => File.GetLastWriteTime(f));
    }

    private static string GetSafeFileName(string name)
    {
        char[] invalid = Path.GetInvalidFileNameChars();
        string safe = new string(name.Select(c => invalid.Contains(c) ? '_' : c).ToArray());
        return safe.Length > 30 ? safe[..30] : safe;
    }
}
