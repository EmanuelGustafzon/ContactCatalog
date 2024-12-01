using Infrastructure.Interfaces;

namespace Infrastructure.Models;

internal class SearchableContact : Contact, ISearchbaleTerm
{
    public string SearchTerm { get; set; } = null!;
}
