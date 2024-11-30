using Infrastructure.Interfaces;
using Infrastructure.Models.Enums;
using System.Collections.ObjectModel;

namespace Infrastructure.Repositories;

public class ContactRepository : BaseRepository<IContact>
{
    public ObservableCollection<IContact> PublicEntities => Entities;
    public ContactRepository(IJsonService<IContact> jsonService, IFileService fileService)
        : base(jsonService, fileService, "Contacts.json") 
    {
    }
    public override ObservableCollection<IContact> GetObservableCollection()
    {
        return PublicEntities;
    }
    public override int Delete(string id)
    {
        try
        {
            IContact? contact = Entities.FirstOrDefault(x => x.ID == id);
            if (contact == null) return (int)StatusCodes.NotFound;
            Entities.Remove(contact);
            return (int)StatusCodes.OK;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (int)StatusCodes.InternalError;
        }
    }

    public override IContact? Get(string id)
    {
        IContact? foundContact = Entities.FirstOrDefault(x => x.ID == id);
        return foundContact ?? null;
    }

    public override int Update(string id, IContact entity)
    {
        try
        {
            IContact? contact = Entities.FirstOrDefault(x => x.ID == id);

            if (contact == null) return (int)StatusCodes.NotFound;

            contact.Name = entity.Name;
            contact.Lastname = entity.Lastname;
            contact.Email = entity.Email;
            contact.Phone = entity.Phone;
            contact.Address = entity.Address;
            contact.Postcode = entity.Postcode;
            contact.City = entity.City;

            return (int)StatusCodes.OK;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (int)StatusCodes.InternalError;
        }
    }
}
