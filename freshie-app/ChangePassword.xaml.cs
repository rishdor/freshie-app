
using freshie_app.DTO;
namespace freshie_app;

public partial class ChangePassword : ContentPage
{
    private User _user;
	public ChangePassword(User user)
	{
        _user = user;
        InitializeComponent();
        BindingContext = user;
    }
    private void ReturnToSettingsPage(object sender, EventArgs e)
    {
        Application.Current.MainPage = new SettingsPage(_user);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string oldPassword = OldPasswordEntry.Text;
        string newPassword = NewPasswordEntry.Text;

        UserUpdateModel currentUser = new UserUpdateModel
        {
            OldPassword = oldPassword,
            NewPassword = newPassword,
            Email = _user.Email,
            Name = _user.Name
        };

        HttpResponseMessage response = await ApiClient.ChangeUserDetails(_user.UserId, currentUser);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Success", "Password changed successfully.", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Failed to change password.", "OK");
        }
    }

}