using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossPlatformApplication.Pages;
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
    ContactsCollectionViewModel _contactsCollectionVm;
    public ObservableCollection<IObservableContact> Contacts => _contactsCollectionVm.Contacts;

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

    public ContactsViewModel(IContactService contactService, ContactsCollectionViewModel contactsCollectionVm)
    {
        _contactService = contactService;
        _contactsCollectionVm = contactsCollectionVm;
        GetAllContacts(); 
    }

    public void GetAllContacts()
    {
        try
        {
            IEnumerable<IContact> allContacts = _contactService.GetAll();

            IEnumerable<IObservableContact>? observableContacts = allContacts.Select(contact => ContactFactory.CreateObservable(contact));
            foreach (var contact in observableContacts)
            {
                Contacts.Add(contact);
            }
            foreach(var a in Contacts)
            {
                Debug.WriteLine("---",a.Name);
            }
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    public void AddContact()
    {
        try
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
            Contacts.Add(ContactFactory.CreateObservable(contact));
            Feedback = "Success";
        }
        catch (Exception ex)
        {
            Feedback = ex.Message;
        }
    }
    [RelayCommand]
    async Task NavigateToDetails(string id)
    {
        await Shell.Current.GoToAsync($"{nameof(ContactDetails)}?Id={id}");
    }
}
