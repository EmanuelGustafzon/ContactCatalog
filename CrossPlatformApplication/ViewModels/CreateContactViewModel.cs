using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Collections.ObjectModel;

namespace CrossPlatformApplication.ViewModels;

public partial class CreateContactViewModel : ObservableObject
{
    IContactService _contactService;
    ContactsCollectionViewModel _contactsCollectionVm;
    public ObservableCollection<IObservableContact> Contacts => _contactsCollectionVm.Contacts;

    [ObservableProperty]
    string feedback = "";

    [ObservableProperty]
    IContact contactForm = ContactFactory.Create();
    public CreateContactViewModel(IContactService contactService, ContactsCollectionViewModel contactCollectionVm)
    {
        _contactService = contactService;
        _contactsCollectionVm = contactCollectionVm;
    }

    [RelayCommand]
    public async Task AddContact()
    {
        try
        {
            StatusResponse res =  _contactService.Add(ContactForm);
            if(res.StatusCode == 201)
            {
                Contacts.Add(ContactFactory.CreateObservable(ContactForm));
                await ReDirectHome();
                ContactForm = ContactFactory.Create();
                return;
            } 
                
            Feedback = res.Message;

        }
        catch (Exception ex)
        {
            Feedback = ex.Message;
        }
    }
    async Task ReDirectHome()
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}
