namespace LionHeart.Core.Models;

public class Brand
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ImageId { get; set; } = null!;
    public Image Image { get; set; } = null!;
}