using Medical.Utils;

namespace Medical.DTOs.AppointmentDTOs;

public class AppointmentDetailsDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public Status Status { get; set; }
    public Reason Reason { get; set; }
    public string PatientId { get; set; }
    public string ProviderId { get; set; }
    public int RecordId { get; set; }
}
