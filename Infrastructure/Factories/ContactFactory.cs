using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Factories;
public static class ContactFactory
{
    public static IContact Create()
    {
        return new Contact();
    }
    public static IContact Create(string name, string lastname, string email, string phone, string address, string postcode, string city)
    {
        return new Contact { Name = name, Lastname = lastname, Email = email, Phone = phone, Address = address, Postcode = postcode, City = city };
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
