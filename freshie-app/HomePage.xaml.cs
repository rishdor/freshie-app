using freshie_app.DTO;
using System.Collections.Generic;
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
            var _userProducts = await ApiClient.GetUserProducts(_user.UserId);
            ProductsCollectionView.ItemsSource = _userProducts;
            if (_userProducts == null)
            {
                WelcomeLabel.IsVisible = true;
                WelcomeLabel.Text = $"Hello {_user.Name}!\nYou have no products in your fridge.\nWanna add some?";
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
                        var response = await ApiClient.AddProduct(_user.UserId, product);
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
                        DateOnly? expirationDate = await ApiClient.GetExpirationDate(_user.UserId, product);
                        string currentExpirationDate = expirationDate != null ? $"Current expiration date is {expirationDate}. Do you want to change it?" : "The item doesn't have an expiration date. Do you want to add it?";

                        string newExpirationDateString = await DisplayPromptAsync("Expiration date", $"{currentExpirationDate}", "OK", "Cancel", "dd.mm.yyyy", maxLength: 10, keyboard: Keyboard.Text);

                        if (newExpirationDateString != null)
                        {
                            if (DateOnly.TryParse(newExpirationDateString, out DateOnly newExpirationDate))
                            {
                                List<FridgeItem> fridgeItems = await ApiClient.GetFridgeItems(_user.UserId);
                                var item = fridgeItems.FirstOrDefault(p => p.ProductId == product.ProductId);
                                item.ExpirationDate = newExpirationDate;
                                await ApiClient.ChangeExpirationDate(item);
                            }
                            else
                            {
                                await DisplayAlert("Error", "Invalid date format", "OK");
                            }
                        }
                    }

                }

            });
        }

        public async void OnDoubleTapped(object sender, EventArgs e)
        {
            isSingleTap = false;
            var button = (Button)sender;
            var product = (Product)button.BindingContext;
            if (_isShowingAllProducts)
            {
                string expirationDateString = await DisplayPromptAsync("Expiration date", "Enter expiration date", "OK", "Cancel", "dd.mm.yyyy");
                if (expirationDateString != null)
                {
                    if (DateOnly.TryParse(expirationDateString, out DateOnly expirationDate))
                    {
                        var response = await ApiClient.AddProduct(_user.UserId, product, expirationDate);
                        if (response == "The groceries item was added successfully.")
                        {
                            var allProducts = (List<Product>)ProductsCollectionView.ItemsSource;
                            allProducts.Remove(product);
                            ProductsCollectionView.ItemsSource = null;
                            ProductsCollectionView.ItemsSource = allProducts;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Invalid date format", "OK");
                    }
                }
            }
            else
            {
                await ApiClient.DeleteProduct(_user.UserId, product);
                await LoadUserProducts();
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
                    ProductsCollectionView.ItemsSource = availableProducts;
                }
                else
                {
                    ProductsCollectionView.ItemsSource = allProducts;
                }
            }
            else
            {
                await LoadUserProducts();
            }
        }
        private async void OnSortLabelTapped(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Sort by...", "Cancel", null, "Default", "Expiration date", "Name");
            if (action == "Name")
            {
                var _userProducts = await ApiClient.GetUserProducts(_user.UserId);
                ProductsCollectionView.ItemsSource = _userProducts.OrderBy(p => p.ProductName);
            }
            else if (action == "Expiration date")
            {
                ProductsCollectionView.ItemsSource = await ApiClient.SortByExpirationDate(_user.UserId);
            }
            else if (action == "Default")
            {
                ProductsCollectionView.ItemsSource = await ApiClient.GetUserProducts(_user.UserId);
            }
        }
    }
}
