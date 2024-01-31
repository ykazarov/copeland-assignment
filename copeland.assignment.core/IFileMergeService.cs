namespace copeland.assignment.core;

public interface IFileMergeService<T>
    where T : class
{
    void Merge<T>(string file1, string file2, string outputFile);
}