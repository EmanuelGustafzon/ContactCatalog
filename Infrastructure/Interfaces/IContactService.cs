using Infrastructure.Models;
using System.Collections.ObjectModel;

namespace Infrastructure.Interfaces;
public interface IContactService
{
    public StatusResponse Add(IContact contact);
    public IEnumerable<IContactEntity> GetAll();
    public IContact? GetByID(string id);
    public StatusResponse Update(string id, IContact contact);
    public int Delete(string id);
}
