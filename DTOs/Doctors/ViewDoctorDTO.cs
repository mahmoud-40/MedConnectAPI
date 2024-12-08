using System;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Doctors;

public class ViewDoctorDTO
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Title { get; set; }
    public string? ProviderName { get; set; }
    public DateOnly HireDate { get; set; }
    public int YearExperience { get; set; }
}

