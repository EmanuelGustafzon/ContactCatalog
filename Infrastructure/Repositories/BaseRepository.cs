using Infrastructure.Interfaces;
using Infrastructure.Models.Enums;
using System.Collections.ObjectModel;

namespace Infrastructure.Repositories;
public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public ObservableCollection<TEntity> Entities { get; set; } = [];
    IJsonService<TEntity> _jsonService;
    IFileService _fileService;
    string _storageFileName;
    public BaseRepository(IJsonService<TEntity> jsonService, IFileService fileService, string storageFileName)
    {
        _jsonService = jsonService;
        _fileService = fileService;
        _storageFileName = storageFileName;
        PopulateListFromFile(storageFileName);
    }
    public virtual int Add(TEntity entity)
    {
        try
        {
            Entities.Add(entity);
            return (int)StatusCodes.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (int)StatusCodes.InternalError;
        }
    }
    public virtual IEnumerable<TEntity> Get() => Entities;
    public abstract TEntity? Get(string id);
    public abstract int Delete(string id);
    public abstract int Update(string id, TEntity entity);
    public int SaveChanges()
    {
        try
        {
            var json = _jsonService.Serialize(Entities);
            _fileService.WriteFile(json, _storageFileName);
            return (int)StatusCodes.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (int)StatusCodes.InternalError;
        }
    }
    private void PopulateListFromFile(string fileName)
    {
        try
        {
            if (!_fileService.FileExist(fileName)) return;

            var json = _fileService.ReadFile(fileName);
            if (json == null) return;
            ObservableCollection<TEntity>? items = _jsonService.Deserialize(json);
            if (items == null) return;
            foreach (var item in items)
            {
                Entities.Add(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
