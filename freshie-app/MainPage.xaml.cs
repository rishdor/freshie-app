using Microsoft.Maui.Controls;

namespace freshie_app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string login = EmailEntry.Text;
            string password = PasswordEntry.Text;
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
            //await Shell.Current.GoToAsync(nameof(RegisterPage)); - inna metoda, może zmienię, jak poznam bliżej Data Binding
            
        }
    }
}
