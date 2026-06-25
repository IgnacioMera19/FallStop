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

            Routing.RegisterRoute(nameof(WarningView), typeof(WarningView));

            Routing.RegisterRoute(nameof(InformationClient), typeof(InformationClient));

        }

        // Devuelve a la página de inicio de sesión cuando se hace clic en el botón de salir
        private void OnLogoutClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginView();
        }
    }
}
