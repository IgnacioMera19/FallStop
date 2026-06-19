namespace FallStop.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    private async void Ingresar(object sender, EventArgs e)
    {
        string rut = RutEntry.Text;
        string password = PasswordEntry.Text;

        if (rut == "" || password == "")
        {
            await DisplayAlert("Error", "Por favor, ingrese su Rut y contraseña.", "OK");
            return;
        } else if (rut != "222171237" || password != "1234")
        {
            await DisplayAlert("Error", "Rut o contraseña incorrectos.", "OK");
            return;
        }

         Application.Current.MainPage = new MainPage();
    }
}