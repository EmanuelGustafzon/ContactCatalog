namespace Infrastructure.Interfaces;

public interface IFileWriter
{
    bool WriteFile(string content, string fileName);

}
