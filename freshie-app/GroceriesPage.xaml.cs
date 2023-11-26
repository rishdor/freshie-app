using freshie_app.DTO;
namespace freshie_app;

public partial class GroceriesPage : ContentPage
{
    private User _user;
    public GroceriesPage(User user )
    {
        InitializeComponent();
        _user = user;
        WelcomeLabel.IsVisible = true;
        WelcomeLabel.Text = $"Hello {_user.Name}!\nThis is your groceries list";
        WelcomeLabel.TextColor = Color.FromArgb("#F7F2E7");
    }
}