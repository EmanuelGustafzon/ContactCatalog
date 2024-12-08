﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
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
    public CreateContactViewModel(IContactService contactService, ContactsCollectionViewModel contactCollectionVm)
    {
        _contactService = contactService;
        _contactsCollectionVm = contactCollectionVm;
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
}
