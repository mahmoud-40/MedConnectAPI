using Medical.DTOs.Providers;

namespace Medical.DTOs.Records;

public class ViewRecordByPatientDTO : ViewRecordDTO
{
    public ViewProviderDTO? Provider { get; set; }
}
