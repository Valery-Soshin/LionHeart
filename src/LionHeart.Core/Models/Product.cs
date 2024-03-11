namespace LionHeart.Core.Models;

public class Product
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public ImageModel Image { get; set; } = null!;

    public List<ProductUnit> Units { get; set; } = [];
    public List<Feedback> Feedbacks { get; set; } = [];
}