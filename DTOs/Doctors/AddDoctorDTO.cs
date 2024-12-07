using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Doctors;

public class AddDoctorDTO
{
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string FullName { get; set; }
    public string? Title { get; set; }

    [Required]
    public DateOnly HireDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public int YearExperience { get; set; } = 0;
}
