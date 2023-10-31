using Microsoft.Maui.Controls.PlatformConfiguration;
using freshie_app.Data;
namespace freshie_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Title = "freshie";
            const int newWidth = 400;
            const int newHeight = 800;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}