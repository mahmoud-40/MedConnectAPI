using Medical.Utils;
using System.ComponentModel.DataAnnotations;
namespace Medical.Models;

// Privider == Clinic
public class Provider : AppUser 
{
    public string? bio { get; set; }

    [Required]
    public Shift Shift { get; set; } // shift clinc

    public float Rate { get; set; }

    public virtual List<Doctor> Doctors { get; set; } = new List<Doctor>();
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();

}
