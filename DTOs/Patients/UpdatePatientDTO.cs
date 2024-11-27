using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Patients;

public class UpdatePatientDTO
{
    public required string Id { get; set; } //TODO: remove and get user from current session
    [StringLength(256)]
    public required string Name { get; set; }

    [StringLength(256)]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    public required string Email { get; set; }

    [Required]
    [Range(typeof(DateOnly), "1/1/1900", "1/1/2100", ErrorMessage = "Not a Valid Birthday")] // Validate Age 18+ and date don't after Now (when update & add)
    public DateOnly BirthDay { get; set; }

    public string? Address { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [StringLength(256)]
    public string? PhoneNumber { get; set; }
}
