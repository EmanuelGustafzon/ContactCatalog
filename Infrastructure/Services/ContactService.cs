using Infrastructure.Factories;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Infrastructure.Services;
public class ContactService : IContactService
{
    IRepository<IContact> _contactRepository;
    public ContactService(IRepository<IContact> contactRepository)
    {
        _contactRepository = contactRepository;
        _contactRepository.GetObservableCollection()
            .CollectionChanged += CollectionChanged;
    }

    public ObservableCollection<IContact> GetObservableList()
    {
        return _contactRepository.GetObservableCollection();
    }
    public IEnumerable<IContact> GetAll()
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Count() == 0) PopulateListFromFile();
        return allContacts;
    }
    public IContact? GetByID(string id)
    {
        var foundContact = _contactRepository.Get(id);
        return foundContact ?? null;
    }
    public void Add(IContact contact)
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Count() == 0) PopulateListFromFile();
        if (allContacts.Any(item => item.Name == contact.Name && item.Lastname == contact.Lastname))
            return;

        IObservableContact observableContact = ContactFactory.CreateObservable(contact);
        observableContact.PropertyChanged += CollectionPropertyChanged;
        _contactRepository.Add(observableContact);
    }
    public void Update(string id, IContact contact)
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.ID == id && item.Name == contact.Name && item.Lastname == contact.Lastname))
            return;
        _contactRepository.Update(id, contact);
    }
    public void Delete(string id)
    {
        _contactRepository.Delete(id);
    }

    private void PopulateListFromFile()
    {
        _contactRepository.PopulateListFromFile();
    }
    private void CollectionPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        _contactRepository.SaveChanges();
    }
    private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _contactRepository.SaveChanges();
    }
}
