namespace LionHeart.Core.Models;

public abstract class MarkedProduct
{
    public string Id { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product Product { get; set; } = null!;
}