using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Configurations;

public class OrderItemDetailConfiguration : IEntityTypeConfiguration<OrderItemDetail>
{
    public void Configure(EntityTypeBuilder<OrderItemDetail> builder)
    {
        builder.Property(d => d.Id).ValueGeneratedOnAdd();
    }
}