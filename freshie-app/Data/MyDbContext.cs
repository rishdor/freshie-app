using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace freshie_app.Data
{
    [XmlRoot("Database")]
    public class MyDbContext : DbContext
    {
        [XmlArray("Users"), XmlArrayItem("User")]
        public DbSet<User> Users { get; set; }
        [XmlArray("Products"), XmlArrayItem("Product")]
        public DbSet<Product> Products { get; set; }
        [XmlArray("FridgeItems"), XmlArrayItem("Item")]
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
                            new Product { Name = "Apple"},
                            new Product { Name = "Pasta" },
                            new Product { Name = "Milk" },
                            new Product { Name = "Eggs" },
                            new Product { Name = "Cheese"},
                            new Product { Name = "Carrot"},
                            new Product {Name = "Bread"},
                            new Product {Name = "Banana"}
                        };

                        _instance.Products.AddRange(predefinedProducts);
                        _instance.SaveChanges();
                    }
                    if (!_instance.Users.Any())
                    {
                        var predefinedUsers = new List<User>
                        {
                            new User { Name = "admin", Email = "1", Password= "1"},
                            new User { Name = "user1", Email = "2", Password= "2"},
                            new User {Name = "user2", Email = "3", Password= "3"},
                        };

                        _instance.Users.AddRange(predefinedUsers);
                        _instance.SaveChanges();
                    }
                    if (!_instance.FridgeItems.Any())
                    {
                        var predefinedItems = new List<FridgeItem>
                        {
                            new FridgeItem {ProductId = 1, UserId= 1, ExpirationDate = DateTime.Now},
                            new FridgeItem {ProductId = 2, UserId = 1, ExpirationDate = DateTime.Now },
                            new FridgeItem {ProductId = 3, UserId = 1, ExpirationDate = DateTime.Now },
                            new FridgeItem {ProductId = 4, UserId = 2, ExpirationDate = DateTime.Now},
                            new FridgeItem {ProductId = 5, UserId = 2, ExpirationDate = DateTime.Now}
                        };

                        _instance.FridgeItems.AddRange(predefinedItems);
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
        public void ClearDatabase()
        {
            _instance.Users.RemoveRange(_instance.Users);
            _instance.Products.RemoveRange(_instance.Products);
            _instance.FridgeItems.RemoveRange(_instance.FridgeItems);

            _instance.SaveChanges();
            var tableNames = new List<string> { "Users", "Products", "FridgeItems" };

            foreach (var tableName in tableNames)
            {
                using (var command = _instance.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $"DELETE FROM sqlite_sequence WHERE name = '{tableName}';";
                    _instance.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _instance.Database.CloseConnection();
                }
            }
        }

    }

    public class XML
    {
        public static void ExportXml<T>(T obj, string file)
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException(nameof(file));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                ser.Serialize(fs, obj);
            }
        }
    }
}
