using System;
using Medical.DTOs.Appointments;
using Medical.DTOs.Notifications;

namespace Medical.DTOs.Patients;

public class ProfilePatientDTO : ViewPatientDTO
{
    public string? UserName { get; set; }

    //List of Upcoming Appointments
    public List<ViewAppointmentDTO> UpcomingAppointments { get; set; } = new List<ViewAppointmentDTO>(); 

    //List of Unread Notifications
    public List<ViewNotificationDTO> UnreadNotifications { get; set; } = new List<ViewNotificationDTO>();
}
