using System.Text.RegularExpressions;

namespace freshie_app;

public partial class RegisterPage : ContentPage
{
    public static bool CheckPasswordCriteria(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(password);
    }
    public RegisterPage()
	{
		InitializeComponent();
	}
    private async void SignUpButtonClicked(object sender, EventArgs e)
    {
        if (!PrivacyPolicy.IsChecked)
        {
            await DisplayAlert("Terms of Use", "Please make sure you checked all required fields.", "OK");
        }
        else if (Password.Text != Password2.Text)
        {
            await DisplayAlert("Incorrect password", "Please make sure you entered the same password twice.", "OK");
        }
        else if (!CheckPasswordCriteria(Password.Text))
        {
            PasswordCheck.IsVisible = true;
        }
        else
        {
            PasswordCheck.IsVisible = false;
            string email = Email.Text;
            string password = Password.Text;
            string name = Name.Text;

            var user = new MainPage.User { Email = email, Password = password, Name = name };

            //database.AddUser(user);
            await DisplayAlert("Successful registration!", "Proceed with login.", "OK");
        }

    }
}