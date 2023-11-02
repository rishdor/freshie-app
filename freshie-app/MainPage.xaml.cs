using Microsoft.Maui.Controls;
using freshie_app.Data;
using Microsoft.EntityFrameworkCore;

namespace freshie_app
{
    public partial class MainPage : ContentPage
    {
        private MyDbContext _context = MyDbContext.Instance;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == EmailEntry.Text);

            if (user != null)
            {
                await Navigation.PushAsync(new HomePage(user));
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
