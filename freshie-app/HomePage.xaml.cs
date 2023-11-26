using freshie_app.DTO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml;

namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private User _user;

        public HomePage(User user)
        {
            InitializeComponent();
            _user = user;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUserProducts(_user);
        }
        public async void LoadUserProducts(User user)
        {
            Grid existingGrid = VSL.Children.OfType<Grid>().FirstOrDefault();
            if (existingGrid != null)
            {
                VSL.Children.Remove(existingGrid);
            }

            var _userProducts = await ApiClient.GetUserProducts(_user.UserId);
            if (_userProducts == null)
            {
                WelcomeLabel.IsVisible = true;
                WelcomeLabel.Text = $"Hello {_user.Name}!\nYou have no products in your fridge.\nWanna add some?";
                WelcomeLabel.TextColor = Color.FromArgb("#F7F2E7");
            }
            else
            {
                var grid = new Grid
                {
                    
                };
                int columns = 3;
                int rows = (_userProducts.Count + columns - 1) / columns;

                for (int i = 0; i < columns; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                for (int i = 0; i < rows; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                }

                for (int i = 0; i < _userProducts.Count; i++)
                {
                    var productButton = new Button
                    {
                        Text = _userProducts[i].ProductName,
                        FontSize = 20,
                        WidthRequest = 100,
                        HeightRequest = 100,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };
                    //var product = _userProducts[i];
                    //var productButton = new ProductButton(product);

                    int row = i / columns;
                    int column = i % columns;

                    Grid.SetRow(productButton, row);
                    Grid.SetColumn(productButton, column);

                    
                    grid.Children.Add(productButton);
                }
                VSL.Children.Add(grid);
            }
        }
        public class ProductButton : Button
        {
            public ProductButton(Product product)
            {
                this.Text = product.ProductName;
                this.BackgroundColor = Color.FromArgb("#F7F2E7");
                this.WidthRequest = 100;
                this.HeightRequest = 100;
                this.VerticalOptions = LayoutOptions.Center;
                this.HorizontalOptions = LayoutOptions.Center;
                // Dodaj tutaj dodatkow¹ logikê dla przycisku produktu
            }
        }
        public void OnAddProductClicked(object sender, EventArgs e)
        {
            
        }
    }
}
