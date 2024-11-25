using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

public class Doctor : BaseEntity
{
    [Required]
    [StringLength(256)]
    public string? FullName { get; set; }

    [StringLength(256)]
    public string? Title { get; set; }

    [Column(TypeName = "date")]
    [Required]
    public DateOnly HireDate { get; set; }

    public int YearExperience { get; set; }

    [ForeignKey("Provider")]
    public required string ProviderId { get; set; }

    public virtual Provider? Provider { get; set; }
}
