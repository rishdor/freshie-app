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
		WelcomeLabel.Text = $"Hello {_user.Name}!\nThis is your settings page.";
	}
}