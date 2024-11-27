using Infrastructure.Interfaces;

namespace Infrastructure.Services;
public class FileService : IFileService
{
    string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public string ReadFile(string fileName)
    {
        try
        {
            using StreamReader reader = new(Path.Combine(_filePath, fileName));
            string text = reader.ReadToEnd();
            return text;
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }

    public void WriteFile(string content, string fileName)
    {
        try
        {
            using StreamWriter outputFile = new StreamWriter(Path.Combine(_filePath, fileName));
            outputFile.WriteLine(content);
        }
        catch (IOException e)
        {
            Console.WriteLine("Could not write to file:");
            Console.WriteLine(e.Message);
        }
    }
    public bool FileExist(string fileName)
    {
        return File.Exists(Path.Combine(_filePath, fileName));
    }
}
