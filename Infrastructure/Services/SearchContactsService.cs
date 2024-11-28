using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.ML;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Infrastructure.Services;

public class SearchContactsService
{
    MLContext mlContext = new();
    IRepository<IContact> _contactRepository;

    List<SearchableContact> _contactsList = [];
    ITransformer _transformer = null!;

    public SearchContactsService(IRepository<IContact> contactRepository)
    {
        _contactRepository = contactRepository;
        _contactRepository.Entities.CollectionChanged += ContactListChanged;
    }
    void ContactListChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _contactsList = [];
        foreach (var item in _contactRepository.Get())
        {
            _contactsList.Add(ContactFactory.CreateSearchable(item));
        }
        BuildPipeLine();
    }

    private void BuildPipeLine()
    {
        var contactView = mlContext.Data.LoadFromEnumerable(_contactsList);
        TextFeaturizingEstimator pipeline = mlContext.Transforms.Text.FeaturizeText("Features", "SearchTerm");
        _transformer = pipeline.Fit(contactView);
    }

    public void Search()
    {
        List<SearchText> searchData = [new SearchText { SearchTerm = "Alice" }];
        var searchView = mlContext.Data.LoadFromEnumerable(searchData);
        var transformedSerach = _transformer.Transform(searchView);
        IEnumerable<TransformedSearchText> transformedSearchEnumerable = mlContext.Data.CreateEnumerable<TransformedSearchText>(transformedSerach, reuseRowObject: false);
        float[] searchTermFeatures = transformedSearchEnumerable.First().Features;
        Console.WriteLine(searchTermFeatures.Count());

        var contactView = mlContext.Data.LoadFromEnumerable(_contactsList);
        var transfomredContacts = _transformer.Transform(contactView);
        IEnumerable<TransformedSearchableContact> transformedContactsEnumerable = mlContext.Data.CreateEnumerable<TransformedSearchableContact>(transfomredContacts, reuseRowObject: false);

    }
}
