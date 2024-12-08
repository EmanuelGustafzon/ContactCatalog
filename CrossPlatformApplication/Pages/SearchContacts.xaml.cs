using CrossPlatformApplication.ViewModels;

namespace CrossPlatformApplication.Pages;

public partial class SearchContacts : ContentPage
{
	public SearchContacts(SearchContactsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}