using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Brand;

public class ShowBrandViewModel
{
    public string Name { get; set; } = null!;
    public PagedResponse<Core.Models.Product> Page { get; set; } = null!;
}