namespace LionHeart.Core.Dtos.Company;

public class AddCompanyDto
{
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}