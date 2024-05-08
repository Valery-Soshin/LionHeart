using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.HasAlternateKey(f => new { f.ProductId, f.UserId });
    }
}