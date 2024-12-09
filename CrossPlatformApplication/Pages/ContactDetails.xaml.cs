using CrossPlatformApplication.ViewModels;

namespace CrossPlatformApplication.Pages;

public partial class ContactDetails : ContentPage
{
	public ContactDetails(ContactDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}