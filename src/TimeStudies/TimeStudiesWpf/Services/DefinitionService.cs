using System.IO;
using TimeStudiesWpf.Helpers;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Service for managing time study definitions.
/// Dienst für die Verwaltung von Zeitaufnahme-Definitionen.
/// </summary>
public class DefinitionService
{
    private readonly string _baseDirectory;
    private readonly List<TimeStudyDefinition> _loadedDefinitions;

    /// <summary>
    /// Initializes a new instance of DefinitionService.
    /// Initialisiert eine neue Instanz von DefinitionService.
    /// </summary>
    /// <param name="baseDirectory">The base directory where definitions are stored.</param>
    public DefinitionService(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
        _loadedDefinitions = new List<TimeStudyDefinition>();
        PathHelper.EnsureDirectoriesExist(baseDirectory);
    }

    /// <summary>
    /// Creates a new time study definition.
    /// Erstellt eine neue Zeitaufnahme-Definition.
    /// </summary>
    /// <returns>The newly created definition.</returns>
    public TimeStudyDefinition CreateDefinition()
    {
        var definition = new TimeStudyDefinition
        {
            Name = "New Definition",
            Description = string.Empty,
            DefaultProcessStepOrderNumber = 0
        };

        return definition;
    }

    /// <summary>
    /// Updates an existing time study definition.
    /// Aktualisiert eine vorhandene Zeitaufnahme-Definition.
    /// </summary>
    /// <param name="definition">The definition to update.</param>
    /// <returns>True if updated successfully; otherwise, false.</returns>
    public bool UpdateDefinition(TimeStudyDefinition definition)
    {
        if (definition.IsLocked)
        {
            return false;
        }

        definition.UpdateModifiedTimestamp();
        SaveDefinitionToJson(definition);

        return true;
    }

