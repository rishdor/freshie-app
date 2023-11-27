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
            Grid existingGrid = MainGrid.Children.OfType<Grid>().FirstOrDefault();
            if (existingGrid != null)
            {
                MainGrid.Children.Remove(existingGrid);
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
                var grid = new Grid { };
                
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

                    Grid.SetRow(productButton, row);
                    Grid.SetColumn(productButton, column);

                    grid.Children.Add(productButton);
                }
                ScrollView scrollView = new ScrollView { Content = grid };
                Grid.SetRow(scrollView, 0);
                MainGrid.Children.Add(scrollView);
            }
        }
        public void OnAddProductClicked(object sender, EventArgs e)
        {
            
        }
    }
}
