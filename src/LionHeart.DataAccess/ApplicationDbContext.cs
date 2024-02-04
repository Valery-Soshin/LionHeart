using LionHeart.Core.Models;
using LionHeart.DataAccess.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<MarkedProduct> MarkedProducts { get; set; }
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

        var category = new Category()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Одежда"
        };
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            CategoryId = category.Id,
            Name = "Футболка",
            Price = 1250,
            Quantity = 1,
            Description = "Красивая и удобная футболка",
            Specifications = "Размер - XXL"
        };

        builder.Entity<Category>().HasData(category);
        builder.Entity<Product>().HasData(product);

        base.OnModelCreating(builder);
    }
}