using LionHeart.Core.Enums;

namespace LionHeart.Core.Dtos.ProductUnit;

public class AddProductUnitDto
{
    public string ProductId { get; set; } = null!;
    public SaleStatus SaleStatus { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}