using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models.Enums;
using Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;

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
    public StatusResponse Add(IContact contact)
    {
        List<ValidationResult>? errors = ValidateContact(contact);
        if (errors != null)
        {
            var firstValidationErrorMessage = errors.First().ErrorMessage ?? "";
            return new StatusResponse { StatusCode = (int)StatusCodes.BadRequest, Message = firstValidationErrorMessage };
        }
        IEnumerable<IContact> allContacts = _contactRepository.Get();
        if (allContacts.Any(item => item.Name == contact.Name && item.Lastname == contact.Lastname))
            return new StatusResponse { StatusCode = (int)StatusCodes.Duplicate, Message = "Contact already exist" };

        bool result = _contactRepository.Add(contact);

        if (result == false) return new StatusResponse { StatusCode = (int)StatusCodes.InternalError, Message = "Internal Error" };

        return new StatusResponse { StatusCode = (int)StatusCodes.Created, Message = "Contact created successfully" };
    }
    public StatusResponse Update(string id, IContact contact)
    {
        List<ValidationResult>? errors = ValidateContact(contact);
        if (errors != null)
        {
            var firstValidationErrorMessage = errors.First().ErrorMessage ?? "";
            return new StatusResponse { StatusCode = (int)StatusCodes.BadRequest, Message = firstValidationErrorMessage };
        }

        if (_contactRepository.Get(id) == null) return new StatusResponse { StatusCode = (int)StatusCodes.NotFound, Message = "Contact not found"};

        bool result = _contactRepository.Update(id, contact);

        if (result == false) return new StatusResponse { StatusCode = (int)StatusCodes.InternalError, Message = "Could not update contact, try again" };

        return new StatusResponse { StatusCode = (int)StatusCodes.OK, Message = "Contact was successfully updated" };
    }
    public StatusResponse Delete(string id)
    {
        if (_contactRepository.Get(id) == null) return new StatusResponse { StatusCode = (int)StatusCodes.NotFound, Message = "Contact not found" };
        bool result = _contactRepository.Delete(id);

        if(result == false) return new StatusResponse { StatusCode = (int)StatusCodes.InternalError, Message = "Could not delete contact, please try again" };

        return new StatusResponse { StatusCode = (int)StatusCodes.OK, Message="The contact was successfully removed" };
    }
    public List<ValidationResult>? ValidateContact(IContact contact)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(contact);
        bool isValid = Validator.TryValidateObject(contact, validationContext, validationResults, true);

        if (isValid == false) return validationResults;
        return null;
    }
}
