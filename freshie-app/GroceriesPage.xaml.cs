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
            var groceries = await ApiClient.GetUserGroceries(_user.UserId);
            if (groceries != null)
            {
                var availableProducts = allProducts.Except(groceries, new ProductComparer()).ToList();
                ProductsCollectionView.ItemsSource = availableProducts;
            }
            else
            {
                ProductsCollectionView.ItemsSource = allProducts;
            }
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
            var response = await ApiClient.AddGroceriesItem(_user.UserId, product);
            if (response == "Product added successfully.")
            {
                var allProducts = (List<Product>)ProductsCollectionView.ItemsSource;
                allProducts.Remove(product);
                ProductsCollectionView.ItemsSource = null;
                ProductsCollectionView.ItemsSource = allProducts;
            }
        }
        else
        {
            await ApiClient.DeleteGroceries(_user.UserId, product);
            await LoadGroceries();
        }
    }


}
