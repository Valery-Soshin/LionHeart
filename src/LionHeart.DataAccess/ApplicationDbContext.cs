using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace LionHeart.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<MarkedProduct> MarkedProducts { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Entity<ProductDetail>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Entity<Category>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Entity<Feedback>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Entity<MarkedProduct>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Entity<Order>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Entity<OrderDetail>()
            .Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}