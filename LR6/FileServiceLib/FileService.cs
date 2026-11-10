using System.Text.Encodings.Web;
using System.Text.Json;
using LR6.Contracts;

namespace FileServiceLib
{
    public class FileService<T> : IFileService<T> where T : class
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public IEnumerable<T> ReadFile(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<T>>(json, _options)!;
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            string json = JsonSerializer.Serialize(data.ToList(), _options);
            File.WriteAllText(fileName, json);
        }
    }
}
