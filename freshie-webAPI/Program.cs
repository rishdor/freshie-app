
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using freshie_webAPI.Models;

namespace freshie_webAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Freshie Web API - tester API",
                    Version = "0.9",
                });
            });

            var connection = String.Empty;
            //if (builder.Environment.IsDevelopment())
            //{
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
                connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            //}
            //else
            //{
            //    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            //}

            builder.Services.AddDbContext<FreshieDbContext>(options =>
                options.UseSqlServer(connection));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}