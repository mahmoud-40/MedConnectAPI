using Medical.Utils;

namespace Medical.DTOs.Providers
{
    public class AddPatientToProviderDTO
    {
        public string ProviderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly BirthDay { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
    }
}
