using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freshie_app.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FridgeItem> FridgeItems { get; set; }

        private static MyDbContext _instance;

        public static MyDbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MyDbContext();
                    _instance.Database.EnsureCreated();

                    if (!_instance.Products.Any())
                    {
                        var predefinedProducts = new List<Product>
                    {
                        new Product { Name = "Apple" },
                        new Product { Name = "Pasta" },
                        new Product { Name = "Milk" },
                        new Product { Name = "Eggs" }
                    };

                        _instance.Products.AddRange(predefinedProducts);
                        _instance.SaveChanges();
                    }
                }

                return _instance;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=mydatabase.db");
        }
    }
}
