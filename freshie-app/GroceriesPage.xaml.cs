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
        ClearExistingGrid();

        var userProducts = await GetUserGroceries();

        if (userProducts == null)
        {
            WelcomeLabel.IsVisible = true;
            WelcomeLabel.Text = $"Hello {_user.Name}!\nThis is your groceries list";
        }
        else
        {
            WelcomeLabel.IsVisible = false;
            DisplayProducts(userProducts);
        }
    }

    private void ClearExistingGrid()
    {
        ScrollView existingScrollView = MainGrid.Children.OfType<ScrollView>().FirstOrDefault();
        if (existingScrollView != null)
        {
            MainGrid.Children.Remove(existingScrollView);
        }
    }

    private async Task<List<Product>> GetUserGroceries()
    {
        return await ApiClient.GetUserGroceries(_user.UserId);
    }

    private void DisplayProducts(List<Product> products)
    {
        ClearExistingGrid();

        ProductsCollectionView.ItemsSource = null;
        ProductsCollectionView.ItemsSource = products;

        var grid = CreateGridForProducts(products);

        ScrollView scrollView = new ScrollView { Content = grid };
        Grid.SetRow(scrollView, 0);
        MainGrid.Children.Add(scrollView);
    }

    private Grid CreateGridForProducts(List<Product> Products)
    {
        var grid = new Grid { };

        int columns = 3;
        int rows = (Products.Count + columns - 1) / columns;

        AddGridDefinitions(grid, columns, rows);
        AddProductButtonsToGrid(grid, Products, columns);

        return grid;
    }

    private void AddGridDefinitions(Grid grid, int columns, int rows)
    {
        for (int i = 0; i < columns; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
        }
    }

    private void AddProductButtonsToGrid(Grid grid, List<Product> userProducts, int columns)
    {
        for (int i = 0; i < userProducts.Count; i++)
        {
            var productButton = CreateProductButton(userProducts[i]);

            int row = i / columns;
            int column = i % columns;

            Grid.SetRow(productButton, row);
            Grid.SetColumn(productButton, column);

            grid.Children.Add(productButton);
        }
    }
    private Button CreateProductButton(Product product)
    {
        var productButton = new Button
        {
            FontSize = 20,
            WidthRequest = 115,
            HeightRequest = 115,
            Margin = new Thickness(10, 5, 10, 5),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        productButton.BindingContext = product;
        productButton.SetBinding(Button.TextProperty, "ProductName");

        AddTapGesturesToButton(productButton);

        return productButton;
    }

    private bool doubleTapped = false;
    private bool ignoreNextTap = false;

    private void AddTapGesturesToButton(Button button)
    {
        var singleTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
        singleTap.Tapped += OnSingleTapped;
        button.GestureRecognizers.Add(singleTap);

        var doubleTap = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
        doubleTap.Tapped += OnDoubleTapped;
        button.GestureRecognizers.Add(doubleTap);

        void OnSingleTapped(object sender, EventArgs args)
        {
            var button = (Button)sender;
            var product = (Product)button.BindingContext;

            _ = Task.Delay(200).ContinueWith(t =>
            {
                if (doubleTapped)
                {
                    doubleTapped = false;
                    ignoreNextTap = true;
                }
                else if (!ignoreNextTap)
                {
                    button.Dispatcher.Dispatch(async () =>
                    {
                        if (_isShowingAllProducts)
                        {
                            var response = await ApiClient.AddGroceriesItem(_user.UserId, product); 
                            if (response == "Product added successfully.")
                            {
                                var allProducts = (List<Product>)ProductsCollectionView.ItemsSource;
                                allProducts.Remove(product);
                                DisplayProducts(allProducts);
                            }
                        }
                        else
                        {
                            await ApiClient.AddProduct(_user.UserId, product); //add to fidge
                            await ApiClient.DeleteGroceries(_user.UserId, product); //remove from groceries
                            await LoadGroceries();
                        }
                    });
                }
                else
                {
                    ignoreNextTap = false;
                }
            });
        }

        void OnDoubleTapped(object sender, EventArgs args)
        {
            var button = (Button)sender;
            var product = (Product)button.BindingContext;
            doubleTapped = true;
            Task.Delay(200).ContinueWith(t =>
            {
                if (doubleTapped)
                {
                    button.Dispatcher.Dispatch(async () =>
                    {
                        if (!_isShowingAllProducts)
                        {
                            await ApiClient.DeleteGroceries(_user.UserId, product); //remove from groceries
                            await LoadGroceries();
                        }
                    });
                    doubleTapped = false;
                }
            });
        }
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        _isShowingAllProducts = !_isShowingAllProducts;
        AddProduct.Text = _isShowingAllProducts ? "Show my groceries" : "Add products";
        if (_isShowingAllProducts)
        {
            var allProducts = await ApiClient.GetAllProducts();
            var groceries = await ApiClient.GetUserGroceries(_user.UserId);
            if (groceries != null)
            {
                var availableProducts = allProducts.Except(groceries, new ProductComparer()).ToList();
                DisplayProducts(availableProducts);
            }
            else
            {
                DisplayProducts(allProducts);
            }
        }
        else
        {
            await LoadGroceries();
        }
    }
}
