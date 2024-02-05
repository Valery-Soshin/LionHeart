namespace LionHeart.Core.Models;

public class OrderDetail
{
    public string Id { get; set; } = null!;
    public string OrderId { get; set; } = null!;
    public string ProductUnitId { get; set; } = null!;
    public ProductUnit? ProductUnit { get; set; }
}