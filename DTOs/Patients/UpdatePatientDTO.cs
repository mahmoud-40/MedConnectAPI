using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Patients;

public class UpdatePatientDTO
{
    [StringLength(256, ErrorMessage = "max len 256")]
    public string? Name { get; set; }

    [StringLength(256, ErrorMessage = "max len 256")]
    [EmailAddress(ErrorMessage = "Invalid Email address")]
    public string? Email { get; set; }

    public DateOnly? BirthDay { get; set; }

    public string? Address { get; set; }

    public Gender? Gender { get; set; }

    [StringLength(256, ErrorMessage = "max len 256")]
    public string? PhoneNumber { get; set; }
}
