using Medical.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medical.Models;

public class Appointment : BaseEntity
{
    [Column(TypeName = "date")]
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    [Range(0, 24)]
    public int Time { get; set; }

    public Status Status { get; set; } = Status.Waiting;

    public Reason Reason { get; set; } //كشف او اعاده

    public required string PatientId { get; set; }
    // public required string ProviderId { get; set; } //TODO: Make Relation with doctor
    public required int DoctorId { get; set; }
    //public int RecordId { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
    //public virtual Record? Record { get; set; }
}
