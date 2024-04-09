using LionHeart.Core.Enums;

namespace LionHeart.Core.Dtos.ProductUnit;

public class UpdateProductUnitDto
{
    public string Id { get; set; } = null!;
    public SaleStatus SaleStatus { get; set; }
}