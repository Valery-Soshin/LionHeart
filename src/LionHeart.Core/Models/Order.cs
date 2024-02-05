namespace LionHeart.Core.Models;

public class Order
{
    public string Id { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product? Product { get; set; }
    public string UserId { get; set; } = null!;
    public User? User { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = [];
}