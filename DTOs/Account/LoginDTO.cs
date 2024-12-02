using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account
{
    public class LoginDTO
    {

      
            [Required(ErrorMessage = "Username required")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password required")]
            [MinLength(6, ErrorMessage = "min len 6")]
            [MaxLength(10, ErrorMessage = "max len 10")]
            public string Password { get; set; }

           
    }
}
