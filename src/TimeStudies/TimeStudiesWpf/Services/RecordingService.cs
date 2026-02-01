using System.Diagnostics;
using TimeStudiesWpf.Helpers;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Service for managing time study recordings.
/// Dienst für die Verwaltung von Zeitaufnahmen.
/// </summary>
public class RecordingService
{
    private readonly string _baseDirectory;
    private TimeStudyRecording? _currentRecording;
    private Stopwatch? _stopwatch;
    private TimeEntry? _lastEntry;
    private ProcessStepDefinition? _currentProcessStep;
    private int _entrySequenceNumber;

    /// <summary>
    /// Event raised when the recording state changes.
    /// Wird ausgelöst, wenn der Aufnahmezustand ändert.
    /// </summary>
    public event EventHandler? RecordingStateChanged;

    /// <summary>
    /// Event raised when a time entry is added.
    /// Wird ausgelöst, wenn ein Zeiteintrag hinzugefügt wird.
    /// </summary>
    public event EventHandler<TimeEntry>? TimeEntryAdded;

    /// <summary>
    /// Event raised when the current process step changes.
    /// Wird ausgelöst, wenn der aktuelle Ablaufabschnitt ändert.
    /// </summary>
    public event EventHandler<ProcessStepDefinition?>? CurrentProcessStepChanged;

    /// <summary>
    /// Gets the current active recording, or null if no recording is in progress.
    /// Ruft die aktuelle aktive Aufnahme ab oder null, wenn keine Aufnahme läuft.
    /// </summary>
    public TimeStudyRecording? CurrentRecording => _currentRecording;

    /// <summary>
    /// Gets a value indicating whether a recording is currently in progress.
    /// Ruft einen Wert ab, der angibt, ob derzeit eine Aufnahme läuft.
    /// </summary>
    public bool IsRecording => _currentRecording != null && !_currentRecording.IsCompleted;

    /// <summary>
    /// Gets the elapsed time since the recording started.
    /// Ruft die verstrichene Zeit seit dem Start der Aufnahme ab.
    /// </summary>
    public TimeSpan ElapsedTime => _stopwatch?.Elapsed ?? TimeSpan.Zero;

    /// <summary>
    /// Gets the current active process step.
    /// Ruft den aktuellen aktiven Ablaufabschnitt ab.
    /// </summary>
    public ProcessStepDefinition? CurrentProcessStep => _currentProcessStep;

    /// <summary>
    /// Initializes a new instance of RecordingService.
    /// Initialisiert eine neue Instanz von RecordingService.
    /// </summary>
    /// <param name="baseDirectory">The base directory where recordings are stored.</param>
    public RecordingService(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
        PathHelper.EnsureDirectoriesExist(baseDirectory);
    }

    /// <summary>
    /// Starts a new recording based on a time study definition.
    /// Startet eine neue Aufnahme basierend auf einer Zeitaufnahme-Definition.
    /// </summary>
    /// <param name="definition">The definition to use for the recording.</param>
    /// <returns>The newly created recording.</returns>
    public TimeStudyRecording StartRecording(TimeStudyDefinition definition)
    {
        if (IsRecording)
        {
            throw new InvalidOperationException("A recording is already in progress.");
        }

        _currentRecording = new TimeStudyRecording
        {
            DefinitionId = definition.Id,
            DefinitionName = definition.Name
        };

        _stopwatch = Stopwatch.StartNew();
        _lastEntry = null;
        _currentProcessStep = definition.GetDefaultProcessStep();
        _entrySequenceNumber = 0;

        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
        CurrentProcessStepChanged?.Invoke(this, _currentProcessStep);

        return _currentRecording;
    }

    /// <summary>
    /// Stops the current recording.
    /// Stoppt die aktuelle Aufnahme.
    /// </summary>
    /// <returns>The completed recording.</returns>
    public TimeStudyRecording StopRecording()
    {
        if (!IsRecording)
        {
            throw new InvalidOperationException("No recording is in progress.");
        }

        if (_lastEntry != null)
        {
            _lastEntry.Duration = _stopwatch.Elapsed - _lastEntry.ElapsedFromStart;
        }

        _currentRecording!.Complete();
        _stopwatch!.Stop();

        SaveRecording(_currentRecording);

        var recording = _currentRecording;
        _currentRecording = null;
        _stopwatch = null;
        _lastEntry = null;
        _currentProcessStep = null;

        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
        CurrentProcessStepChanged?.Invoke(this, null);

        return recording;
    }

