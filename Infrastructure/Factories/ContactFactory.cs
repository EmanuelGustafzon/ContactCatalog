using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Factories;
public static class ContactFactory
{
    public static IContact Create()
    {
        IContact contact = new Contact();
        contact.ID = GenerateUniqueID.Generate();
        return new Contact();
    }
    public static IContact Create(string name, string lastname, string email, string phone, string address, string postcode, string city)
    {
        return new Contact { ID = GenerateUniqueID.Generate(), Name = name, Lastname = lastname, Email = email, Phone = phone, Address = address, Postcode = postcode, City = city };
    }
    public static IObservableContact CreateObservable(IContact contact)
    {
        return new ObservableContact
        {
            ID = contact.ID,
            Name = contact.Name,
            Lastname = contact.Lastname,
            Email = contact.Email,
            Phone = contact.Phone,
            Address = contact.Address,
            Postcode = contact.Postcode,
            City = contact.City,
        };
    }
    internal static SearchableContact CreateSearchable(IContact contact) 
    {
        SearchableContact searchableContact = new();
        searchableContact.ID = contact.ID;
        searchableContact.Name = contact.Name;
        searchableContact.Lastname = contact.Lastname;
        searchableContact.Email = contact.Email;
        searchableContact.Phone = contact.Phone;
        searchableContact.Address = contact.Address;
        searchableContact.Postcode = contact.Postcode;
        searchableContact.City = contact.City;
        searchableContact.SearchTerm = $"{contact.Name} {contact.Lastname}";

        return searchableContact;
    }
}
