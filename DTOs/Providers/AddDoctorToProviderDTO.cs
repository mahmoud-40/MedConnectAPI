using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.ProvidersDTOs;

public class AddDoctorToProviderDTO
{
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string FullName { get; set; }
    public string? Title { get; set; }

    public DateOnly HireDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public int YearExperience { get; set; }
}
