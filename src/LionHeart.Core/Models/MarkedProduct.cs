using LionHeart.Core.Enums;

namespace LionHeart.Core.Models;

public class MarkedProduct
{
    public string Id { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public Mark Mark { get; set; }
}