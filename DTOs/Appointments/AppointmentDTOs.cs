using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.AppointmentDTOs;

public class BookAppointmentDTO
{
    public DateOnly Date { get; set; }

    [Range(0, int.MaxValue)]
    public int Time { get; set; }
    public Reason Reason { get; set; } = Reason.Routine_check_up;
    public required int DoctorId { get; set; }
}
