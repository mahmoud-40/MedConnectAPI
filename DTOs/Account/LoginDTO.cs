using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account;

public class LoginDTO
{
    [Required(ErrorMessage = "Username required")]
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password required")]
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string Password { get; set; }
}
