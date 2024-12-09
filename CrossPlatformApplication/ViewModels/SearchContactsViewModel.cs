using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossPlatformApplication.Pages;
using Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrossPlatformApplication.ViewModels;

public partial class SearchContactsViewModel : ObservableObject
{
    ISearchContactsService _searchContactsService;
    public SearchContactsViewModel(ISearchContactsService searchContactsService)
    {
        _searchContactsService = searchContactsService;
    }
    public ObservableCollection<IContact> SearchResult { get; set; } = [];
    [ObservableProperty]
    public string searchterm = "";

    [RelayCommand]
    public void SearchContacts()
    {
        SearchResult.Clear();
        IEnumerable<IContact>? list = _searchContactsService.SearchContact(Searchterm);
        if (list != null && list.Any())
        {
            foreach (var contact in list)
            {
                SearchResult.Add(contact);
                Debug.WriteLine(contact.Name);
            }
        }
    }
    [RelayCommand]
    async Task NavigateToDetails(string id)
    {
        await Shell.Current.GoToAsync($"{nameof(EditContact)}?Id={id}");
    }
}
