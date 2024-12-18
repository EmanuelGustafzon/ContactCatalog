using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ContactRepository : BaseRepository<IContact>
{
    public ContactRepository(IJsonService<IContact> jsonService, IFileService fileService, string fileName = "Contacts.json")
        : base(jsonService, fileService, fileName) { }

    public override bool Add(IContact entity)
    {
        if (Entities.Count == 0)
            PopulateListFromFile();
        return base.Add(entity);
    }
    public override IEnumerable<IContact> Get()
    {
        if (Entities.Count == 0)
            PopulateListFromFile();
        return base.Get();
    }
    public override bool Delete(string id)
    {
        try
        {
            IContact? contact = Entities.FirstOrDefault(x => x.ID == id);
            if (contact == null) return false;
            Entities.Remove(contact);
            SaveChanges();
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public override IContact? Get(string id)
    {
        IContact? foundContact = Entities.FirstOrDefault(x => x.ID == id);
        return foundContact ?? null;
    }

    public override bool Update(string id, IContact entity)
    {
        try
        {
            IContact? contact = Entities.FirstOrDefault(x => x.ID == id);

            if (contact == null) return false;

            contact.Name = entity.Name;
            contact.Lastname = entity.Lastname;
            contact.Email = entity.Email;
            contact.Phone = entity.Phone;
            contact.Address = entity.Address;
            contact.Postcode = entity.Postcode;
            contact.City = entity.City;

            SaveChanges();

            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
