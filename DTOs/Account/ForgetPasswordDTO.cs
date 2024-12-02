using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
