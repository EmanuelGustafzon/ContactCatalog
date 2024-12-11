namespace Infrastructure.Interfaces;
public interface IFileService
{
    string ReadFile(string fileName);
    bool WriteFile(string content, string fileName);
    public bool FileExist(string fileName);
}
