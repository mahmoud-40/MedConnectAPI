using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

// triger to get remenders
public class Notification
{
    public int Id { get; set; }

    public bool IsSeen { get; set; }

    public DateTime Time { get; set; }


    [ForeignKey("Appointment")]
    public int AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }
}

 