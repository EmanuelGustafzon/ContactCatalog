namespace Infrastructure.Interfaces;
public interface IFileService
{
    string ReadFile(string fileName);
    void WriteFile(string content, string filePath);
    public bool FileExist(string fileName);
}
