using Infrastructure.Interfaces;
using Infrastructure.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrossPlatformApplication.ViewModels;

public class ContactsViewModel
{
    IContactService _contactService;
    public ObservableCollection<IContact> Contacts { get; set; } = new();

    public ContactsViewModel(IContactService contactService)
    {
        _contactService = contactService;
        GetAllContacts(); 
    }

    public void GetAllContacts()
    {
        try
        {
            IEnumerable<IContact> allContacts = _contactService.GetAll();
            foreach(var contact in allContacts)
            {
                Contacts.Add(contact);
            }
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

}
