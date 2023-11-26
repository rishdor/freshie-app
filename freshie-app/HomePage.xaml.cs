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

                for (int i = 0; i < _userProducts.Count; i++)
                {
                    bool doubleTapped = false;
                    bool ignoreNextTap = false;

                    var productButton = new Button
                    {
                        Text = _userProducts[i].ProductName,
                        FontSize = 20,
                        WidthRequest = 115,
                        HeightRequest = 115,
                        Margin = new Thickness(10, 5, 10, 5),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };
                    button.Clicked += (s, e) =>
                    {
                        // Tutaj umieœæ kod, który ma siê wykonaæ po klikniêciu przycisku
                    };

                    var singleTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
                    singleTap.Tapped += OnSingleTapped;
                    productButton.GestureRecognizers.Add(singleTap);

                    var doubleTap = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
                    doubleTap.Tapped += OnDoubleTapped;
                    productButton.GestureRecognizers.Add(doubleTap);

                    void OnSingleTapped(object sender, EventArgs args)
                    {
                        Task.Delay(200).ContinueWith(t =>
                        {
                            if (doubleTapped)
                            {
                                doubleTapped = false;
                                ignoreNextTap = true;
                            }
                            else if (!ignoreNextTap)
                            {
                                productButton.Dispatcher.Dispatch(() =>
                                {
                                    DisplayAlert("Single Tap", "Single tap detected", "OK");
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
                                productButton.Dispatcher.Dispatch(() =>
                                {
                                    DisplayAlert("Double Tap", "Double tap detected", "OK");
                                });
                                doubleTapped = false;
                            }
                        });
                    }

                    int row = i / columns;
                    int column = i % columns;

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
