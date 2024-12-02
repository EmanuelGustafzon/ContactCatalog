using System.Collections.ObjectModel;

namespace Infrastructure.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{
    public bool Add(TEntity entity);
    public IEnumerable<TEntity> Get();
    public TEntity? Get(string id);
    public bool Delete(string id);
    public bool Update(string id, TEntity entity);
    public bool SaveChanges();
    public bool PopulateListFromFile();
}
