using Infrastructure.Interfaces;
using ConsoleApplication.Interfaces;
using Infrastructure.Factories;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Models;
using System.Reflection;
using Infrastructure.Services;

namespace ConsoleApplication.Services;

internal class MenuService : IMenu
{
    IContactService _contactService;

    public MenuService(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void ViewMenu()
    {
        while (true)
        {

            Console.Clear();
            Console.WriteLine("Contact Catalog Menu");
            Console.WriteLine("To see all your constacts write 1");
            Console.WriteLine("To Create new contact  write  2");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    viewAllContacts();
                    break;
                case "2":
                    CreateContact();
                    break;
            }
        }
    }
    public void viewAllContacts()
    {
        Console.WriteLine("|---------------------All Contacts---------------------|");
        foreach (var contact in _contactService.GetAll())
        {
            Console.WriteLine($"{contact.Name} {contact.Lastname}");
            Console.WriteLine($"{contact.Phone} {contact.Email}");
            Console.WriteLine(contact.Address);
            Console.WriteLine($"{contact.Postcode} {contact.City}");
            Console.WriteLine("|--------------------------------------------|");
        }
        Console.ReadKey();
    }
    public void CreateContact()
    {
        Console.WriteLine("---------| Create Contact |---------------");
        IContact contact = ContactFactory.Create();
        Console.WriteLine("Enter Contact firstname: ");
        contact.Name = Console.ReadLine()!;
        Console.WriteLine("Enter Contact lastname: ");
        contact.Lastname = Console.ReadLine()!;
        Console.WriteLine("Enter Contact Phone: ");
        contact.Phone = Console.ReadLine()!;
        Console.WriteLine("Enter Contact Email: ");
        contact.Email = Console.ReadLine()!;
        Console.WriteLine("Enter Contact Address: ");
        contact.Address = Console.ReadLine()!;
        Console.WriteLine("Enter Contact Postcode: ");
        contact.Postcode = Console.ReadLine()!;
        Console.WriteLine("Enter Contact City: ");
        contact.City = Console.ReadLine()!;

        List<ValidationResult>? invalidResult;
        do
        {
            invalidResult = ValidateModelService.Validate<IContact>(contact);
            if (!invalidResult.Any()) break;
            foreach (var result in invalidResult)
            {
                var contactType = contact.GetType();
                foreach(var member in result.MemberNames)
                {
                    var propertyInfo = contactType.GetProperty(member);
                    Console.WriteLine(result.ErrorMessage);
                    Console.WriteLine($"Enter Contact {member}");
                    string input = Console.ReadLine()!;
                    propertyInfo?.SetValue(contact, input);
                }
            } 
        } while (invalidResult != null);

        StatusResponse response = _contactService.Add(contact);

        Console.WriteLine(response.Message);
        Console.ReadKey();
    }
 }
