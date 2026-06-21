namespace FallStop.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    // Este método se llama cuando el usuario hace clic en el botón de ingresar. Valida el Rut y la contraseña ingresados.
    private async void Ingresar(object sender, EventArgs e)
    {
        string rut = RutEntry.Text;
        string password = PasswordEntry.Text;

        if (rut == "" || password == "")
        {
            await DisplayAlertAsync("Error", "Por favor, ingrese su Rut y contraseña.", "OK");
            return;
        } else if (rut != "222171237" || password != "1234")
        {
            await DisplayAlertAsync("Error", "Rut o contraseña incorrectos.", "OK");
            return;
        }

         Application.Current.MainPage = new MainPage();
    }
}