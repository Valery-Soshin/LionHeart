using Microsoft.AspNetCore.Http;

namespace LionHeart.Core.Dtos.Product;

public class UpdateProductDto
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public IFormFile? Image { get; set; } = null!;
}