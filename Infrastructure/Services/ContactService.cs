using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Services;
public class ContactService : IContactService
{
    IRepository<IContact> _contactRepository;

    public ContactService(IRepository<IContact> contactRepository)
    {
        _contactRepository = contactRepository;
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
        foreach (var item in GetAll())
        {
            if (item.Name == contact.Name && item.Lastname == contact.Lastname) return;
        }
        _contactRepository.Add(contact);
    }
    public void Update(string id, IContact contact)
    {
        foreach (var item in GetAll())
        {
            if (item.Name == contact.Name && item.Lastname == contact.Lastname) return;
        }
        _contactRepository.Update(id, contact);
    }
    public void Delete(string id)
    {
        _contactRepository.Delete(id);
    }
    public void SaveChanges()
    {
        _contactRepository.SaveChanges();
    }
}
