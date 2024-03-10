using System.ComponentModel.DataAnnotations;

namespace LionHeart.Web.Models.Profile;

public class EditUserViewModel
{
    public string Id { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
    public decimal PersonalDiscount { get; set; }

    [DataType(DataType.Password)]
    public string? CurrentPassword { get; set; }

    [Compare("ConfirmPassword")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}