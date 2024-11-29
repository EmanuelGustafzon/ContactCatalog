// See https://aka.ms/new-console-template for more information
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

Console.WriteLine("Hello, World!");

IContact Alice = ContactFactory.Create("Alicia", "Larsson", "email", "Phone", "address", "Postcode", "City");
IContact Anna = ContactFactory.Create("Anna", "Hansson", "email", "Phone", "address", "Postcode", "City");
IContact Ann = ContactFactory.Create("Ann", "dan", "email", "Phone", "address", "Postcode", "City");
IContact annaLisa = ContactFactory.Create("Anna-Lisa", "Hansson", "email", "Phone", "address", "Postcode", "City");

IJsonService<IContact> jsonService = new JsonService<IContact>();
IFileService fileService = new FileService(@"C:\Users\Emanuel");
IRepository<IContact> repo = new ContactRepository(jsonService, fileService, "Contacts.json");
IContactService service = new ContactService(repo);
SearchContactsService searchService = new(service);

var searchResult = searchService.SearchContact("Dani");
foreach (var contact in searchResult)
{
    Console.WriteLine(contact.Name);
}

