using Medical.Utils;

namespace Medical.DTOs.Patients;

public class ViewPatientDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public Gender Gender { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public TimeSpan MemberSince { get; set; }

    //List of Upcoming Appointments
    //List of Unread Notifications
}
