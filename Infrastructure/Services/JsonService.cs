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
    public List<TEntity>? Deserialize(string json)
    {
        List<TEntity>? deserilizedList = JsonSerializer.Deserialize<List<TEntity>>(json, options);
        return deserilizedList ?? null;
    }
    public string Serialize(List<TEntity> listOfObjects)
    {
        string json = JsonSerializer.Serialize(listOfObjects, options);
        return json;
    }
}
