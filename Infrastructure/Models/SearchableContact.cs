using Infrastructure.Interfaces;

namespace Infrastructure.Models;

internal class SearchableContact : ContactEntity, ISearchbaleTerm
{
    public string SearchTerm { get; set; } = null!;
}
