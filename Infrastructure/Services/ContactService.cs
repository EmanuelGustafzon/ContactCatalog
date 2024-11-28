using Infrastructure.Factories;
using Infrastructure.Interfaces;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Infrastructure.Services;
public class ContactService : IContactService
{
    IRepository<IContact> _contactRepository;

    public ContactService(IRepository<IContact> contactRepository)
    {
        _contactRepository = contactRepository;
        _contactRepository.Entities.CollectionChanged += SaveChanges;
    }
    public IEnumerable<IContact> GetAll()
    {
        return _contactRepository.Get();
    }
    public IContact? GetByID(string id)
    {
        var foundContact = _contactRepository.Get(id);
        return foundContact ?? null;
    }
    public void Add(IContact contact)
    {
        if (GetAll().Any(item => item.Name == contact.Name && item.Lastname == contact.Lastname))
            return;

        IObservableContact observableContact = ContactFactory.CreateObservable(contact);
        observableContact.PropertyChanged += SaveChanges;
        _contactRepository.Add(observableContact);
    }
    public void Update(string id, IContact contact)
    {
        _contactRepository.Update(id, contact);
    }
    public void Delete(string id)
    {
        _contactRepository.Delete(id);
    }
    private void SaveChanges(object? sender, PropertyChangedEventArgs e)
    {
        _contactRepository.SaveChanges();
    }
    private void SaveChanges(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _contactRepository.SaveChanges();
    }
}
