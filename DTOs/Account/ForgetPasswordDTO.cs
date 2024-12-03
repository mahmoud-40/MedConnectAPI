using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account;
public class ForgetPasswordDTO
{
    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Invalid Email address")]
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string Email { get; set; }
}
