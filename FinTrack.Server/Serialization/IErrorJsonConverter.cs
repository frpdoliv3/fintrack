using FinTrack.Server.Entity;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinTrack.Server.Serialization;
internal class IErrorJsonConverter: JsonConverter<IError>
{
    public override IError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new InvalidOperationException("Reading of IError is not supported");
    }
    public override void Write(Utf8JsonWriter writer, IError value, JsonSerializerOptions options)
    {
        if (value is StringError)
        {
            JsonSerializer.Serialize(writer, value as StringError, options);
        }
        if (value is ObjectError)
        {
            JsonSerializer.Serialize(writer, value as ObjectError, options);
        }
        if (value is ListError)
        {
            JsonSerializer.Serialize(writer, value as ListError, options);
        }
    }
}
