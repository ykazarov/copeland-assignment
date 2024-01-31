using System.Text.Json;
using copeland.assignment.core;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace copeland.assignment.services;

public class FileIOService<T> : IFileIOService<T>
    where T : class
{
    public void WriteToFile(string filePath, T content)
    {
        var fileContent = JsonConvert.SerializeObject(content);
        File.WriteAllText(filePath, fileContent);
    }

    public T ReadFromFile(string filePath)
    {
        var fileContent = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(fileContent);
        
        //.Deserialize<T>(fileContent, options : new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}