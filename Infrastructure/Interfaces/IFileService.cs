namespace Infrastructure.Interfaces;
public interface IFileService
{
    string ReadFile(string fileName);
    void WriteFile(string content, string fileName);
    void WriteFile(string content, string filePath, string fileName);
    public bool FileExist(string fileName);
}
