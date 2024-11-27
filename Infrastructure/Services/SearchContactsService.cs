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
    ContactRepository _contactRepository;
    IEnumerable<TransformedContact> TransformedContactList { get; set; }

    public SearchContactsService(ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        _contactRepository.Entities.CollectionChanged += ContactListChanged;
    }
    public void TransformProductList()
    {
        var contactsDataview = mlContext.Data.LoadFromEnumerable(_contactRepository.Get());
        var contactsPipeline = mlContext.Transforms.Concatenate("Features", "Name", "Lastname")
            .Append(mlContext.Transforms.Text.FeaturizeText("Features"));
        var contactTransformer = contactsPipeline.Fit(contactsDataview);

        var transformedContactsDataview = contactTransformer.Transform(contactsDataview);
        TransformedContactList = mlContext.Data.CreateEnumerable<TransformedContact>(transformedContactsDataview, reuseRowObject: false);
    }
    public void ContactListChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        TransformProductList();
    }
}
