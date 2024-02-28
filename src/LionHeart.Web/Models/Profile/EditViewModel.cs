using System.ComponentModel.DataAnnotations;

namespace LionHeart.Web.Models.Profile;

public class EditViewModel
{
    public string Id { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;

    [Phone]
    [Display(Name ="\"Phone\"")]
    public string? PhoneNumber { get; set; } = null!;

    [DataType(DataType.Password)]
    public string? CurrentPassword { get; set; } = null!;

    [Compare("ConfirmPassword")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "\"Пароль повторно\"")]
    public string? ConfirmPassword { get; set; } = null!;
}