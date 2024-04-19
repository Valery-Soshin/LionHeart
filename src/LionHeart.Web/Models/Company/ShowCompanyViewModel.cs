using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Company;

public class ShowCompanyViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public PagedResponse<Product> Page { get; set; } = null!;
}