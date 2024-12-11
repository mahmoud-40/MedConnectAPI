using Medical.DTOs.Doctors;
using Medical.Utils;

namespace Medical.DTOs.Providers;

public class ViewProviderDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? bio { get; set; }
    public Shift Shift { get; set; }
    public float Rate { get; set; }

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<ViewDoctorDTO> Doctors { get; set; } = new List<ViewDoctorDTO>();
    public TimeSpan MemberSince { get; set; }
    public Uri? PhotoUri { get; set; }
}
