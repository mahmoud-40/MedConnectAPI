namespace Medical.DTOs.AppointmentDTOs;

public class RescheduleAppointmentDTO
{
    public DateOnly NewDate { get; set; }
    public int NewTime { get; set; }
}
