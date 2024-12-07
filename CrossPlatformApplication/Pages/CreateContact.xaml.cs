using CrossPlatformApplication.ViewModels;

namespace CrossPlatformApplication.Pages;

public partial class CreateContact : ContentPage
{
	public CreateContact(ContactsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}