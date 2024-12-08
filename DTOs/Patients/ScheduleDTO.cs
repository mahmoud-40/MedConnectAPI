using System;
using Medical.DTOs.Appointments;

namespace Medical.DTOs.Patients;

public class ScheduleDTO
{
    public DateOnly Day { get; set; }
    public List<ViewAppointmentDTO> Appointments { get; set; } = new();
}
