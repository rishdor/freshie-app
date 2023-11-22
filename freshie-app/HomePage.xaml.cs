using freshie_app.DTO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml;

namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private User _user;
        //private List<Product> _userProducts;
        //private List<Product> allProducts;
        public HomePage(User user)
        {
            InitializeComponent();
            //WelcomeLabel.Text = $"Hello {user.Name}!\nuserId: {user.UserId}";
            _user = user;
            //_userProducts = userProducts;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUserProducts(_user);
        }
        public async void LoadUserProducts(User user)
        {
            
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
                    RowSpacing = 10,
                    ColumnSpacing = 10,
                    Padding = new Thickness(20)
                };

                int columns = 3; // Maksymalna liczba przycisków w jednym rzêdzie
                for (int i = 0; i < _userProducts.Count; i++)
                {
                    int row = i / columns;
                    int column = i % columns;

                    // Jeœli to jest pierwszy element w rzêdzie, dodaj nowy rz¹d do siatki
                    if (column == 0)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    }

                    // Jeœli to jest pierwszy element w siatce, dodaj odpowiedni¹ liczbê kolumn
                    if (i == 0)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        }
                    }

                    var button = new Button
                    {
                        Text = _userProducts[i].ProductName,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };
                    button.Clicked += (s, e) =>
                    {
                        // Tutaj umieœæ kod, który ma siê wykonaæ po klikniêciu przycisku
                    };


                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    grid.Children.Add(button);
                }
                //var grid = new Grid
                //{
                //    RowSpacing = 10,
                //    ColumnSpacing = 10,

                //    Padding = new Thickness(20)
                //};
                //for (int i = 0; i < _userProducts.Count; i++)
                //{
                //    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                //    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


                //    var label = new Label
                //    {
                //        Text = _userProducts[i].ProductName,
                //        FontSize = 20,
                //        HorizontalOptions = LayoutOptions.Center,
                //        VerticalOptions = LayoutOptions.Center
                //    };

                //    grid.Children.Add(label);
                //}
                VSL.Children.Add(grid);
            }
        }
        public void OnAddProductClicked(object sender, EventArgs e)
        {
            
        }
    }
}
