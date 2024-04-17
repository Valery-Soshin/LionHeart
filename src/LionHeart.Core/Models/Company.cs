namespace LionHeart.Core.Models;

public class Company
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}