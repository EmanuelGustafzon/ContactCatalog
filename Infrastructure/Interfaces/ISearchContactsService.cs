namespace Infrastructure.Interfaces
{
    public interface ISearchContactsService
    {
        public IEnumerable<IContactEntity>? SearchContact(string searchTerm);
    }
}
