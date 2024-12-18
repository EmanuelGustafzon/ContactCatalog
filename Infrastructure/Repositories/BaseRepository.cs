﻿using Infrastructure.Interfaces;
using Infrastructure.Models.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Infrastructure.Repositories;
public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected List<TEntity> Entities = [];

    IJsonService<TEntity> _jsonService;
    IFileService _fileService;
    string _storageFileName;
    protected BaseRepository(IJsonService<TEntity> jsonService, IFileService fileService, string storageFileName)
    {
        _jsonService = jsonService;
        _fileService = fileService;
        _storageFileName = storageFileName;
    }
    public virtual bool Add(TEntity entity)
    {
        try
        {
            Entities.Add(entity);
            SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public virtual IEnumerable<TEntity> Get()
    {
        return Entities;
    }
    public abstract TEntity? Get(string id);

    public abstract bool Delete(string id);

    public abstract bool Update(string id, TEntity entity);

    public bool SaveChanges()
    {
        try
        {
            var json = _jsonService.Serialize(Entities);
            bool result = _fileService.WriteFile(json, _storageFileName);
            if (result == false) return false;
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public bool PopulateListFromFile()
    {
        try
        {
            var json = _fileService.ReadFile(_storageFileName);
            List<TEntity>? items = _jsonService.Deserialize(json);
            Entities = items ?? [];

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
