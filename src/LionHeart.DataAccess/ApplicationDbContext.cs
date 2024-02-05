using LionHeart.Core.Models;
using LionHeart.DataAccess.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;

namespace LionHeart.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<MarkedProduct> MarkedProducts{ get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductUnitConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new FeedbackConfiguration());
        builder.ApplyConfiguration(new MarkedProductConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderDetailConfiguration());
        builder.ApplyConfiguration(new BasketConfiguration());

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
            Quantity = 1,
            Description = "Красивая и удобная футболка",
            Specifications = "Размер - XXL"
        };

        builder.Entity<Category>().HasData(category);
        builder.Entity<Product>().HasData(product);

        for (int i = 0; i < 5; i++)
        {
            var productUnit = new ProductUnit
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product.Id,
                SaleStatus = Core.Enums.SaleStatus.Available,
                CreatedAt = DateTimeOffset.Now
            };

            builder.Entity<ProductUnit>().HasData(productUnit);
        }

        base.OnModelCreating(builder);
    }
}