using freshie_app.DTO;
namespace freshie_app;

public partial class GroceriesPage : ContentPage
{
    private User _user;
    private bool _isShowingAllProducts;

    public GroceriesPage(User user)
    {
        InitializeComponent();
        _user = user;
        WelcomeLabel.Text = $"Hello {_user.Name}!\nThis is your groceries list";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadGroceries();
    }

    private async Task LoadGroceries()
    {
        var groceries = await ApiClient.GetUserGroceries(_user.UserId);
        ProductsCollectionView.ItemsSource = groceries;
    }

    private async void OnToggleViewButtonClicked(object sender, EventArgs e)
    {
        _isShowingAllProducts = !_isShowingAllProducts;
        ToggleViewButton.Text = _isShowingAllProducts ? "Show My Groceries" : "Show All Products";
        if (_isShowingAllProducts)
        {
            var allProducts = await ApiClient.GetAllProducts();
            ProductsCollectionView.ItemsSource = allProducts;
        }
        else
        {
            await LoadGroceries();
        }
    }

    private async void OnAddToGroceriesButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var product = (Product)button.BindingContext;
        if (_isShowingAllProducts)
        {
            await ApiClient.AddGroceriesItem(_user.UserId, product);
        }
        else
        {
            await ApiClient.DeleteGroceries(_user.UserId, product);
        }
        if (_isShowingAllProducts)
        {
            var allProducts = await ApiClient.GetAllProducts();
            ProductsCollectionView.ItemsSource = allProducts;
        }
        else
        {
            await LoadGroceries();
        }
    }

}
