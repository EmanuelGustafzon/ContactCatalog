namespace Infrastructure.Interfaces;
public interface IFileService : IFileReader, IFileWriter
{
    public bool FileExist(string fileName);
}
