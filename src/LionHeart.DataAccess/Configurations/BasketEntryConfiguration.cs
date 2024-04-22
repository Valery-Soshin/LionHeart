using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LionHeart.DataAccess.Configurations;

public class BasketEntryConfiguration : IEntityTypeConfiguration<BasketEntry>
{
    public void Configure(EntityTypeBuilder<BasketEntry> builder)
    {
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.HasAlternateKey(b => new { b.UserId, b.ProductId });
    }
}