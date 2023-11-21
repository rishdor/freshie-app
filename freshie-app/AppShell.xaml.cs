//using Android.Media;
using freshie_app.DTO;

namespace freshie_app
{
    public partial class AppShell : Shell
    {
        private User user;
        public AppShell(User user)
        {
            this.user = user;
            InitializeComponent();
            TabBar tabBar = new TabBar();

            ShellContent recipiesPageContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(RecipiesPage)),
                Title = "Recipies",
                Route = "RecipiesPage",
                Icon = "plate.png"
            };
            tabBar.Items.Add(recipiesPageContent);

            ShellContent homePageContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(() => new HomePage(user)),
                Title = "Home",
                Route = "HomePage",
                Icon = "home.png"
            };
            tabBar.Items.Add(homePageContent);

            ShellContent groceriesPageContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(GroceriesPage)),
                Title = "Groceries",
                Route = "GroceriesPage",
                Icon = "list.png"
            };
            tabBar.Items.Add(groceriesPageContent);

            Items.Add(tabBar);
            
        }
    }
}