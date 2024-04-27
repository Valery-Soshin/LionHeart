namespace LionHeart.Core.Models;

public class Order
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = [];
}