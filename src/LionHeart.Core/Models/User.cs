namespace LionHeart.Core.Models;

public class User
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public decimal PersonalDiscount { get; set; } = 0;
}