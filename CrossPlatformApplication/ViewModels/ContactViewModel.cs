using Infrastructure.Interfaces;

namespace CrossPlatformApplication.ViewModels;

public class ContactViewModel
{
    IContact _contact;

    public string ID => _contact.ID;
    public string Name => _contact.Name;
    public string Lastname => _contact.Lastname;
    public string Email => _contact.Email;
    public string Phone => _contact.Phone;
    public string Address => _contact.Address;
    public string PostCode => _contact.Postcode;
    public string City => _contact.City;

    public ContactViewModel(IContact contact) 
    { 
        _contact = contact;
    }
}
