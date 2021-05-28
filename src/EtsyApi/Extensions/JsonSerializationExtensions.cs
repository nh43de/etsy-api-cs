using System.Text.Json;

namespace EtsyApi
{
    public static class JsonSerializationExtensions
    {
        public static TObject ToObject<TObject>(this string jsonString)
        {
            var r = JsonSerializer.Deserialize<TObject>(jsonString);

            return r;
        }

        public static string ToJson(this object serializableObject, bool indent = false)
        {
            var o = new JsonSerializerOptions
            {
                WriteIndented = indent
            };
            
            return JsonSerializer.Serialize(serializableObject, o);
        }
    }
}
