

using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace Tests;

public class FileService_Tests
{
    string _testPath = Path.GetTempPath();
    IFileService _service;

    public FileService_Tests()
    {
        _service = new FileService(_testPath);
    }

    [Fact]
    public void WriteFile_ShouldWriteContentToFile_AndReturnTrue()
    {
        bool result = _service.WriteFile("hello world", "WriteLines.txt");
        Assert.True(result);
    }
    [Fact]
    public void ReadFile_ShouldReadAFile_ReturnTheText()
    {
        string text = _service.ReadFile("WriteLines.txt");
        Assert.NotEmpty(text);
        Assert.IsType<string>(text);
    }
    [Fact]
    public void FileExistShould_ReturnBoolIfFileExist()
    {
        bool NoneExistingFile = _service.FileExist("Filmjölk.txt");
        bool existingFile = _service.FileExist("WriteLines.txt");

        Assert.False(NoneExistingFile);
        Assert.True(existingFile);
    }
}
