// See https://aka.ms/new-console-template for more information
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

Console.WriteLine("Hello, World!");

IContact Alice = ContactFactory.Create("Alicia", "Larsson", "email", "Phone", "address", "Postcode", "City");
IContact Anna = ContactFactory.Create("Anna", "Hansson", "email", "Phone", "address", "Postcode", "City");
IContact annaLisa = ContactFactory.Create("Anna-Lisa", "Hansson", "email", "Phone", "address", "Postcode", "City");

IJsonService<IContact> jsonService = new JsonService<IContact>();
IFileService fileService = new FileService(@"C:\Users\Emanuel");
IRepository<IContact> repo = new ContactRepository(jsonService, fileService, "Contacts.json");
IContactService service = new ContactService(repo);


SearchContactsService search = new(repo);
service.Add(annaLisa);
service.Add(Anna);

var list = search.SearchContact("Anita");




foreach (var contact in list)
{
    Console.WriteLine(contact.Name);
}
