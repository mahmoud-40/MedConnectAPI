using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

public class Record : BaseEntity
{
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("Patient")]
    public required string PatientId { get; set; }

    [ForeignKey("Provider")]
    public required string ProviderId { get; set; }

    public string? Treatments { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Provider? Provider { get; set; }
    //public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();

}
