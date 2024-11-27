namespace Infrastructure.Interfaces;
public interface IContactService
{
    public void Add(IContact contact);
    public IEnumerable<IContact> GetAll();

    public IContact? GetByID(string id);
    public void Update(string id, IContact contact);
    public void Delete(string id);
}
