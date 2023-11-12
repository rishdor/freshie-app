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

            User user = await ApiClient.LoginUser(email, password);

            if (user == null)
            {
                //FIX THIS SO IT SHOWS TYPE OF ERROR (wrong email or wrong password)
                await DisplayAlert("failed to log in", "try again", "ok");
            }
            else
            {
                //await DisplayAlert("SUCCESS", "YAAAAAY", "ok");

                //FIGURE OUT REDIRECTION
                await Navigation.PushAsync(new HomePage(user));
            }
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
