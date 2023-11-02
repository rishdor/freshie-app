﻿using Microsoft.EntityFrameworkCore;
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
                            new Product { Name = "Apple" },
                            new Product { Name = "Pasta" },
                            new Product { Name = "Milk" },
                            new Product { Name = "Eggs" },
                            new Product { Name = "Cheese"},
                            new Product { Name = "Carrot"}
                        };

                        _instance.Products.AddRange(predefinedProducts);
                        _instance.SaveChanges();
                    }
                    if (!_instance.Users.Any())
                    {
                        var predefinedUser = new List<User>
                        {
                            new User { Name = "admin", Email = "1", Password= "1"}
                        };

                        _instance.Users.AddRange(predefinedUser);
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
        }

    }

    public class XML
    {
        //private static MyDbContext _context = MyDbContext.Instance;
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
        //public static void ImportXml<T>(string file) where T : class, new()
        //{
        //    var serializer = new XmlSerializer(typeof(List<T>));

        //    List<T> entities;
        //    using (var reader = new StreamReader(file))
        //    {
        //        entities = (List<T>)serializer.Deserialize(reader);
        //    }

        //    var newEntities = entities.Select(e => {
        //        var newEntity = new T();
        //        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
        //        foreach (var property in properties)
        //        {
        //            property.SetValue(newEntity, property.GetValue(e));
        //        }
        //        return newEntity;
        //    }).ToList();

        //    _context.Set<T>().AddRange(newEntities);
        //    _context.SaveChanges();
        //}
    }
}