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
    IJsonService<IContact> _jsonService;
    public ObservableCollection<IObservableContact> Contacts => _contactsCollectionVm.Contacts;

    public ContactsViewModel(IContactService contactService, ContactsCollectionViewModel contactsCollectionVm, IJsonService<IContact> jsonService)
    {
        _contactService = contactService;
        _contactsCollectionVm = contactsCollectionVm;
        _jsonService = jsonService;
        _ = GetAllContacts();
    }

    public async Task GetAllContacts()
    {
        try
        {
            if(!_contactService.GetAll().Any()) 
                await AddDefaultContactsFromMauiAsset();

            IEnumerable<IContact> allContacts = _contactService.GetAll();

            IEnumerable<IObservableContact>? observableContacts = allContacts
                .Select(contact => ContactFactory.CreateObservable(contact))
                .OrderBy(contact => contact.Name);
            foreach (var contact in observableContacts)
            {
                Contacts.Add(contact);
            }
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    async Task AddDefaultContactsFromMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("DefaultContacts.json");
        using var reader = new StreamReader(stream);

        var jsonContacts = reader.ReadToEnd();
        List<IContact>? contacts = _jsonService.Deserialize(jsonContacts);
        contacts?.ForEach(contact => _contactService.Add(contact));
    }

    [RelayCommand]
    async Task NavigateToDetails(string id)
    {
        await Shell.Current.GoToAsync($"{nameof(EditContact)}?Id={id}");
    }
}
