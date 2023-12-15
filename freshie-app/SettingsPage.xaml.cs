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
}