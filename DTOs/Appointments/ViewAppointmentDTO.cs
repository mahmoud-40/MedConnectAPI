using Medical.DTOs.Records;
using Medical.Utils;

namespace Medical.DTOs.Appointments;

public class ViewAppointmentDTO
{
    public int Id { get; set; }
    public string? PatientName { get; set; }
    public string? DoctorName { get; set; }
    public string? ProviderName { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public Status Status { get; set; }
    public Reason Reason { get; set; }

    public ViewRecordDTO? DisplayRecord { get; set; }
}
