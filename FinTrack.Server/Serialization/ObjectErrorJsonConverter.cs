using FinTrack.Server.Entity;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinTrack.Server.Serialization;

internal class ObjectErrorJsonConverter: JsonConverter<ObjectError>
{
    public override ObjectError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new InvalidOperationException("Reading of ObjectError is not supported");
    }

    public override void Write(Utf8JsonWriter writer, ObjectError value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("type", "object");
        if (value.ErrorMessages.Count > 0)
        {
            writer.WriteStartArray("errors");
            value.ErrorMessages.ForEach(writer.WriteStringValue);
            writer.WriteEndArray();
        }
        writer.WriteStartObject("children");
        foreach (var errorPair in value.ChildErrors)
        {
            writer.WritePropertyName(errorPair.Key);
            JsonSerializer.Serialize(writer, errorPair.Value, options);
        }
        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}
