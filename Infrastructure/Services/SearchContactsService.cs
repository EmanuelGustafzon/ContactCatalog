using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using System.Numerics.Tensors;

namespace Infrastructure.Services;
public class SearchContactsService
{
    IRepository<IContact> _contactRepository;

    MLContext mlContext = new();
    List<SearchableContact> _contactsList = [];
    ITransformer _transformer = null!;
    public SearchContactsService(IRepository<IContact> contactRepository)
    {
        _contactRepository = contactRepository;
    }
    private void ConvertContacts()
    {
        _contactsList.Clear();
        var allContacts = _contactRepository.Get();
        foreach (var contact in allContacts)
        {
            _contactsList.Add(ContactFactory.CreateSearchable(contact));
        }
    }
    private void BuildPipeLine()
    {
        var contactView = mlContext.Data.LoadFromEnumerable(_contactsList);
        TextFeaturizingEstimator pipeline = mlContext.Transforms.Text.FeaturizeText("Features", "SearchTerm");
        _transformer = pipeline.Fit(contactView);
    }
    public IEnumerable<IContact> SearchContact(string searchTerm)
    {
        ConvertContacts();
        BuildPipeLine();

        // Transform Searchterm to get the features
        List<SearchText> searchData = [new SearchText { SearchTerm = searchTerm }];
        var searchView = mlContext.Data.LoadFromEnumerable(searchData);
        var transformedSerach = _transformer.Transform(searchView);
        IEnumerable<TransformedSearchText> transformedSearchEnumerable = mlContext.Data.CreateEnumerable<TransformedSearchText>(transformedSerach, reuseRowObject: false);
        float[] searchTermFeatures = transformedSearchEnumerable.First().Features;

        // Transform Contact list to get the features
        var contactView = mlContext.Data.LoadFromEnumerable(_contactsList);
        var transfomredContacts = _transformer.Transform(contactView);
        IEnumerable<TransformedSearchableContact> transformedContactsEnumerable = mlContext.Data.CreateEnumerable<TransformedSearchableContact>(transfomredContacts, reuseRowObject: false);
        
        var listOfSimilarContacts = transformedContactsEnumerable
            .OrderByDescending(x => TensorPrimitives.CosineSimilarity(x.Features, searchTermFeatures))
            .Where(x => TensorPrimitives.CosineSimilarity(x.Features, searchTermFeatures) > 0)
            .ToList();

        IEnumerable<IContact> EnumerableOfSimilarContacts = listOfSimilarContacts;
        return EnumerableOfSimilarContacts;
    }
}
