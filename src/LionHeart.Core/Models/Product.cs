namespace LionHeart.Core.Models;

public class Product
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public string CompanyId { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public Image Image { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public List<ProductUnit> Units { get; set; } = [];
    public List<Feedback> Feedbacks { get; set; } = [];
}