    /// <summary>
    /// Pauses the recording by switching to the default process step.
    /// Unterbricht die Aufnahme, indem zum Standard-Ablaufabschnitt gewechselt wird.
    /// </summary>
    /// <param name="definition">The definition used for the recording.</param>
    public void PauseRecording(TimeStudyDefinition definition)
    {
        if (!IsRecording)
        {
            throw new InvalidOperationException("No recording is in progress.");
        }

        var defaultStep = definition.GetDefaultProcessStep();
        if (defaultStep != null)
        {
            AddTimeEntry(defaultStep, definition);
        }
    }

    /// <summary>
    /// Adds a time entry for a specific process step.
    /// Fügt einen Zeiteintrag für einen bestimmten Ablaufabschnitt hinzu.
    /// </summary>
    /// <param name="processStep">The process step being performed.</param>
    /// <param name="definition">The definition containing the process steps.</param>
    /// <param name="dimensionValue">Optional override for the dimension value.</param>
    public void AddTimeEntry(ProcessStepDefinition processStep, TimeStudyDefinition definition, decimal? dimensionValue = null)
    {
        if (!IsRecording)
        {
            throw new InvalidOperationException("No recording is in progress.");
        }

        var elapsed = _stopwatch!.Elapsed;

        if (_lastEntry != null)
        {
            _lastEntry.Duration = elapsed - _lastEntry.ElapsedFromStart;
        }

        var entry = new TimeEntry
        {
            ProcessStepOrderNumber = processStep.OrderNumber,
            ProcessStepDescription = processStep.Description,
            Timestamp = DateTime.Now,
            ElapsedFromStart = elapsed,
            Duration = TimeSpan.Zero,
            DimensionValue = dimensionValue ?? processStep.DefaultDimensionValue
        };

        _currentRecording!.AddEntry(entry);
        _lastEntry = entry;
        _currentProcessStep = processStep;

        TimeEntryAdded?.Invoke(this, entry);
        CurrentProcessStepChanged?.Invoke(this, processStep);
    }

    /// <summary>
    /// Saves a recording to a JSON file.
    /// Speichert eine Aufnahme in einer JSON-Datei.
    /// </summary>
    /// <param name="recording">The recording to save.</param>
    public void SaveRecording(TimeStudyRecording recording)
    {
        string filePath = PathHelper.GetRecordingFilePath(
            _baseDirectory,
            recording.DefinitionName,
            recording.Id,
            recording.StartedAt);

        JsonSerializerHelper.SerializeToFile(recording, filePath);
    }

    /// <summary>
    /// Loads a recording from a JSON file.
    /// Lädt eine Aufnahme aus einer JSON-Datei.
    /// </summary>
    /// <param name="filePath">The file path to load from.</param>
    /// <returns>The loaded recording, or null if not found.</returns>
    public TimeStudyRecording? LoadRecording(string filePath)
    {
        return JsonSerializerHelper.DeserializeFromFile<TimeStudyRecording>(filePath);
    }

    /// <summary>
    /// Gets all recordings for a specific definition.
    /// Ruft alle Aufnahmen für eine bestimmte Definition ab.
    /// </summary>
    /// <param name="definition">The definition to get recordings for.</param>
    /// <returns>A list of recordings for the definition.</returns>
    public List<TimeStudyRecording> GetAllRecordingsForDefinition(TimeStudyDefinition definition)
    {
        string[] files = PathHelper.GetRecordingFilesForDefinition(_baseDirectory, definition.Name);
        var recordings = new List<TimeStudyRecording>();

        foreach (string file in files)
        {
            var recording = JsonSerializerHelper.DeserializeFromFile<TimeStudyRecording>(file);
            if (recording != null && recording.DefinitionId == definition.Id)
            {
                recordings.Add(recording);
            }
        }

        return recordings.OrderBy(r => r.StartedAt).ToList();
    }

    /// <summary>
    /// Cancels the current recording without saving.
    /// Bricht die aktuelle Aufnahme ab, ohne zu speichern.
    /// </summary>
    public void CancelRecording()
    {
        if (_stopwatch != null)
        {
            _stopwatch.Stop();
        }

        _currentRecording = null;
        _stopwatch = null;
        _lastEntry = null;
        _currentProcessStep = null;

        RecordingStateChanged?.Invoke(this, EventArgs.Empty);
        CurrentProcessStepChanged?.Invoke(this, null);
    }
}