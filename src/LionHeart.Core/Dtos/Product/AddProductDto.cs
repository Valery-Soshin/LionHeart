using LionHeart.Core.Models;
using Microsoft.AspNetCore.Http;

namespace LionHeart.Core.Dtos.Product;

public class AddProductDto
{
    public string CategoryId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public IFormFile Image { get; set; } = null!;
}