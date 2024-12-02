using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Services;
public class ContactService : IContactService
{
    IRepository<IContactEntity> _contactRepository;
    public ContactService(IRepository<IContactEntity> contactRepository)
    {
        _contactRepository = contactRepository;
    }
    public IEnumerable<IContactEntity> GetAll()
    {
        return _contactRepository.Get();
    }
    public IContact? GetByID(string id)
    {
        var foundContact = _contactRepository.Get(id);
        return foundContact ?? null;
    }

    public IContact CreateContactForm()
    {
        return ContactFactory.Create();
    }
    public StatusResponse Add(IContact contact)
    {
        IContactEntity contactEntity = ContactFactory.Create(contact); 
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(contactEntity);
        bool isValid = Validator.TryValidateObject(contactEntity, validationContext, validationResults, true);
        if(!isValid)
        {
            var firstValidationErrorMessage = validationResults.First().ErrorMessage ?? "";
            return new StatusResponse { StatusCode = (int)StatusCodes.BadRequest, Message = firstValidationErrorMessage };
        }
        IEnumerable<IContactEntity> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.Name == contact.Name && item.Lastname == contact.Lastname))
            return new StatusResponse { StatusCode = (int)StatusCodes.Duplicate, Message = "Contact already exist" };

        bool result = _contactRepository.Add(contactEntity);

        if (result == false) return new StatusResponse { StatusCode = (int)StatusCodes.InternalError, Message = "Internal Error" };

        return new StatusResponse { StatusCode = (int)StatusCodes.Created, Message = "Contact created successfully" };
    }
    public StatusResponse Update(string id, IContact contact)
    {
        IContactEntity contactEntity = ContactFactory.Create(contact);
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(contactEntity);
        bool isValid = Validator.TryValidateObject(contactEntity, validationContext, validationResults, true);
        if (!isValid)
        {
            var firstValidationErrorMessage = validationResults.First().ErrorMessage ?? "";
            return new StatusResponse { StatusCode = (int)StatusCodes.BadRequest, Message = firstValidationErrorMessage };
        }
        if (GetByID(id) != null) return new StatusResponse { StatusCode = (int)StatusCodes.NotFound, Message = "Contact not found"};
        IEnumerable<IContactEntity> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.ID != id && item.Name == contactEntity.Name && item.Lastname == contactEntity.Lastname))
            return new StatusResponse
            {
                StatusCode = (int)StatusCodes.Duplicate,
                Message = "Contact already exist"
            };

        bool result = _contactRepository.Update(id, contactEntity);

        if (result == false) return new StatusResponse { StatusCode = (int)StatusCodes.InternalError, Message = "Could not update contact, try again" };

        return new StatusResponse { StatusCode = (int)StatusCodes.OK, Message = "Contact was successfully updated" };
    }
    public int Delete(string id)
    {
        if (_contactRepository.Get(id) == null) return (int)StatusCodes.NotFound;
        bool result = _contactRepository.Delete(id);

        if(result == false) return (int)StatusCodes.InternalError;

        return (int)StatusCodes.OK;
    }
}
