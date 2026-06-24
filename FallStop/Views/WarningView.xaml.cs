namespace FallStop.Views;

public partial class WarningView : ContentPage
{
	private int _segundosRestantes = 10; // Número de segundos para la cuenta regresiva
	private CancellationTokenSource _cts;
    public WarningView()
	{
		InitializeComponent();
	}

    // inicia el contador cuando la página aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _cts = new CancellationTokenSource();
        _ = StartCountdown(_cts.Token);
    }

    // Inicia la cuenta regresiva y envía la ubicación si no se cancela
    private async Task StartCountdown(CancellationToken token)
    {
        try
        {
            while (_segundosRestantes > 0)
            {
                if (token.IsCancellationRequested) return;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CancelButton.Text = $"Cancelar envío ({_segundosRestantes})";
                });

                await Task.Delay(1000);
                _segundosRestantes--;
            }

            if (!token.IsCancellationRequested)
            {
                await EnviarUbicacion();
            }
        }
        catch (Exception)
        {
            // Ignoramos cualquier error al cancelar
        }
    }

    private async void onCancelClicked(object sender, EventArgs e)
    {
        _cts?.Cancel();
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Navigation.PopModalAsync();
        });
    }

    private async Task EnviarUbicacion()
    {
        // Aquí iría la lógica para enviar la ubicación a un servidor o servicio
        // Por ahora, solo mostraremos un mensaje de éxito
        await DisplayAlert("Envío de ubicación", "Ubicación enviada con éxito.", "OK");
        await Navigation.PopAsync(); // Regresa a la página anterior después de enviar la ubicación
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _cts?.Cancel(); // Cancelar la cuenta regresiva si la página desaparece
    }
}