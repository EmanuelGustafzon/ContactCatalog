using Infrastructure.Interfaces;

namespace Infrastructure.Models;
public class SearchText : ISearchbaleTerm
{
    public string SearchTerm { get; set; } = null!;
}
public class TransformedSearchText : SearchText, ITransformedSearchbaleTerm
{
    public float[] Features { get; set; } = [];
}
