// See https://aka.ms/new-console-template for more information
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

IJsonService<IContact> jsonService = new JsonService<IContact>();
IFileService fileService = new FileService(@"C:\Users\Emanuel");
IRepository<IContact> repo = new ContactRepository(jsonService, fileService);

IContactService contactService = new ContactService(repo);

IContact e = ContactFactory.Create("Emanuel", "Gustafzon", "emanuel.g@gmail.com", "092929292", "benvägen 5", "123 78", "Mölndal");
IObservableContact oe = ContactFactory.CreateObservable(e);

Console.WriteLine(oe.Name);






