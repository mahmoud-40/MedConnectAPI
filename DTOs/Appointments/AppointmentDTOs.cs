using Medical.Utils;

namespace Medical.DTOs.AppointmentDTOs;

public class BookAppointmentDTO
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public Reason Reason { get; set; }
    public string PatientId { get; set; }
    public string ProviderId { get; set; }
}
