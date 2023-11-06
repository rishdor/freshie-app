using freshie_DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace freshie_webAPI
{
    public class FreshieDbContext : DbContext
    {
        public FreshieDbContext(DbContextOptions <FreshieDbContext> options) : base(options)
        {
        }

        public DbSet<Model.User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
    }
}
