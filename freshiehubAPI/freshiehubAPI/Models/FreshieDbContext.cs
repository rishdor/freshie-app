using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace freshiehubAPI.Models;

public partial class FreshieDbContext : DbContext
{
    public FreshieDbContext()
    {
    }

    public FreshieDbContext(DbContextOptions<FreshieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Cuisine> Cuisines { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<FridgeItem> FridgeItems { get; set; }

    public virtual DbSet<GroceriesHistory> GroceriesHistories { get; set; }

    public virtual DbSet<GroceriesList> GroceriesLists { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Shelf> Shelves { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__D54EE9B4F636E546");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
        });

        modelBuilder.Entity<Cuisine>(entity =>
        {
            entity.ToTable("Cuisine");

            entity.Property(e => e.CuisineId).HasColumnName("cuisine_id");
            entity.Property(e => e.CuisineName)
                .HasMaxLength(50)
                .HasColumnName("cuisine_name");
        });

        modelBuilder.Entity<Diet>(entity =>
        {
            entity.HasKey(e => e.DietId).HasName("PK__Diet__1FFA6C695891FC62");

            entity.ToTable("Diet");

            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.DietName)
                .HasMaxLength(500)
                .HasColumnName("diet_name");
        });

        modelBuilder.Entity<FridgeItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FridgeIt__3213E83F31062E6B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            //entity.HasOne(d => d.Product).WithMany(p => p.FridgeItems)
            //    .HasForeignKey(d => d.ProductId)
            //    .HasConstraintName("FK__FridgeIte__produ__02084FDA");

            //entity.HasOne(d => d.User).WithMany(p => p.FridgeItems)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__FridgeIte__user___02FC7413");
        });

        modelBuilder.Entity<GroceriesHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grocerie__3213E83F2110E7CF");

            entity.ToTable("GroceriesHistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Purchased).HasColumnName("purchased");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            //entity.HasOne(d => d.Product).WithMany(p => p.GroceriesHistories)
            //    .HasForeignKey(d => d.ProductId)
            //    .HasConstraintName("FK__Groceries__produ__03F0984C");

            //entity.HasOne(d => d.User).WithMany(p => p.GroceriesHistories)
            //    .HasForeignKey(d => d.UserId)
            //    .HasConstraintName("FK__Groceries__user___04E4BC85");
        });

        modelBuilder.Entity<GroceriesList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grocerie__3213E83FA8C9DFEA");

            entity.ToTable("GroceriesList");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            //entity.HasOne(d => d.Product).WithMany(p => p.GroceriesLists)
            //    .HasForeignKey(d => d.ProductId)
            //    .HasConstraintName("FK__Groceries__produ__17036CC0");

            //entity.HasOne(d => d.User).WithMany(p => p.GroceriesLists)
            //    .HasForeignKey(d => d.UserId)
            //    .HasConstraintName("FK__Groceries__user___17F790F9");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__47027DF532A6BE04");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasColumnName("icon");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");
            entity.Property(e => e.ShelfId).HasColumnName("shelf_id");

            //entity.HasOne(d => d.Category).WithMany(p => p.Products)
            //    .HasForeignKey(d => d.CategoryId)
            //    .HasConstraintName("FK__Products__catego__05D8E0BE");

            //entity.HasOne(d => d.Shelf).WithMany(p => p.Products)
            //    .HasForeignKey(d => d.ShelfId)
            //    .HasConstraintName("FK__Products__shelf___06CD04F7");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK_Recipies");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CuisineId).HasColumnName("cuisine_id");
            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.Ingredients)
                .HasMaxLength(3200)
                .HasColumnName("ingredients");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(150)
                .HasColumnName("recipe_name");
            entity.Property(e => e.Url)
                .HasMaxLength(550)
                .HasColumnName("URL");

            //entity.HasOne(d => d.Course).WithMany(p => p.Recipes)
            //    .HasForeignKey(d => d.CourseId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Recipes_Course");

            //entity.HasOne(d => d.Cuisine).WithMany(p => p.Recipes)
            //    .HasForeignKey(d => d.CuisineId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Recipes_Cuisine");

            //entity.HasOne(d => d.Diet).WithMany(p => p.Recipes)
            //    .HasForeignKey(d => d.DietId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Recipes_Diet");
        });

        modelBuilder.Entity<Shelf>(entity =>
        {
            entity.HasKey(e => e.ShelfId).HasName("PK__Shelves__E33A5B7C07E7D398");

            entity.Property(e => e.ShelfId).HasColumnName("shelf_id");
            entity.Property(e => e.ShelfName)
                .HasMaxLength(100)
                .HasColumnName("shelf_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F11DAC13A");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164BD2D3FDE").IsUnique();

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
