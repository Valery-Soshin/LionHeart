namespace LionHeart.Core.Dtos.BasketEntry;

public class AddBasketEntryDto
{
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}