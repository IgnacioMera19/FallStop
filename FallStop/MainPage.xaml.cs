namespace FallStop
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count = count = 0;

            if (count == 1)
                CounterBtn.Text = $"Siguiente Pagina";
            else
                CounterBtn.Text = $"Siguiente Pagina";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
