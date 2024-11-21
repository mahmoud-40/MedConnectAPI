using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

public class Doctor
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string? Title { get; set; }
    public DateOnly HireDate { get; set; }
    public int YearExperience { get; set; }

    [ForeignKey("Provider")]
    public string? ProviderId { get; set; }

    public virtual Provider Provider { get; set; }
}
