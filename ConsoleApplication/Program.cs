// See https://aka.ms/new-console-template for more information
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

Console.WriteLine("Hello, World!");

IContact Ben = ContactFactory.Create("Emanuel", "Larsson", "email", "Phone", "address", "Postcode", "City");
IJsonService<IContact> jsonService = new JsonService<IContact>();
IFileService fileService = new FileService(@"C:\Users\Emanuel");
var repo = new ContactRepository(jsonService, fileService, "Contacts.json");
var service = new ContactService(repo);

service.Add(Ben);
service.SaveChanges();