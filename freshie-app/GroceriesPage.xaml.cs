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
        if (groceries == null)
        {
            WelcomeLabel.IsVisible = true;
            WelcomeLabel.Text = $"Hello {_user.Name}!\nThis is your groceries list. Want to add some items?";
            WelcomeLabel.TextColor = Color.FromArgb("#F7F2E7");
        }
        else
        {
            ProductsCollectionView.IsVisible = true;
        }
    }

    bool isSingleTap = true;

    public void OnSingleTapped(object sender, EventArgs e)
    {
        isSingleTap = true;
        var button = (Button)sender;
        var product = (Product)button.BindingContext;

        Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(200), async () =>
        {
            if (isSingleTap)
            {
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
        });
    }
    public async void OnDoubleTapped(object sender, EventArgs e)
    {
        isSingleTap = false;
        var button = (Button)sender;
        var product = (Product)button.BindingContext;
        if (!_isShowingAllProducts)
        {
            await ApiClient.AddProduct(_user.UserId, product);
            await ApiClient.DeleteGroceries(_user.UserId, product);
            await LoadGroceries();
        }
    }
    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        _isShowingAllProducts = !_isShowingAllProducts;
        WelcomeLabel.IsVisible = false;
        ProductsCollectionView.IsVisible = true;
        AddProduct.Text = _isShowingAllProducts ? "Show My Groceries" : "Show All Products";
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
                ProductsCollectionView.ItemsSource = allProducts.ToList();
            }
        }
        else
        {
            await LoadGroceries();
        }
    }
}