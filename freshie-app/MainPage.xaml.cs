using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using freshie_app.DTO;
namespace freshie_app
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            WrongDetails.IsVisible = false;

            User user = await ApiClient.LoginUser(email, password);

            if (user == null)
            {
                WrongDetails.IsVisible = true;
            }
            else
            {
                //await Navigation.PushAsync(new HomePage(user));
                Application.Current.MainPage = new AppShell(user);
                await Shell.Current.GoToAsync("//HomePage");
            }
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
