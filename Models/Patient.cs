using Medical.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

public class Patient : AppUser
{
    [Required]
    [Column(TypeName = "date")]
    [Range(typeof(DateOnly), "1/1/1900", "1/1/2100", ErrorMessage = "Not a Valid Birthday")]  // Validate Age 18+ and date don't after Now (when adding & update)
    public DateOnly BirthDay { get; set; }

    public string? Address { get; set; }

    [Required]
    public Gender Gender { get; set; }

    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual List<Record> Records { get; set; } = new List<Record>();
}
