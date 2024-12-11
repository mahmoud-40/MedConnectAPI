using System.ComponentModel.DataAnnotations;
using Medical.Utils;

namespace Medical.DTOs.Providers;

public class UpdateProviderDTO
{
    [StringLength(256, ErrorMessage = "max len 256")]
    public string? Name { get; set; }

    [StringLength(256, ErrorMessage = "max len 256")]
    [EmailAddress(ErrorMessage = "Invalid Email address")]
    public string? Email { get; set; }

    [StringLength(256, ErrorMessage = "max len 256")]
    public string? PhoneNumber { get; set; }
    public string? bio { get; set; }
    public Shift? Shift { get; set; }
    [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
    public float? Rate { get; set; }
}
