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
            var users = _context.Users.ToList();
            var products = _context.Products.ToList();
            var fridgeItems = _context.FridgeItems.ToList();

            UserProductsListView.ItemsSource = users;


            //var products = _context.FridgeItems.Where(p => p.UserId == user.Id).ToList();
            //var productNames = new List<string>();

            //foreach (var product in products)
            //{
            //    var productName = _context.Products.FirstOrDefault(p => p.Id == product.ProductId)?.Name;
            //    productNames.Add(productName);
            //}

            //UserProductsListView.ItemsSource = productNames;
        }

    }
}
