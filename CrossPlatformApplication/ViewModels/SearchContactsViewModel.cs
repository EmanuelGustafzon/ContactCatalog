using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System.Collections.ObjectModel;

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
            }
        }
    }
}
