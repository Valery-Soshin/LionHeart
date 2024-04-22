using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Configurations;

public class FavoriteProductConfiguration : IEntityTypeConfiguration<FavoriteProduct>
{
    public void Configure(EntityTypeBuilder<FavoriteProduct> builder)
    {
        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.HasAlternateKey(f => new { f.UserId, f.ProductId });
    }
}