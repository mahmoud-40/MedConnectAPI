using Medical.Utils;

namespace Medical.DTOs.AppointmentDTOs;

public class AppointmentDetailsDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int Time { get; set; }
    public Status Status { get; set; }
    public Reason Reason { get; set; }
    public string? PatientId { get; set; }
    public int? DoctorId { get; set; }
    public string? ProviderId { get; set; }
}
