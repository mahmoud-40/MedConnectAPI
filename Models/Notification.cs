using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

// triger to get remenders
public class Notification : BaseEntity
{
    public bool IsSeen { get; set; } = false;
    public string? Message { get; set; }

    [ForeignKey("Appointment")]
    public int AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
