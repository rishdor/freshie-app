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

            if (user == null)
            {
                UserCheck.IsVisible = true;
                PasswordCheck.IsVisible = false;
            }
            else if (PasswordEntry.Text != user.Password)
            {
                PasswordCheck.IsVisible = true;
                UserCheck.IsVisible = false;
            }
            else
            {
                await Navigation.PushAsync(new HomePage(user));
            }
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
