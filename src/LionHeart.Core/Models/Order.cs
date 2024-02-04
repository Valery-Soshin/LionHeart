namespace LionHeart.Core.Models;

public class Order
{
    public string Id { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public User Customer { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = [];
}