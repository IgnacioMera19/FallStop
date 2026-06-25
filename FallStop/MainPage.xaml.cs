using FallStop.Views;

namespace FallStop;

public partial class MainPage : BaseShake
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void ToggleDarkMode(object sender, EventArgs e)
    {
        Application.Current.UserAppTheme =
            Application.Current.UserAppTheme == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
    }

    private bool _sensorActivo = true;

    // Función para manejar el evento de clic del botón de alternancia del sensor
    private void OnToggleSensorClicked(object sender, EventArgs e)
    {
        BaseShake.ToggleSensor();

        if (BaseShake.SensorHabilitado)
        {
            ToggleSensorButton.Text = "Desactivar sensor";
            ToggleSensorButton.BackgroundColor = Color.FromArgb("#1565C0");
            SensorStatusLabel.Text = "El sensor de caídas está monitoreando.";
        }
        else
        {
            ToggleSensorButton.Text = "Activar sensor";
            ToggleSensorButton.BackgroundColor = Color.FromArgb("#E53935");
            SensorStatusLabel.Text = "El sensor de caídas está desactivado.";
        }
    }
}