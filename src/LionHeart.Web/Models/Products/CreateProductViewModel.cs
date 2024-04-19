namespace LionHeart.Web.Models.Products;

public class CreateProductViewModel
{
    public string Name { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string BrandId { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public int Quantity { get; set; }
    public IFormFile Image { get; set; } = null!;
}