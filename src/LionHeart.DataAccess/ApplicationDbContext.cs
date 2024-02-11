using LionHeart.Core.Models;
using LionHeart.DataAccess.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<BasketEntry> BasketEntries{ get; set; }
    public DbSet<FavoriteProduct> FavoriteProducts{ get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderItemDetail> OrderItemDetails { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductUnitConfiguration());
        builder.ApplyConfiguration(new BasketEntryConfiguration());
        builder.ApplyConfiguration(new FavoriteProductConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new FeedbackConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderItemConfiguration());
        builder.ApplyConfiguration(new OrderItemDetailConfiguration());

        var suppplier = new User
        {
            UserName = "admin",
            Email = "admin",
        };

        builder.Entity<User>().HasData(suppplier);

        var category = new Category()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Одежда"
        };
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = suppplier.Id,
            CategoryId = category.Id,
            Name = "Футболка",
            Price = 1250,
            Description = "Красивая и удобная футболка.",
            Specifications = "Размер - XXL"
        };
        var product2 = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = suppplier.Id,
            CategoryId = category.Id,
            Name = "Мяч",
            Price = 1250,
            Description = "Футбольный мяч, может быть использован даже во время дождя.",
            Specifications = ""
        };

        builder.Entity<Category>().HasData(category);
        builder.Entity<Product>().HasData(product);
        builder.Entity<Product>().HasData(product2);

        for (int i = 0; i < 5; i++)
        {
            var productUnit = new ProductUnit
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product.Id,
                SaleStatus = Core.Enums.SaleStatus.Available,
                CreatedAt = DateTimeOffset.Now
            };

            var productUnit2 = new ProductUnit
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product2.Id,
                SaleStatus = Core.Enums.SaleStatus.Available,
                CreatedAt = DateTimeOffset.Now
            };

            builder.Entity<ProductUnit>().HasData(productUnit);
            builder.Entity<ProductUnit>().HasData(productUnit2);
        }

        base.OnModelCreating(builder);
    }
}