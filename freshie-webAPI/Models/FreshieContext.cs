using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace freshie_webAPI.Models;

public partial class FreshieContext : DbContext
{
    public FreshieContext()
    {
    }

    public FreshieContext(DbContextOptions<FreshieContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<Dietum> Dieta { get; set; }

    public virtual DbSet<FridgeItem> FridgeItems { get; set; }

    public virtual DbSet<GroceriesHistory> GroceriesHistories { get; set; }

    public virtual DbSet<GroceriesList> GroceriesLists { get; set; }

    public virtual DbSet<IndianFood> IndianFoods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDiet> ProductDiets { get; set; }

    public virtual DbSet<ProductsFromWeb> ProductsFromWebs { get; set; }

    public virtual DbSet<Shelf> Shelves { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Password=Karuzela6012;Persist Security Info=True;User ID=emilia;Initial Catalog=Freshie;Data Source=emilia.database.windows.net");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__D54EE9B4B2D11400");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Diet>(entity =>
        {
            entity.HasKey(e => e.DietId).HasName("PK__Diet__1FFA6C69A15E1F3C");

            entity.ToTable("Diet");

            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.DietName)
                .HasMaxLength(500)
                .HasColumnName("diet_name");
        });

        modelBuilder.Entity<Dietum>(entity =>
        {
            entity.HasKey(e => e.DietaId).HasName("PK__Dieta__D6A1C530B267331D");

            entity.Property(e => e.DietaId).HasColumnName("dieta_id");
            entity.Property(e => e.DietaName)
                .HasMaxLength(500)
                .HasColumnName("dieta_name");
        });

        modelBuilder.Entity<FridgeItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FridgeIt__3213E83FACDEF489");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expiration_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.FridgeItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__FridgeIte__produ__4A8310C6");

            entity.HasOne(d => d.User).WithMany(p => p.FridgeItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FridgeIte__user___4B7734FF");
        });

        modelBuilder.Entity<GroceriesHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grocerie__3213E83F40FF1DCB");

            entity.ToTable("GroceriesHistory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Purchased).HasColumnName("purchased");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.GroceriesHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Groceries__produ__58D1301D");

            entity.HasOne(d => d.User).WithMany(p => p.GroceriesHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Groceries__user___59C55456");
        });

        modelBuilder.Entity<GroceriesList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grocerie__3213E83F14489EC9");

            entity.ToTable("GroceriesList");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.GroceriesLists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Groceries__produ__55009F39");

            entity.HasOne(d => d.User).WithMany(p => p.GroceriesLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Groceries__user___55F4C372");
        });

        modelBuilder.Entity<IndianFood>(entity =>
        {
            entity.ToTable("IndianFood");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Course).HasMaxLength(50);
            entity.Property(e => e.Cuisine).HasMaxLength(50);
            entity.Property(e => e.Diet).HasMaxLength(50);
            entity.Property(e => e.Ingredients).HasMaxLength(3200);
            entity.Property(e => e.RecipeName).HasMaxLength(150);
            entity.Property(e => e.TranslatedIngredients).HasMaxLength(3250);
            entity.Property(e => e.TranslatedRecipeName).HasMaxLength(150);
            entity.Property(e => e.Url)
                .HasMaxLength(550)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__47027DF559B9BFF5");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Icon)
                .HasMaxLength(1000)
                .HasColumnName("icon");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ShelfId).HasColumnName("shelf_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__catego__00200768");

            entity.HasOne(d => d.Shelf).WithMany(p => p.Products)
                .HasForeignKey(d => d.ShelfId)
                .HasConstraintName("FK__Products__shelf___01142BA1");
        });

        modelBuilder.Entity<ProductDiet>(entity =>
        {
            entity.HasKey(e => e.ProductdietId).HasName("PK__ProductD__2C2DB12CEAA35D46");

            entity.ToTable("ProductDiet");

            entity.Property(e => e.ProductdietId).HasColumnName("productdiet_id");
            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Diet).WithMany(p => p.ProductDiets)
                .HasForeignKey(d => d.DietId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductDi__diet___04E4BC85");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDiets)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductDi__produ__03F0984C");
        });

        modelBuilder.Entity<ProductsFromWeb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductsFromWeb");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(500)
                .HasColumnName("category_name");
            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .HasColumnName("product_name");
        });

        modelBuilder.Entity<Shelf>(entity =>
        {
            entity.HasKey(e => e.ShelfId).HasName("PK__Shelves__E33A5B7C0F40E759");

            entity.Property(e => e.ShelfId).HasColumnName("shelf_id");
            entity.Property(e => e.ShelfName)
                .HasMaxLength(100)
                .HasColumnName("shelf_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F70FF945A");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61649A1D3FD0").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("salt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
