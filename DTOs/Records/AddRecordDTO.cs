using Medical.Utils;

namespace Medical.DTOs.Records
{
    public class AddRecordDTO
    {
        public string PatientId { get; set; }
        public string ProviderId { get; set; }
        public string PatientName { get; set; }
        public string? Treatments { get; set; }
        public Status Status { get; set; }
        public Reason Reason { get; set; }
    }
}
