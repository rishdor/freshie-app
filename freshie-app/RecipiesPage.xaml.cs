namespace freshie_app;
using freshie_app.DTO;

public partial class RecipiesPage : ContentPage
{
	private User _user;
	public RecipiesPage(User user)
	{
		InitializeComponent();
		_user = user;
	}
	protected override async void OnAppearing()
	{
        base.OnAppearing();
        await LoadRecipies();
    }
	private async Task LoadRecipies()
	{
		//ofc we need to replace products by cousines and diets but this is just for try if it actually works
        var cousines = await ApiClient.GetAllProducts();
		CousinePicker.ItemsSource = cousines.Select(p=> p.ProductName).ToList();
		var diets = await ApiClient.GetUserProducts(_user.UserId);
		DietPicker.ItemsSource = diets.Select(p => p.ProductName).ToList();
    }
}