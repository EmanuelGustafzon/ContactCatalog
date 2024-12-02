// See https://aka.ms/new-console-template for more information
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

IJsonService<IContactEntity> jsonService = new JsonService<IContactEntity>();
IFileService fileService = new FileService(@"C:\Users\Emanuel");
IRepository<IContactEntity> repo = new ContactRepository(jsonService, fileService);

IContactService contactService = new ContactService(repo);
IContact emanuel = ContactFactory.Create("emanuel", "a", "s@g.s", "0761888619", "e", "e", "2");
var r = contactService.Add(emanuel);
Console.WriteLine(r.Message);

foreach (var contact in contactService.GetAll())
{
    Console.WriteLine(contact.Name);
}

ISearchContactsService search = new SearchContactsService(contactService);
var searchResult = search.SearchContact("Emanuel");

foreach(var contact in searchResult)
{
    Console.WriteLine(contact.Name);
}





