using freshie_app.Data;
namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private MyDbContext _context = MyDbContext.Instance;
        public HomePage(User user)
        {
            InitializeComponent();
            WelcomeLabel.Text = $"Hello {user.Name}!";
            LoadUserProducts(user);
        }

        private void LoadUserProducts(User user)
        {
            var products = from p in _context.Products
                           join f in _context.FridgeItems on p.Id equals f.ProductId
                           where f.UserId == user.Id
                           select p;
            UserProductsListView.ItemsSource = products.ToList();
        }

    }
}
