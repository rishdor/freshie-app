using freshie_app.Data;
using System.Xml;

namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private MyDbContext _context = MyDbContext.Instance;
        private User _user;
        private List<Product> products;
        public HomePage(User user)
        {
            InitializeComponent();
            WelcomeLabel.Text = $"Hello {user.Name}!";
            LoadUserProducts(user);
            _user = user;
        }

        private void LoadUserProducts(User user)
        {
            products = (from p in _context.Products
                           join f in _context.FridgeItems on p.Id equals f.ProductId
                           where f.UserId == user.Id
                           select p).ToList();
            UserProductsListView.ItemsSource = products;
        }

        //void OnShowProductsButtonClicked(object sender, EventArgs e)
        //{
        //    AvailableProductsListView.IsVisible = true;
        //    //AvailableProductsListView.ItemsSource = _context.Products.ToList();

        //    //create a list of available products (that user doesnd have)
        //    var availableproducts = _context.Products.ToList();
                
        //        //from p in _context.Products
        //        //           join f in _context.FridgeItems on p.Id equals f.ProductId //fix this so it shows all the products and not
        //        //           //only those users have
        //        //           where f.UserId != _user.Id
        //        //           select p;
        //    AvailableProductsListView.ItemsSource = products.ToList();
        //}

        private void OnShowAllProductsButtonClicked(object sender, EventArgs e)
        {
            var products = _context.Products.ToList();
            AllProductsListView.ItemsSource = products;
            AllProductsListView.IsVisible = true;
        }

        private async void OnProductTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedProduct = (Product)e.Item;
            if (selectedProduct != null)
            {
                string expirationDate = await DisplayPromptAsync("Expiration Date", "Please enter the expiration date:");
                if (!string.IsNullOrEmpty(expirationDate))
                {
                    var fridgeItem = new FridgeItem
                    {
                        UserId = _user.Id,
                        ProductId = selectedProduct.Id,
                        ExpirationDate = DateTime.Parse(expirationDate)
                    };
                    _context.FridgeItems.Add(fridgeItem);
                    _context.SaveChanges();
                }
                LoadUserProducts(_user);
                AllProductsListView.IsVisible = false;
            }
        }
    }
}
