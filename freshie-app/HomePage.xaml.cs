using freshie_app.DTO;
using System.Xml;

namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private User _user;
        private List<Product> _userProducts;
        private List<Product> allProducts;
        public HomePage(User user)
        {
            InitializeComponent();
            WelcomeLabel.Text = $"Hello {user.Name}!\nuserId: {user.UserId}";
            //LoadUserProducts(user);
            _user = user;
            //_userProducts = userProducts;
        }

        //private void LoadUserProducts(User user)
        //{
        //    //userProducts = (from p in _context.Products
        //    //               join f in _context.FridgeItems on p.Id equals f.ProductId
        //    //               where f.UserId == user.Id
        //    //               select p).ToList();
        //    //UserProductsListView.ItemsSource = userProducts;

        //    //allProducts = _context.Products.ToList();            
        //    //AllProductsListView.ItemsSource = allProducts.Except(userProducts).ToList();
        //}
        //private bool isButtonClicked = false;
        //private void OnShowAllProductsButtonClicked(object sender, EventArgs e)
        //{
        //    if (!isButtonClicked)
        //    {
        //        AllProductsListView.IsVisible = true;
        //        isButtonClicked = true;
        //    }
        //    else
        //    {
        //        AllProductsListView.IsVisible = false;
        //        isButtonClicked = false;
        //    }
        //}

        //private async void OnProductTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var selectedProduct = (Product)e.Item;
        //    if (selectedProduct != null)
        //    {
        //        //string expirationDate = await DisplayPromptAsync("Expiration Date", "Please enter the expiration date:");
        //        //if (!string.IsNullOrEmpty(expirationDate))
        //        //{
        //        //    var fridgeItem = new FridgeItem
        //        //    {
        //        //        UserId = _user.Id,
        //        //        ProductId = selectedProduct.Id,
        //        //        ExpirationDate = DateTime.Parse(expirationDate)
        //        //    };
        //        //    _context.FridgeItems.Add(fridgeItem);
                    
        //        //    _context.SaveChanges();
        //        //}
        //        LoadUserProducts(_user);
        //    }
        //}
    }
}
