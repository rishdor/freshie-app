using freshie_app.DTO;

namespace freshie_app;

public partial class SettingsPage : ContentPage
{
	private User _user;

	public SettingsPage(User user)
	{
		_user = user;
		InitializeComponent();
		BindingContext = user;
		WelcomeLabel.Text = $"Welcome to your settings {_user.Name}.";
	}
	private async void ReturnToHomePage(object sender, EventArgs e)
	{
        Application.Current.MainPage = new AppShell(_user);
        await Shell.Current.GoToAsync("//HomePage");
    }
    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ChangePassword(_user));
		//Application.Current.MainPage = new AppShell(_user);
		//await Shell.Current.GoToAsync("//ChangePassword");
		//i have no idea whats going on here and how to navigate to the change password page
	}
}