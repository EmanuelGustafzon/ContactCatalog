

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossPlatformApplication.Pages;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrossPlatformApplication.ViewModels;
[QueryProperty("Id", "Id")]
public partial class EditContactViewModel : ObservableObject
{
    IContactService _contactService;
    ContactsCollectionViewModel _contactcollectionVm;
    ObservableCollection<IObservableContact> Contacts => _contactcollectionVm.Contacts;

    [ObservableProperty]
    string id = "";
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
    [ObservableProperty]
    string feedback = "";

    public EditContactViewModel(IContactService contactService, ContactsCollectionViewModel contactsCollectionVm)
    {
        _contactService = contactService;
        _contactcollectionVm = contactsCollectionVm;
    }

    partial void OnIdChanged(string? oldValue, string newValue)
    {
        IContact? contact = _contactService.GetByID(Id);
        if (contact == null)
            return;

        Firstname = contact.Name;
        Lastname = contact.Lastname;
        Phone = contact.Phone;
        Email = contact.Email;
        Address = contact.Address;
        Postcode = contact.Postcode;
        Address = contact.Address;
        City = contact.City;
    }

    [RelayCommand]
    public async void DeleteContact()
    {
        try
        {
            StatusResponse res = _contactService.Delete(Id);
            if (res.StatusCode == 200)
            {
                var foundContact = Contacts.FirstOrDefault(x => x.ID == Id);
                if(foundContact != null)
                    Contacts.Remove(foundContact);
                Shell.Current.GoToAsync("..");
                return;
            }

            Feedback = res.Message;
                
        } catch (Exception ex) {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    public void UpdateContact()
    {
        try
        {
            IContact updatedContact = ContactFactory.Create(Firstname, Lastname, Email, Phone, Address, Postcode, City);
            StatusResponse res = _contactService.Update(Id, updatedContact);
            if (res.StatusCode == 200)
            {
                IObservableContact foundContact = Contacts.FirstOrDefault(x => x.ID == Id)!;
                foundContact.Name = updatedContact.Name;
                foundContact.Lastname = updatedContact.Lastname;
                foundContact.Email = updatedContact.Email;
                foundContact.Phone = updatedContact.Phone;
                foundContact.Address = updatedContact.Address;
                foundContact.Postcode = updatedContact.Postcode;
                foundContact.City = updatedContact.City;
                Shell.Current.GoToAsync("..");
                return;
            }
            
            Feedback = res.Message;
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Feedback = "Fail";
        }
        
    }
}
