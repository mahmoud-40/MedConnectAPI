using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Account;

public class RegisterDTO
{
    [StringLength(256, ErrorMessage = "max len 256")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "UserName required")]
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Invalid Email address")]
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password required")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Birthday Is Required")]
    [Range(typeof(DateOnly), "1/1/1900", "1/1/2100", ErrorMessage = "Not a Valid Birthday")] // Validate Age 18+ and date don't after Now (when update & add)
    public DateOnly BirthDay { get; set; }

    public string? Address { get; set; }


    [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
    public Gender Gender { get; set; }
    public string? bio { get; set; }

    public Shift Shift { get; set; }
    [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
    public float? Rate { get; set; }


    [Required]
    public AppUserType UserType { get; set; }
}
