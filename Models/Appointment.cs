using Medical.Utils;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medical.Models;

public class Appointment
{
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateOnly Date { get; set; }

    [Column(TypeName = "time")]
    public TimeOnly Time { get; set; }
    public Status Status { get; set; }
    public Reason Reason { get; set; }


    public string PatientId { get; set; }

    public string ProviderId { get; set; }
    
    public int RecordId { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Provider? Provider { get; set; }
    public virtual Record Record { get; set; }

    public virtual List<Notification> Notification { get; set; } = new List<Notification>();
}
