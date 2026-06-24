namespace FallStop.Views;

public partial class InformationView : BaseShake
{
    public InformationView()
    {
        InitializeComponent();
    }

    public async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}