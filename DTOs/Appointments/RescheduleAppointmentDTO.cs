using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.AppointmentDTOs;

public class RescheduleAppointmentDTO
{
    [Required]
    public DateOnly NewDate { get; set; }

    [Required]
    [Range(0, 24)]
    public int NewTime { get; set; }
}
