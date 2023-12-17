using freshie_app.DTO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml;

namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private User _user;
        private bool _isShowingAllProducts;

        public HomePage(User user)
        {
            InitializeComponent();
            _user = user;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadUserProducts();
        }

        private async Task LoadUserProducts()
        {
            ClearExistingGrid();

            var userProducts = await GetUserProducts();

            if (userProducts == null)
            {
                DisplayWelcomeMessage();
            }
            else
            {
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

        private async Task<List<Product>> GetUserProducts()
        {
            return await ApiClient.GetUserProducts(_user.UserId);
        }

        private void DisplayWelcomeMessage()
        {
            WelcomeLabel.IsVisible = true;
            WelcomeLabel.Text = $"Hello {_user.Name}!\nYou have no products in your fridge.\nWanna add some?";
        }

        private void DisplayProducts(List<Product> Products)
        {
            ClearExistingGrid();

            ProductsCollectionView.ItemsSource = Products;
            var grid = CreateGridForProducts(Products);

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
                Text = product.ProductName,
                FontSize = 20,
                WidthRequest = 115,
                HeightRequest = 115,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

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

            async void OnSingleTapped(object sender, EventArgs args)
            {
                var button = (Button)sender;
                string productName = (string)button.BindingContext;
                var product = (await ApiClient.GetAllProducts()).FirstOrDefault(p => p.ProductName == productName);

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
                                await ApiClient.AddProduct(_user.UserId, product);
                            }
                            else
                            {
                                var expirationDate = ApiClient.GetExpirationDate(_user.UserId, product);
                                if (expirationDate != null)
                                {
                                    DisplayAlert("Expiration date", $"Product expires on {expirationDate}", "OK");
                                }
                                else
                                {
                                    DisplayAlert("Expiration date", "Product doesn't have an expiration date", "OK");
                                }

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
                doubleTapped = true;
                Task.Delay(200).ContinueWith(t =>
                {
                    if (doubleTapped)
                    {
                        button.Dispatcher.Dispatch(() =>
                        {
                            if (_isShowingAllProducts)
                            {
                                // ask for expiration date
                            }
                            else
                            {
                                //remove from the fridge items
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
            AddProduct.Text = _isShowingAllProducts ? "Show inventory" : "Add new products";
            if (_isShowingAllProducts)
            {
                var allProducts = await ApiClient.GetAllProducts();
                var usersProdutct = await ApiClient.GetUserProducts(_user.UserId);
                if (usersProdutct != null)
                {
                    var availableProducts = allProducts.Except(usersProdutct, new ProductComparer()).ToList();
                    DisplayProducts(availableProducts);
                }
                else
                {
                    DisplayProducts(allProducts);
                }
            }
            else
            {
                await LoadUserProducts();
            }
        }
    }
}
