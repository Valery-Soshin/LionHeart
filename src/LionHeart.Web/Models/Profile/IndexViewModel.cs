namespace LionHeart.Web.Models.Profile;

public class IndexViewModel
{
    public string Id { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public decimal PersonalDiscount { get; set; }
}