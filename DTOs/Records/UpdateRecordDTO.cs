using Medical.Utils;

namespace Medical.DTOs.Records
{
    public class UpdateRecordDTO
    {
        public string? PatientId { get; set; }
        public int RecordId { get; set; }
        public string? PatientName { get; set; }
        public string? Treatments { get; set; }
        public Status Status { get; set; }
        public Reason Reason { get; set; }
    }
}
