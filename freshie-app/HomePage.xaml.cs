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

            Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(200), () =>
            {
                if (isSingleTap)
                {
                    DisplayAlert("Alert", "Single tap detected", "OK");
                }

            });
        }

        public void OnDoubleTapped(object sender, EventArgs e)
        {
            isSingleTap = false;
            DisplayAlert("Alert", "Double tap detected", "OK");
        }

        public void OnAddProductClicked(object sender, EventArgs e)
        {
            
        }
    }
}
