using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.Models;

public class Patient : AppUser
{
    [Required]
    public int Age { get; set; }

    public string? Address { get; set; }

    [Required]
    public Gender Gender { get; set; }
    
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual List<Record> Records { get; set; } = new List<Record>();
}
