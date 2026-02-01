using System.IO;

namespace TimeStudiesWpf.Helpers;

/// <summary>
/// Helper class for building file paths for definitions and recordings.
/// Hilfsklasse zum Erstellen von Dateipfaden für Definitionen und Aufnahmen.
/// </summary>
public static class PathHelper
{
    /// <summary>
    /// The name of the definitions subdirectory.
    /// Der Name des Definitions-Unterverzeichnisses.
    /// </summary>
    private const string DefinitionsFolder = "Definitions";

    /// <summary>
    /// The name of the recordings subdirectory.
    /// Der Name des Aufnahmen-Unterverzeichnisses.
    /// </summary>
    private const string RecordingsFolder = "Recordings";

    /// <summary>
    /// Gets the definitions directory path.
    /// Gibt den Pfad des Definitionsverzeichnisses zurück.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    /// <returns>The full path to the definitions directory.</returns>
    public static string GetDefinitionsDirectory(string baseDirectory)
    {
        return Path.Combine(baseDirectory, DefinitionsFolder);
    }

    /// <summary>
    /// Gets the recordings directory path.
    /// Gibt den Pfad des Aufnahmenverzeichnisses zurück.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    /// <returns>The full path to the recordings directory.</returns>
    public static string GetRecordingsDirectory(string baseDirectory)
    {
        return Path.Combine(baseDirectory, RecordingsFolder);
    }

    /// <summary>
    /// Gets the file path for a time study definition.
    /// Gibt den Dateipfad für eine Zeitaufnahme-Definition zurück.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    /// <param name="definitionName">The name of the definition.</param>
    /// <param name="definitionId">The ID of the definition.</param>
    /// <returns>The full file path for the definition JSON file.</returns>
    public static string GetDefinitionFilePath(string baseDirectory, string definitionName, Guid definitionId)
    {
        string sanitized = SanitizeFileName(definitionName);
        string fileName = $"{sanitized}_{definitionId}.json";
        return Path.Combine(GetDefinitionsDirectory(baseDirectory), fileName);
    }

    /// <summary>
    /// Gets the file path for a time study recording.
    /// Gibt den Dateipfad für eine Zeitaufnahme zurück.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    /// <param name="definitionName">The name of the definition the recording is based on.</param>
    /// <param name="recordingId">The ID of the recording.</param>
    /// <param name="date">The date of the recording.</param>
    /// <returns>The full file path for the recording JSON file.</returns>
    public static string GetRecordingFilePath(string baseDirectory, string definitionName, Guid recordingId, DateTime date)
    {
        string sanitized = SanitizeFileName(definitionName);
        string dateStr = date.ToString("yyyyMMdd");
        string fileName = $"{sanitized}_{recordingId}_{dateStr}.json";
        return Path.Combine(GetRecordingsDirectory(baseDirectory), fileName);
    }

    /// <summary>
    /// Ensures that the definitions and recordings directories exist.
    /// Stellt sicher, dass die Definitions- und Aufnahmenverzeichnisse vorhanden sind.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    public static void EnsureDirectoriesExist(string baseDirectory)
    {
        string defsDir = GetDefinitionsDirectory(baseDirectory);
        string recsDir = GetRecordingsDirectory(baseDirectory);

        if (!Directory.Exists(defsDir))
        {
            Directory.CreateDirectory(defsDir);
        }

        if (!Directory.Exists(recsDir))
        {
            Directory.CreateDirectory(recsDir);
        }
    }

    /// <summary>
    /// Sanitizes a filename by removing invalid characters.
    /// Bereinigt einen Dateinamen, indem ungültige Zeichen entfernt werden.
    /// </summary>
    /// <param name="fileName">The filename to sanitize.</param>
    /// <returns>The sanitized filename.</returns>
    private static string SanitizeFileName(string fileName)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string sanitized = fileName;

        foreach (char c in invalidChars)
        {
            sanitized = sanitized.Replace(c, '_');
        }

        return sanitized;
    }

    /// <summary>
    /// Gets all definition files from the definitions directory.
    /// Ruft alle Definitionsdateien aus dem Definitionsverzeichnis ab.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    /// <returns>An array of file paths for all definition files.</returns>
    public static string[] GetAllDefinitionFiles(string baseDirectory)
    {
        string defsDir = GetDefinitionsDirectory(baseDirectory);

        if (!Directory.Exists(defsDir))
        {
            return Array.Empty<string>();
        }

        return Directory.GetFiles(defsDir, "*.json");
    }

    /// <summary>
    /// Gets all recording files for a specific definition.
    /// Ruft alle Aufnahmedateien für eine bestimmte Definition ab.
    /// </summary>
    /// <param name="baseDirectory">The base directory path.</param>
    /// <param name="definitionName">The name of the definition.</param>
    /// <returns>An array of file paths for all recording files for the definition.</returns>
    public static string[] GetRecordingFilesForDefinition(string baseDirectory, string definitionName)
    {
        string recsDir = GetRecordingsDirectory(baseDirectory);
        string sanitized = SanitizeFileName(definitionName);

        if (!Directory.Exists(recsDir))
        {
            return Array.Empty<string>();
        }

        return Directory.GetFiles(recsDir, $"{sanitized}_*.json");
    }
}