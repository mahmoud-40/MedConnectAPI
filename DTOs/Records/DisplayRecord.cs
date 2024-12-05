using Medical.Utils;

namespace Medical.DTOs.Records
{
    public class DisplayRecord
    {
        public string? PatientName { get; set; }
        public string? ProviderName { get; set; }
        public string? Treatments { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public Status Status { get; set; }
        public Reason Reason { get; set; }
    }
}
