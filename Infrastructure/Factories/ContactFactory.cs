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
}
