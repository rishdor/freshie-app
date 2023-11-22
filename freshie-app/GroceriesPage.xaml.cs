using freshie_app.DTO;
namespace freshie_app;

public partial class GroceriesPage : ContentPage
{
    private User _user;
    public GroceriesPage(User user )
    {
        InitializeComponent();
        _user = user;
    }
}