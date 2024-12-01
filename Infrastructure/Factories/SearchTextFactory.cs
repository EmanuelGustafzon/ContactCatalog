using Infrastructure.Models;

namespace Infrastructure.Factories;
public static class SearchTextFactory
{
    internal static SearchText Create(string searchTerm)
    {
        return new SearchText { SearchTerm = searchTerm };
    }
}
