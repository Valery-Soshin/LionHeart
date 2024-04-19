using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Brand;

public class ShowBrandViewModel
{
    public string Name { get; set; } = null!;
    public PagedResponse<Product> Page { get; set; } = null!;
}