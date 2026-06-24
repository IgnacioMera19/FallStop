using FallStop.Views;

namespace FallStop
{
    public class BaseShake : ContentPage
    {
        private bool _warningAbierto = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _warningAbierto = false;

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
            if (_warningAbierto) return;
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
    }
}