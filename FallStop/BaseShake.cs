using FallStop.Views;

namespace FallStop
{
    public class BaseShake : ContentPage
    {
        private bool _warningAbierto = false;


        // Maneja la aparición de la página y comienza a monitorear el acelerómetro
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _warningAbierto = false;

            // Si el acelerómetro no está siendo monitoreado, suscribirse al evento ShakeDetected y comenzar a monitorear
            try
            {
                if (!Accelerometer.IsMonitoring)
                {
                    Accelerometer.ShakeDetected += OnShakeDetected;
                    Accelerometer.Start(SensorSpeed.Game);
                }
            }
            catch (Exception) { }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Si el acelerómetro está siendo monitoreado, dejar de monitorearlo
            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.Stop();
                Accelerometer.ShakeDetected -= OnShakeDetected;
            }
        }

        // Maneja el evento de detección de sacudida
        private async void OnShakeDetected(object? sender, EventArgs e)
        {
            if (_warningAbierto) return;
            _warningAbierto = true;

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    // Muestra la ventana de advertencia
                    var warning = new WarningView();
                    warning.Disappearing += (s, args) => _warningAbierto = false;
                    await Navigation.PushModalAsync(warning);
                }
                catch (Exception)
                {
                    _warningAbierto = false;
                }
            });
        }
    }
}