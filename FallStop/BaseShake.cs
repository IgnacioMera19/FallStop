using FallStop.Views;

namespace FallStop
{
    public class BaseShake : ContentPage
    {
        public static bool SensorHabilitado { get; set; } = true;
        private bool _warningAbierto = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _warningAbierto = false;

            // Solo inicia si está habilitado
            if (!SensorHabilitado) return;

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
        }

        private async void OnShakeDetected(object? sender, EventArgs e)
        {
            if (_warningAbierto || !SensorHabilitado) return;
            _warningAbierto = true;

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
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

        // Llama esto desde el botón en MainPage
        public static void ToggleSensor()
        {
            SensorHabilitado = !SensorHabilitado;

            if (!SensorHabilitado && Accelerometer.IsMonitoring)
            {
                Accelerometer.Stop();
            }
            else if (SensorHabilitado && !Accelerometer.IsMonitoring)
            {
                Accelerometer.Start(SensorSpeed.Game);
            }
        }
    }
}