using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account;
public class ResetPasswordDTO
{
    [Required(ErrorMessage = "Password required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Password required")]
    [Compare("Password", ErrorMessage = "password not match")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Invalid Email address")]
    [StringLength(256, ErrorMessage = "max len 256")]
    public string? Email { get; set; }

    public string? Token { get; set; }
}
