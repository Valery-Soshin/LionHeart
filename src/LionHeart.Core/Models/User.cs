using Microsoft.AspNetCore.Identity;

namespace LionHeart.Core.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal PersonalDiscount { get; set; } = 0;
}