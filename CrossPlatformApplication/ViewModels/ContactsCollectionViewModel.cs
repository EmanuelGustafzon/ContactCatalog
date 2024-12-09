using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Interfaces;
using System.Collections.ObjectModel;

namespace CrossPlatformApplication.ViewModels;

public class ContactsCollectionViewModel
{
    public ObservableCollection<IObservableContact> Contacts { get; set; } = [];

}
