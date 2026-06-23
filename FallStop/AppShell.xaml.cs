using FallStop.Views;




namespace FallStop
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

            Routing.RegisterRoute(nameof(InformationView), typeof(InformationView));

        }
        private void OnLogoutClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginView();
        }
    }
}
