namespace LionHeart.Core.Models;

public class BasketEntry
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public DateTimeOffset CreatedAt { get; set; }
}