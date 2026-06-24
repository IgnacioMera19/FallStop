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
}