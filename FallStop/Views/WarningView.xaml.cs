namespace FallStop.Views
{
    public partial class WarningView : ContentPage
    {
        private int _segundos = 10;
        private CancellationTokenSource _cts = new();
        private bool _cancelado = false;

        public WarningView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _cts = new CancellationTokenSource();
            _ = StartCountdownAsync(_cts.Token);
            _ = ObtenerUbicacionAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _cancelado = true;
            _cts?.Cancel();
        }

        private async Task StartCountdownAsync(CancellationToken token)
        {
            try
            {
                while (_segundos > 0 && !token.IsCancellationRequested)
                {
                    int seg = _segundos;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CancelButton.Text = $"Cancelar envío ({seg})";
                    });

                    await Task.Delay(1000);
                    _segundos--;
                }

                if (!_cancelado)
                {
                    await EnviarUbicacion();
                }
            }
            catch (Exception) { }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            _cancelado = true;
            _cts?.Cancel();
            await Navigation.PopModalAsync();
        }

        private async Task EnviarUbicacion()
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("Enviado", "Tu ubicación fue enviada a tu familiar.", "OK");
                await Navigation.PopModalAsync();
            });
        }

        private async Task ObtenerUbicacionAsync()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    UbicacionLabel.Text = "Permiso de ubicación denegado.";
                    return;
                }

                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(5)
                });

                if (location != null)
                {
                    UbicacionLabel.Text = $"Lat: {location.Latitude:F5}\n    Lon: {location.Longitude:F5}";
                }
                else
                {
                    UbicacionLabel.Text = "No se pudo obtener la ubicación.";
                }
            }
            catch (Exception)
            {
                UbicacionLabel.Text = "Error al obtener la ubicación.";
            }
        }
    }
}