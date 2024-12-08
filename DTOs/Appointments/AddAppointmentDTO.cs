using System;
using System.ComponentModel.DataAnnotations;
using Medical.Utils;

namespace Medical.DTOs.Appointments;

public class AddAppointmentDTO
{
    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    [Range(0, 24)]
    public int Time { get; set; }

    public Reason Reason { get; set; } = Reason.Checkup;
}
