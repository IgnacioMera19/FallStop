using FallStop.Views;

namespace FallStop
{
    public partial class MainPage : ContentPage
    {
        private DateTime _ultimoAgite = DateTime.MinValue;

        public MainPage()
        {
            InitializeComponent();
        }

        // Suscribirse al evento ShakeDetected cuando la página aparece
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Accelerometer.ShakeDetected += OnShakeDetected;
            Accelerometer.Start(SensorSpeed.Game);
        }

        // Desuscribirse del evento ShakeDetected cuando la página desaparece

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Accelerometer.ShakeDetected -= OnShakeDetected;
            Accelerometer.Stop();
        }

        // Manejar el evento ShakeDetected, llamando a la página de advertencia si ha pasado suficiente tiempo desde el último agite
        private async void OnShakeDetected(object? sender, EventArgs e)
        {
            // Evita que se abra múltiples veces seguidas
            if ((DateTime.Now - _ultimoAgite).TotalSeconds < 15) return;
            _ultimoAgite = DateTime.Now;

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Navigation.PushModalAsync(new WarningView());
            });
        }
    }
}
