using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Full Name required")]

        public string FullName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //public string Role { get; set; } // Admin, Patient, or Provider

        [Required(ErrorMessage = "Birthday Is Required")]
        [Range(typeof(DateOnly), "1/1/1900", "1/1/2100", ErrorMessage = "Not a Valid Birthday")] // Validate Age 18+ and date don't after Now (when update & add)
        public DateOnly BirthDay { get; set; }

        public string? Address { get; set; }

        [Required]

        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]


        public Gender Gender { get; set; }
        public string? bio { get; set; }

        [Required]
        public Shift Shift { get; set; } // shift clinc

        public float? Rate { get; set; }

        [Required]

        public AppUserType UserType { get; set; }
             

    }
}
