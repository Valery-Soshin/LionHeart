using LionHeart.Core.Models;
using LionHeart.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Configurations;

public class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
{
    public void Configure(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.HasQueryFilter(pu => pu.SaleStatus == SaleStatus.Available);

        builder.HasOne<Product>()
            .WithMany();
    }
}