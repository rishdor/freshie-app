namespace freshie_app;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}
    private async void SignUpButtonClicked(object sender, EventArgs e)
    {
        if (PrivacyPolicy.IsChecked && Password.Text == Password2.Text) // jesli uzytkownik wszystko zaznaczyl dobrze
        {
            string name = Name.Text;
            string email = Email.Text;
            string password = Password.Text;

            // ...
        }
        else if (!PrivacyPolicy.IsChecked) 
        {
            await DisplayAlert("Terms of Use", "Please make sure you checked all required fields.", "OK");
        }
        else if (Password.Text!=Password2.Text)
        {
            await DisplayAlert("Incorrect password", "Please make sure you entered the same password twice.", "OK");
        }
        
    }
}