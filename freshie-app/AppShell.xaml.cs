﻿namespace freshie_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage)); - po poznaniu Data Binding
        }
    }
}