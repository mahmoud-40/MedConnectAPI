using Medical.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Providers;

public class DisplayProviderSceduleDTO
{
    public string? PatientId { get; set; }
    public string? PatientName { get; set; }
    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public Status Status { get; set; } = Status.Waiting;

    public Reason Reason { get; set; }
}