    /// <summary>
    /// Deletes a time study definition.
    /// Löscht eine Zeitaufnahme-Definition.
    /// </summary>
    /// <param name="definition">The definition to delete.</param>
    /// <returns>True if deleted successfully; otherwise, false.</returns>
    public bool DeleteDefinition(TimeStudyDefinition definition)
    {
        try
        {
            string filePath = PathHelper.GetDefinitionFilePath(_baseDirectory, definition.Name, definition.Id);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _loadedDefinitions.Remove(definition);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a specific time study definition by ID.
    /// Ruft eine bestimmte Zeitaufnahme-Definition anhand ihrer ID ab.
    /// </summary>
    /// <param name="id">The ID of the definition.</param>
    /// <returns>The definition if found; otherwise, null.</returns>
    public TimeStudyDefinition? GetDefinition(Guid id)
    {
        var loaded = _loadedDefinitions.FirstOrDefault(d => d.Id == id);
        if (loaded != null)
        {
            return loaded;
        }

        return LoadDefinitionById(id);
    }

    /// <summary>
    /// Gets all available time study definitions.
    /// Ruft alle verfügbaren Zeitaufnahme-Definitionen ab.
    /// </summary>
    /// <returns>A list of all time study definitions.</returns>
    public List<TimeStudyDefinition> GetAllDefinitions()
    {
        LoadAllDefinitions();
        return new List<TimeStudyDefinition>(_loadedDefinitions);
    }

    /// <summary>
    /// Saves a time study definition to JSON file.
    /// Speichert eine Zeitaufnahme-Definition in einer JSON-Datei.
    /// </summary>
    /// <param name="definition">The definition to save.</param>
    public void SaveDefinitionToJson(TimeStudyDefinition definition)
    {
        string filePath = PathHelper.GetDefinitionFilePath(_baseDirectory, definition.Name, definition.Id);
        JsonSerializerHelper.SerializeToFile(definition, filePath);

        var existing = _loadedDefinitions.FirstOrDefault(d => d.Id == definition.Id);
        if (existing != null)
        {
            _loadedDefinitions.Remove(existing);
        }

        _loadedDefinitions.Add(definition);
    }

    /// <summary>
    /// Loads a time study definition from JSON file by ID.
    /// Lädt eine Zeitaufnahme-Definition aus einer JSON-Datei anhand ihrer ID.
    /// </summary>
    /// <param name="id">The ID of the definition to load.</param>
    /// <returns>The loaded definition, or null if not found.</returns>
    public TimeStudyDefinition? LoadDefinitionById(Guid id)
    {
        string[] files = PathHelper.GetAllDefinitionFiles(_baseDirectory);

        foreach (string file in files)
        {
            var definition = JsonSerializerHelper.DeserializeFromFile<TimeStudyDefinition>(file);
            if (definition != null && definition.Id == id)
            {
                var existing = _loadedDefinitions.FirstOrDefault(d => d.Id == id);
                if (existing != null)
                {
                    _loadedDefinitions.Remove(existing);
                }

                _loadedDefinitions.Add(definition);
                return definition;
            }
        }

        return null;
    }

    /// <summary>
    /// Loads a time study definition from a specific JSON file.
    /// Lädt eine Zeitaufnahme-Definition aus einer bestimmten JSON-Datei.
    /// </summary>
    /// <param name="filePath">The file path to load from.</param>
    /// <returns>The loaded definition, or null if not found.</returns>
    public TimeStudyDefinition? LoadDefinitionFromJson(string filePath)
    {
        var definition = JsonSerializerHelper.DeserializeFromFile<TimeStudyDefinition>(filePath);
        if (definition != null)
        {
            var existing = _loadedDefinitions.FirstOrDefault(d => d.Id == definition.Id);
            if (existing != null)
            {
                _loadedDefinitions.Remove(existing);
            }

            _loadedDefinitions.Add(definition);
        }

        return definition;
    }

    /// <summary>
    /// Loads all time study definitions from the definitions directory.
    /// Lädt alle Zeitaufnahme-Definitionen aus dem Definitionsverzeichnis.
    /// </summary>
    private void LoadAllDefinitions()
    {
        string[] files = PathHelper.GetAllDefinitionFiles(_baseDirectory);

        foreach (string file in files)
        {
            var definition = JsonSerializerHelper.DeserializeFromFile<TimeStudyDefinition>(file);
            if (definition != null)
            {
                var existing = _loadedDefinitions.FirstOrDefault(d => d.Id == definition.Id);
                if (existing != null)
                {
                    _loadedDefinitions.Remove(existing);
                }

                _loadedDefinitions.Add(definition);
            }
        }
    }

    /// <summary>
    /// Locks a definition to prevent further edits.
    /// Sperrt eine Definition, um weitere Bearbeitungen zu verhindern.
    /// </summary>
    /// <param name="definition">The definition to lock.</param>
    public void LockDefinition(TimeStudyDefinition definition)
    {
        definition.IsLocked = true;
        SaveDefinitionToJson(definition);
    }

    /// <summary>
    /// Creates a copy of an existing definition.
    /// Erstellt eine Kopie einer vorhandenen Definition.
    /// </summary>
    /// <param name="original">The definition to copy.</param>
    /// <returns>A new definition that is a copy of the original, unlocked.</returns>
    public TimeStudyDefinition CreateCopy(TimeStudyDefinition original)
    {
        var copy = new TimeStudyDefinition
        {
            Name = $"{original.Name} (Copy)",
            Description = original.Description,
            DefaultProcessStepOrderNumber = original.DefaultProcessStepOrderNumber,
            IsLocked = false
        };

        foreach (var step in original.ProcessSteps)
        {
            copy.ProcessSteps.Add(new ProcessStepDefinition
            {
                OrderNumber = step.OrderNumber,
                Description = step.Description,
                ProductDescription = step.ProductDescription,
                DimensionType = step.DimensionType,
                DimensionUnit = step.DimensionUnit,
                DefaultDimensionValue = step.DefaultDimensionValue,
                IsDefaultStep = step.IsDefaultStep
            });
        }

        SaveDefinitionToJson(copy);
        return copy;
    }
}