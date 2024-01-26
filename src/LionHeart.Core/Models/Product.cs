namespace LionHeart.Core.Models;

public class Product
{
    public string Id { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public User Supplier { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
}