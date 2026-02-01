using System.Text.Json;
using System.Text.Json.Serialization;

namespace TimeStudiesWpf.Helpers;

/// <summary>
/// JSON converter for DateTime with ISO 8601 format.
/// JSON-Konverter für DateTime mit ISO 8601-Format.
/// </summary>
public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "o";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        return DateTime.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}