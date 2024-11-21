using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

public class Record
{
    public int Id { get; set; }

    [ForeignKey("Patient")]
    public string PatientId { get; set; }

    public string Treatments { get; set; }

    public virtual Patient Patient { get; set; }
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();

}
