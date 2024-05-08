using System.ComponentModel.DataAnnotations;

namespace LionHeart.Web.Models.Auth;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

    [Required]
    public string Captcha { get; set; } = null!;
}