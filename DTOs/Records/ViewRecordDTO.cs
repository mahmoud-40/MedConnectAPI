using Medical.DTOs.AppointmentDTOs;
using Medical.Utils;

namespace Medical.DTOs.Records;
public class ViewRecordDTO
{
    public int Id { get; set; }
    public string? Treatments { get; set; }
    public TimeSpan LastUpdate { get; set; }
    public TimeSpan Since { get; set; }

    public List<AppointmentDetailsDTO> Appointments { get; set; } = new List<AppointmentDetailsDTO>();
}
