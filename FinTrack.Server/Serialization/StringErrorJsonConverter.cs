using FinTrack.Server.Entity;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinTrack.Server.Serialization;
internal class StringErrorJsonConverter: JsonConverter<StringError>
{
    public override StringError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new InvalidOperationException("Reading of StringError is not supported");
    }

    public override void Write(Utf8JsonWriter writer, StringError value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        value.ErrorMessages.ForEach(writer.WriteStringValue);
        writer.WriteEndArray();
    }
}
