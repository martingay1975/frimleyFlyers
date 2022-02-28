using System.Text.Json;
using System.Text.Json.Serialization;

namespace FF.DataEntry.Utils
{
    public static class JsonSerializerDefaultOptions
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}
