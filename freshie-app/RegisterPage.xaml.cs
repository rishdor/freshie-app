using System.Text.RegularExpressions;
namespace freshie_app;
using freshie_app.DTO;

public partial class RegisterPage : ContentPage
{
    public static bool CheckPasswordCriteria(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        return Regex.IsMatch(password, pattern);
    }
    public static bool ValidateEmailPattern(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
    public RegisterPage()
	{
		InitializeComponent();
	}
    private async void SignUpButtonClicked(object sender, EventArgs e)
    {
        EmailCheckDB.IsVisible = false;
        EmptyField.IsVisible = false;
        UnmatchingPasswords.IsVisible = false;
        EmailWrongFormat.IsVisible = false;
        PasswordCheck.IsVisible = false;
        TermsOfUse.IsVisible = false;

        if (string.IsNullOrWhiteSpace(Password.Text) || string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Password2.Text))
        {
            EmptyField.IsVisible = true;
            return;
        }
        if (Password.Text != Password2.Text)
        {
            UnmatchingPasswords.IsVisible = true;
            return;
        }
        if (!ValidateEmailPattern(Email.Text))
        {
            EmailWrongFormat.IsVisible = true;
            return;
        }
        if (!CheckPasswordCriteria(Password.Text))
        {
            PasswordCheck.IsVisible = true;
            return;
        }
        if (!PrivacyPolicy.IsChecked)
        {
            TermsOfUse.IsVisible = true;
            return;
        }
        string email = Email.Text;
        string password = Password.Text;
        string name = Name.Text;

        var user = new User { Email = email, Password = password, Name = name };
        string registration = await ApiClient.RegisterUser(user);
        if (registration == "A user with this email already exists.")
        {
            EmailCheckDB.IsVisible = true;
            return;
        }
        if (registration == "Registration successful.")
        {
            user = await ApiClient.LoginUser(email, password);
            
            Application.Current.MainPage = new AppShell(user);
            await Shell.Current.GoToAsync("//HomePage");
        }
        else
        {
            await DisplayAlert("Registration was unsuccessful", "Try again", "OK");
        }
    }
    private void ReturnToMainPage(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }

}