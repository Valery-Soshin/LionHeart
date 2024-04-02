namespace LionHeart.Core.Models;

public class Category
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? ParentCategoryId { get; set; }
    public List<Category> SubCategories { get; set; } = [];
}