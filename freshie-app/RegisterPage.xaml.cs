using System.Text.RegularExpressions;
namespace freshie_app;
using freshie_app.DTO;

public partial class RegisterPage : ContentPage
{
    //public static bool CheckPasswordCriteria(string password)
    //{
    //    string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
    //    return Regex.IsMatch(password, pattern);
    //}
    //public static bool ValidateEmailPattern(string email)
    //{
    //    string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
    //    return Regex.IsMatch(email, pattern);
    //}
    //public bool CheckEmail(string email)
    //{
    //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
    //    return user != null;
    //}
    public RegisterPage()
	{
		InitializeComponent();
	}
    private async void SignUpButtonClicked(object sender, EventArgs e)
    {
        //if (string.IsNullOrWhiteSpace(Password.Text) || string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Password2.Text))
        //{
        //    EmptyField.IsVisible = true;
        //    return;
        //}
        //EmptyField.IsVisible = false;

        //if (Password.Text != Password2.Text)
        //{
        //    UnmatchingPasswords.IsVisible = true;
        //    return;
        //}
        //UnmatchingPasswords.IsVisible = false;

        //if (!ValidateEmailPattern(Email.Text))
        //{
        //    EmailWrongFormat.IsVisible = true;
        //    return;
        //}
        //EmailWrongFormat.IsVisible = false;

        //if (CheckEmail(Email.Text))
        //{
        //    EmailCheckDB.IsVisible = true;
        //    return;
        //}
        //EmailCheckDB.IsVisible = false;

        //if (!CheckPasswordCriteria(Password.Text))
        //{
        //    PasswordCheck.IsVisible = true;
        //    return;
        //}
        //PasswordCheck.IsVisible = false;

        //if (!PrivacyPolicy.IsChecked)
        //{
        //    TermsOfUse.IsVisible = true;
        //    return;
        //}
        //TermsOfUse.IsVisible = false;

        string email = Email.Text;
        string password = Password.Text;
        string name = Name.Text;

        var user = new User { Email = email, Password = password, Name = name };
        bool registration = await ApiClient.RegisterUser(user);
        if (registration)
        {
            await DisplayAlert("successful registration", "yay", "ok");
        }
        else
        {
            await DisplayAlert("nope", "something went wrong", "ok");
        }

        //await Navigation.PushAsync(new HomePage(user));
    }

}