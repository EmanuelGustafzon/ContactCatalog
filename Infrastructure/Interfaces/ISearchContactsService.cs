namespace Infrastructure.Interfaces
{
    public interface ISearchContactsService
    {
        public IEnumerable<IContact>? SearchContact(string searchTerm);
    }
}
