// See https://aka.ms/new-console-template for more information
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

Console.WriteLine("Hello, World!");

IContact Alice = ContactFactory.Create("Alice", "Larsson", "email", "Phone", "address", "Postcode", "City");
IJsonService<IContact> jsonService = new JsonService<IContact>();
IFileService fileService = new FileService(@"C:\Users\Emanuel");
IRepository<IContact> repo = new ContactRepository(jsonService, fileService, "Contacts.json");
IContactService service = new ContactService(repo);


SearchContactsService search = new(repo);
service.Add(Alice);

