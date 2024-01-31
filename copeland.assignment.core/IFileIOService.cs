namespace copeland.assignment.core;

public interface IFileIOService<T>
    where T : class
{
    void WriteToFile(string filePath, T content);
    
    T ReadFromFile(string filePath);
}