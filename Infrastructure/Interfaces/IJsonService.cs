using System.Collections.ObjectModel;

namespace Infrastructure.Interfaces;
public interface IJsonService<TEntity>
{
    public ObservableCollection<TEntity>? Deserialize(string json);

    public string Serialize(ObservableCollection<TEntity> listOfObjects);
}
