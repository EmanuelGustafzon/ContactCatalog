using Infrastructure.Interfaces;

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

        _contactRepository.Add(contact);
        SaveChanges();
    }
    public void AddMany(IEnumerable<IContact> listOfContacts)
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Count() == 0) PopulateListFromFile();
        foreach (var contact in listOfContacts)
        {
           _contactRepository.Add(contact);
        }
        SaveChanges();
    }
    public void Update(string id, IContact contact)
    {
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.ID == id && item.Name == contact.Name && item.Lastname == contact.Lastname))
            return;
        _contactRepository.Update(id, contact);
        SaveChanges();
    }
    public void Delete(string id)
    {
        _contactRepository.Delete(id);
        SaveChanges();
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
