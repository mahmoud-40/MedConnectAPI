using Medical.Utils;

namespace Medical.Models;

public class Patient : AppUser
{
    public int Age { get; set; }
    public string? Address { get; set; }
    public Gender Gender { get; set; }
    
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual List<Record> Records { get; set; } = new List<Record>();
}
