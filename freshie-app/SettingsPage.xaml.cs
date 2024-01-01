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
		WelcomeLabel.Text = $"Welcome {_user.Name} to your settings.\nYou can change your name, password and e-mail here.";
	}
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async void OnChangePasswordClicked(object sender, EventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        //add put request
        //add change password method(user_id, old password, new password)
        //implement this
    }
	private async void ReturnToHomePage(object sender, EventArgs e)
	{
        Application.Current.MainPage = new AppShell(_user);
        await Shell.Current.GoToAsync("//HomePage");
    }
	public void OnSavedClicked(object sender, EventArgs e)
	{
        
    }
}