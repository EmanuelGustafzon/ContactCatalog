using Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Infrastructure.Services;
public class JsonService<TEntity> : IJsonService<TEntity> where TEntity : class
{
    JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.Preserve,
        Converters = { new JsonStringEnumConverter() }
    };
    public ObservableCollection<TEntity>? Deserialize(string json)
    {
        ObservableCollection<TEntity>? deserilizedList = JsonSerializer.Deserialize<ObservableCollection<TEntity>>(json, options);
        return deserilizedList ?? null;
    }
    public string Serialize(ObservableCollection<TEntity> listOfObjects)
    {
        string json = JsonSerializer.Serialize(listOfObjects, options);
        return json;
    }
}
