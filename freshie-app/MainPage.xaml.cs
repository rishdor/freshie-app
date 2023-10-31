using Microsoft.Maui.Controls;

namespace freshie_app
{
    public partial class MainPage : ContentPage
    {
        public interface IDatabase
        {
            User GetUser(string email);
            void AddUser(User user);
        }
        public class User
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
        }
        public class MockDatabase : IDatabase
        {
            private List<User> users = new List<User>
        {
            new User { Email = "rish", Password = "1111", Name = "Rish"},
            new User { Email = "flame", Password = "2222", Name = "Flame"},
            new User { Email = "rain", Password = "3333", Name = "Rain"}
        };
            public User GetUser(string email)
            {
                return users.FirstOrDefault(u => u.Email == email);
            }
            public void AddUser(User user)
            {
                users.Add(user);
            }
        }
        public class LoginPanel
        {
            private IDatabase database;
            public LoginPanel(IDatabase database)
            {
                this.database = database;
            }
            public User Login(string email, string password)
            {
                var user = database.GetUser(email);
                if (user != null && user.Password == password)
                {
                    return user;
                }
                return null;
            }
        }
        private LoginPanel loginPanel;
        public class RegisterPanel
        {
            private IDatabase database;
            public RegisterPanel(IDatabase database)
            {
                this.database = database;
            }
            public void Register(string email, string password, string name)
            {
                User user = new User { Email = email, Password = password, Name = name };
                database.AddUser(user);
            }
        }
        public MainPage()
        {
            InitializeComponent();

            loginPanel = new LoginPanel(new MockDatabase());
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string login = EmailEntry.Text;
            string password = PasswordEntry.Text;

            var user = loginPanel.Login(login, password);

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
            //await Shell.Current.GoToAsync(nameof(RegisterPage)); - inna metoda, może zmienię, jak poznam bliżej Data Binding

        }
    }
}
