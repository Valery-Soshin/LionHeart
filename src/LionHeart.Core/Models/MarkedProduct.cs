namespace LionHeart.Core.Models;

public abstract class MarkedProduct
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product Information { get; set; } = null!;
}