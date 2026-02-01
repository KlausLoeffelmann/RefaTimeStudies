using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace TimeStudiesWpf.Helpers;

/// <summary>
/// Helper class for JSON serialization operations.
/// Hilfsklasse für JSON-Serialisierungsvorgänge.
/// </summary>
public static class JsonSerializerHelper
{
    /// <summary>
    /// Gets the default JSON serializer options with pretty printing enabled.
    /// Gibt die Standard-JSON-Serialisierungsoptionen mit aktivierter Pretty-Printing zurück.
    /// </summary>
    public static JsonSerializerOptions DefaultOptions { get; } = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonDateTimeConverter() }
    };

    /// <summary>
    /// Serializes an object to a JSON string.
    /// Serialisiert ein Objekt in einen JSON-String.
    /// </summary>
    /// <typeparam name="T">The type of object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <returns>The JSON string representation of the object.</returns>
    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, DefaultOptions);
    }

    /// <summary>
    /// Deserializes a JSON string to an object of the specified type.
    /// Deserialisiert einen JSON-String in ein Objekt des angegebenen Typs.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized object.</returns>
    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }

    /// <summary>
    /// Serializes an object to a file.
    /// Serialisiert ein Objekt in eine Datei.
    /// </summary>
    /// <typeparam name="T">The type of object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="filePath">The path to the file to write to.</param>
    public static void SerializeToFile<T>(T value, string filePath)
    {
        string json = Serialize(value);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Deserializes an object from a file.
    /// Deserialisiert ein Objekt aus einer Datei.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize to.</typeparam>
    /// <param name="filePath">The path to the file to read from.</param>
    /// <returns>The deserialized object.</returns>
    public static T? DeserializeFromFile<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return Deserialize<T>(json);
    }
}

