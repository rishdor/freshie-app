namespace freshie_app
{
    public partial class HomePage : ContentPage
    {
        private MainPage.User user;

        public HomePage(MainPage.User user)
        {
            this.user = user;

            var nameLabel = new Label
            {
                Text = $"Welcome, {user.Name}!",
                FontSize = 24
            };

            Content = new StackLayout
            {
                Children = { nameLabel }
            };
        }
    }
}
