using System.Text.Json;

namespace FF.DataEntry.Api
{
    internal static class RaceData
    {
        public static async Task<Root> ReadAsync(string filepath)
        {
            using FileStream openStream = File.OpenRead(filepath);
            var root = await JsonSerializer.DeserializeAsync<Root>(openStream, options);
            if (root == null)
            {
                throw new Exception($"Failed to load {filepath}");
            }

            return root;
        }

        public static Root Read(string filepath)
        {
            var json = File.OpenText(filepath).ReadToEnd();
            var root = JsonSerializer.Deserialize<Root>(json, options);
            if (root == null)
            {
                throw new Exception($"Failed to load {filepath}");
            }

            return root;
        }


        public static async Task WriteAsync(Root root, string filePath)
        {
            using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, root, options);
            await createStream.DisposeAsync();
        }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
