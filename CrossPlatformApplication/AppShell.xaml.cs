using CrossPlatformApplication.Pages;

namespace CrossPlatformApplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContactDetails), typeof(ContactDetails));
        }
    }
}
