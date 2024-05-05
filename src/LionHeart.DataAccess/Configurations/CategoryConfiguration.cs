using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasAlternateKey(c => c.Name);

        builder.HasMany(c => c.SubCategories)
            .WithOne()
            .HasForeignKey(c => c.ParentCategoryId);
    }
}