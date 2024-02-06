using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Configurations;

public class MarkedProductConfiguration : IEntityTypeConfiguration<MarkedProduct>
{
    public void Configure(EntityTypeBuilder<MarkedProduct> builder)
    {
        builder.Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        //builder.HasAlternateKey(p => new { p.UserId, p.ProductId });

    
    }
}