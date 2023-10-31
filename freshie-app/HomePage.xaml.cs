using freshie_app.Data;
namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private MyDbContext _context = MyDbContext.Instance;
        private User _user;
        public HomePage(User user)
        {
            InitializeComponent();
            WelcomeLabel.Text = $"Hello {user.Name}!";
            LoadUserProducts(user);
            _user = user;
        }

        private void LoadUserProducts(User user)
        {
            var products = _context.FridgeItems.Where(p => p.UserId == user.Id).ToList();
            var productNames = new List<string>();

            foreach (var product in products)
            {
                var productName = _context.Products.FirstOrDefault(p => p.Id == product.ProductId)?.Name;
                productNames.Add(productName);
            }

            UserProductsListView.ItemsSource = productNames;
        }

        private void OnShowAllProductsButtonClicked(object sender, EventArgs e)
        {
            var products = _context.Products.ToList();
            AllProductsListView.ItemsSource = products;
            AllProductsListView.IsVisible = true;
        }

        private void OnProductTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedProduct = (Product)e.Item;
            if (selectedProduct != null)
            {
                var fridgeItem = new FridgeItem { ProductId = selectedProduct.Id, UserId = _user.Id };
                _context.FridgeItems.Add(fridgeItem);
                _context.SaveChanges();

                LoadUserProducts(_user);
                AllProductsListView.IsVisible = false;
            }
        }
    }
}
