using Medical.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medical.Models;

public class Appointment : BaseEntity
{
    [Column(TypeName = "date")]
    [Required]
    public DateOnly Date { get; set; }

    [Column(TypeName = "time")]
    [Required]
    public TimeOnly Time { get; set; }

    public Status Status { get; set; } = Status.Waiting;

    public Reason Reason { get; set; }

    public required string PatientId { get; set; }
    public required string ProviderId { get; set; }
    public int RecordId { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Provider? Provider { get; set; }
    public virtual Record? Record { get; set; }

    public virtual List<Notification> Notification { get; set; } = new List<Notification>();
}
