using System.Collections.ObjectModel;

namespace Infrastructure.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{
    public int Add(TEntity entity);
    public IEnumerable<TEntity> Get();
    public TEntity? Get(string id);
    public int Delete(string id);
    public int Update(string id, TEntity entity);
    public int SaveChanges();

    public int PopulateListFromFile();
}
