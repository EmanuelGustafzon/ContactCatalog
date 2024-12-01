using Infrastructure.Interfaces;

namespace Infrastructure.Models;
internal class SearchText : ISearchbaleTerm
{
    public string SearchTerm { get; set; } = null!;
}
