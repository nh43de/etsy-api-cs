using System;
using System.Web;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EtsyApi.Extensions
{
    public class EscapeHtmlEncodedStringConverter : JsonConverter<string>
    {
        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var s = (string)reader.Value?.ToString();

            var r = HttpUtility.HtmlDecode(s);

            return r;
        }
    }
}
