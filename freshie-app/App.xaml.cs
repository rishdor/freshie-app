using Microsoft.Maui.Controls.PlatformConfiguration;
using freshie_app.Data;
namespace freshie_app
{
    public partial class App : Application
    {
        private MyDbContext _context = MyDbContext.Instance;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            //_context.ClearDatabase();

            var users = _context.Users.ToList();
            var products = _context.Products.ToList();
            var fridgeItems = _context.FridgeItems.ToList();

            //XML.ExportXml(users, @"C:\Users\mdoro\alltheshit\Practice\project\freshie-app-repo\freshie-app\Data\users.xml");
            //XML.ExportXml(products, @"C:\Users\mdoro\alltheshit\Practice\project\freshie-app-repo\freshie-app\Data\products.xml");
            //XML.ExportXml(fridgeItems, @"C:\Users\mdoro\alltheshit\Practice\project\freshie-app-repo\freshie-app\Data\fridgeitems.xml");
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Title = "freshie";
            const int newWidth = 400;
            const int newHeight = 700;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}