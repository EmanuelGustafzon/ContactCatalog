using CrossPlatformApplication.ViewModels;

namespace CrossPlatformApplication.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(ContactsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}