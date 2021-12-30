using System.Text.Json;

namespace FF.DataEntry.Utils
{
    public static class JsonSerializerDefaultOptions
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
