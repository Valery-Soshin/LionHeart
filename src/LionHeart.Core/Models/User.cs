using Microsoft.AspNetCore.Identity;

namespace LionHeart.Core.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal PersonalDiscount { get; set; } = 0;
    public new string Email
    {
        get => base.Email!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            base.Email = value;
        }
    }

    public List<Feedback> Feedbacks { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    public List<BasketEntry> BasketEntries { get; set; } = [];
 }