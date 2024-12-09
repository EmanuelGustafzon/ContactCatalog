using CrossPlatformApplication.ViewModels;

namespace CrossPlatformApplication.Pages;

public partial class EditContact : ContentPage
{
	public EditContact(EditContactViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}