using Medical.Utils;

namespace Medical.DTOs.Records;

public class UpdateRecordDTO : UpdateRecordByProviderDTO
{
    public required string PatientId { get; set; }
    public required string ProviderId { get; set; }
}
