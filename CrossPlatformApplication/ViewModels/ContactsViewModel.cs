using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossPlatformApplication.Pages;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrossPlatformApplication.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    IContactService _contactService;
    ContactsCollectionViewModel _contactsCollectionVm;
    public ObservableCollection<IObservableContact> Contacts => _contactsCollectionVm.Contacts;

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
    async Task NavigateToDetails(string id)
    {
        await Shell.Current.GoToAsync($"{nameof(EditContact)}?Id={id}");
    }
}
