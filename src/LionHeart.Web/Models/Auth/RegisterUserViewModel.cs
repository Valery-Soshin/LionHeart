using System.ComponentModel.DataAnnotations;

namespace LionHeart.Web.Models.Auth;

public class RegisterUserViewModel
{
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RemeberMe { get; set; }

    [Required]
    public string Captcha { get; set; } = null!;
}