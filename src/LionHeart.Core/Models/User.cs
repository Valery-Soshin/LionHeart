using Microsoft.AspNetCore.Identity;

namespace LionHeart.Core.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal PersonalDiscount { get; set; } = 0;

    public List<Feedback> Feedbacks { get; set; } = new();
    public List<Order> Orders { get; set; } = null!;
    public List<BasketEntry> BasketEntries { get; set; } = null!;
 }