using Infrastructure.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Interfaces;
public interface IContactService
{
    public StatusResponse Add(IContact contact);
    public IEnumerable<IContact> GetAll();
    public IContact? GetByID(string id);
    public StatusResponse Update(string id, IContact contact);
    public StatusResponse Delete(string id);
    public List<ValidationResult>? ValidateContact(IContact contact);
}
