//using Android.Media;

namespace freshie_app
{
    public partial class AppShell : Shell
    {
        private DTO.User user;
        public AppShell(DTO.User user)
        {
            this.user = user;
            InitializeComponent();
            TabBar tabBar = new TabBar();
            
            //ShellContent mainPageContent = new ShellContent
            //{
            //    ContentTemplate = new DataTemplate(typeof(MainPage)),
            //    Route = "MainPage"
            //};
            //tabBar.Items.Add(mainPageContent);

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
                //ContentTemplate = new DataTemplate(typeof(HomePage)),
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
            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage)); - po poznaniu Data Binding
        }
    }
}