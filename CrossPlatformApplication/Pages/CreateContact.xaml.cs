using CrossPlatformApplication.ViewModels;

namespace CrossPlatformApplication.Pages;

public partial class CreateContact : ContentPage
{
	public CreateContact(CreateContactViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}