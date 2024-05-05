using LionHeart.Core.Models;
using LionHeart.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Configurations;

public class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
{
    public void Configure(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.HasQueryFilter(u => u.SaleStatus == SaleStatus.Available);
    }
}