using Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrossPlatformApp.ViewModels;

internal class ContactsViewModel
{
    //IContactService _contactService;
    public ObservableCollection<IContact> Contacts { get; set; } = [];

    public ContactsViewModel() 
    {
        //_contactService = contactService;
    }
    //public void GetAll()
    //{
    //    try
    //    {
    //        IEnumerable<IContact> contacts = _contactService.GetAll();
    //        foreach (var contact in contacts)
    //        {
    //            Contacts.Add(contact);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex);
    //    }
    //}

}
