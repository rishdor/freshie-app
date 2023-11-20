﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace freshie_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Basic Light.ttf", "BasicLight");
                    fonts.AddFont("georgia.ttf", "Georgia");

                });
                
            // Continue initializing your .NET MAUI App here

            return builder.Build();
        }
    }
}