namespace LionHeart.Core.Models;

public class FavoriteProduct
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Product Product { get; set; } = null!;
}