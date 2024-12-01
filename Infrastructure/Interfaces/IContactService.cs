using System.Collections.ObjectModel;

namespace Infrastructure.Interfaces;
public interface IContactService
{
    public int Add(IContact contact);
    public IEnumerable<IContact> GetAll();
    public IContact? GetByID(string id);
    public int Update(string id, IContact contact);
    public int Delete(string id);
}
