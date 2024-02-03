namespace LionHeart.Core.Models;

public class OrderDetail
{
    public string Id { get; set; } = null!;
    public string OrderId { get; set; } = null!;
    public string ProductDetailId { get; set; } = null!;
    public ProductDetail ProductDetail { get; set; } = null!;
}