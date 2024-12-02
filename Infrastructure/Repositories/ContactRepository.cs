using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ContactRepository : BaseRepository<IContactEntity>
{
    public ContactRepository(IJsonService<IContactEntity> jsonService, IFileService fileService)
        : base(jsonService, fileService, "Contacts.json") 
    {
        PopulateListFromFile();
    }
    public override bool Delete(string id)
    {
        try
        {
            IContactEntity? contact = Entities.FirstOrDefault(x => x.ID == id);
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

    public override IContactEntity? Get(string id)
    {
        IContactEntity? foundContact = Entities.FirstOrDefault(x => x.ID == id);
        return foundContact ?? null;
    }

    public override bool Update(string id, IContactEntity entity)
    {
        try
        {
            IContactEntity? contact = Entities.FirstOrDefault(x => x.ID == id);

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
