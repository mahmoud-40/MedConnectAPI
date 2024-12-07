using Medical.DTOs.Patients;

namespace Medical.DTOs.Records;

public class ViewRecordByProviderDTO : ViewRecordDTO
{
    public ViewPatientDTO? Patient { get; set; }
}
