using Infrastructure.Interfaces;
using System.Diagnostics;

namespace Infrastructure.Services;
public class FileService : IFileService
{
    string _defaultFilePath;

    public FileService(string filePath)
    {
        _defaultFilePath = filePath;
    }

    public string ReadFile(string fileName)
    {
        try
        {
            using StreamReader reader = new(Path.Combine(_defaultFilePath, fileName));
            string text = reader.ReadToEnd();
            return text;
        }
        catch (IOException e)
        {
            Debug.WriteLine(e.Message);
            return "";
        }
    }
    public bool WriteFile(string content, string fileName)
    {
        try
        {
            using StreamWriter outputFile = new StreamWriter(Path.Combine(_defaultFilePath, fileName));
            outputFile.WriteLine(content);
            return true;
        }
        catch (IOException e)
        {
            Debug.WriteLine(e.Message);
            return false;
        }
    }
    public bool FileExist(string fileName)
    {
        return File.Exists(Path.Combine(_defaultFilePath, fileName));
    }
}
