using FF.DataEntry.Dto;
using FF.DataEntry.Utils;
using System.Text.Json;

namespace FF.DataEntry.Api
{
    internal static class RaceDataSerializer<TRoot> where TRoot : Root
    {
        public static async Task<TRoot> ReadAsync(string filepath)
        {
            using FileStream openStream = File.OpenRead(filepath);
            var root = await JsonSerializer.DeserializeAsync<TRoot>(openStream, JsonSerializerDefaultOptions.Options);
            if (root == null)
            {
                throw new Exception($"Failed to load {filepath}");
            }

            return root;
        }

        public static TRoot Read(string filepath)
        {
            var json = File.OpenText(filepath).ReadToEnd();
            var root = JsonSerializer.Deserialize<TRoot>(json, JsonSerializerDefaultOptions.Options);
            if (root == null)
            {
                throw new Exception($"Failed to load {filepath}");
            }

            return root;
        }


        public static async Task WriteAsync(TRoot root, string filePath)
        {
            using var createStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(createStream, root, JsonSerializerDefaultOptions.Options);
            await createStream.FlushAsync();
            await createStream.DisposeAsync();
        }
    }
}
