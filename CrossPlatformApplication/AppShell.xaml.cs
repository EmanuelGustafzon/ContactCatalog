using CrossPlatformApplication.Pages;

namespace CrossPlatformApplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EditContact), typeof(EditContact));
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (Application.Current == null) return;
            if (e.Value == true)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
        }
    }
}

