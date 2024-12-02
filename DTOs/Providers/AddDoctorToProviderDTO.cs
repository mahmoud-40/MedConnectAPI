namespace Medical.DTOs.ProvidersDTOs;

public class AddDoctorToProviderDTO
{
    public required string FullName { get; set; }
    public string? Title { get; set; }
    public DateOnly HireDate { get; set; }
    public int YearExperience { get; set; }
}
