using FallStop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;

namespace FallStop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}