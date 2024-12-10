using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossPlatformApplication.Pages;
using Infrastructure.Interfaces;
using System.Collections.ObjectModel;

namespace CrossPlatformApplication.ViewModels;

public partial class SearchContactsViewModel : ObservableObject
{
    ISearchContactsService _searchContactsService;
    public SearchContactsViewModel(ISearchContactsService searchContactsService)
    {
        _searchContactsService = searchContactsService;
    }
    public ObservableCollection<ContactViewModel> SearchResult { get; set; } = [];

    [ObservableProperty]
    public string searchterm = "";

    [ObservableProperty]
    public string noResultFound = "";

    [RelayCommand]
    public void SearchContacts()
    {
        SearchResult.Clear();
        IEnumerable<IContact>? list = _searchContactsService.SearchContact(Searchterm);
        if (list != null && list.Any())
        {
            NoResultFound = "";
            foreach (var contact in list)
            {
                SearchResult.Add(new ContactViewModel(contact));
            }
        } else
        {
            NoResultFound = $"Sorry, No Results found similar to {Searchterm}";
        }
    }
    [RelayCommand]
    async Task NavigateToDetails(string id)
    {
        await Shell.Current.GoToAsync($"{nameof(EditContact)}?Id={id}");
        Searchterm = "";
        SearchResult.Clear();
    }
}
