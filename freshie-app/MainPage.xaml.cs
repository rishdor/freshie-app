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
        private void OnRegisterTapped(object sender, EventArgs e)
        {
            // Obsługa kliknięcia - przenieś użytkownika do strony rejestracji
            // Możesz tutaj użyć nawigacji do przekierowania użytkownika.
            // Przykład:
            // Navigation.PushAsync(new RegistrationPage());
        }
    }
}
