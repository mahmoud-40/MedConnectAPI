using Medical.Utils;

namespace Medical.DTOs.Records;

public class AddRecordDTO : AddRecordByProviderDTO
{
    public required string PatientId { get; set; }
    public required string ProviderId { get; set; }
}
