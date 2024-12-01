using Infrastructure.Interfaces;
using Infrastructure.Models.Enums;

namespace Infrastructure.Services;
public class ContactService : IContactService
{
    IRepository<IContact> _contactRepository;
    public ContactService(IRepository<IContact> contactRepository)
    {
        _contactRepository = contactRepository;
        PopulateListFromFile();
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
    public int Add(IContact contact)
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.Name == contact.Name && item.Lastname == contact.Lastname))
            return (int)StatusCodes.Duplicate;

        _contactRepository.Add(contact);
        SaveChanges();
        return (int)StatusCodes.OK;
    }
    public int Update(string id, IContact contact)
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.ID != id && item.Name == contact.Name && item.Lastname == contact.Lastname))
            return (int)StatusCodes.Duplicate;
        if (GetByID(id) != null) return (int)StatusCodes.BadRequest;
        _contactRepository.Update(id, contact);
        SaveChanges();
        return (int)StatusCodes.OK;
    }
    public int Delete(string id)
    {
        if (_contactRepository.Get(id) == null) return (int)StatusCodes.BadRequest;
        _contactRepository.Delete(id);
        SaveChanges();
        return (int)StatusCodes.OK;
    }
    private void SaveChanges()
    {
        _contactRepository.SaveChanges();
    }

    private void PopulateListFromFile()
    {
        _contactRepository.PopulateListFromFile();
    }
}
