using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrossPlatformApplication.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    IContactService _contactService;

    public ObservableCollection<IContact> Contacts { get; set; } = [];

    [ObservableProperty]
    string feedback = "";
    [ObservableProperty]
    string firstname = "";
    [ObservableProperty]
    string lastname = "";
    [ObservableProperty]
    string email = "";
    [ObservableProperty]
    string phone = "";
    [ObservableProperty]
    string address = "";
    [ObservableProperty]
    string postcode = "";
    [ObservableProperty]
    string city = "";

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

    [RelayCommand]
    public void AddContact()
    {
        IContact contact = ContactFactory.Create(
            Firstname,
            Lastname,
            Email,
            Phone,
            Address,
            Postcode,
            City
            );
        _contactService.Add(contact);
        Contacts.Add(contact);
    }

}
