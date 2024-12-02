using Medical.Models;
using Medical.Utils;

namespace Medical.DTOs.ProvidersDTOs
{
    public class DisplayProviderDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? bio { get; set; }
        public Shift Shift { get; set; }
        public float Rate { get; set; }

        public List<AddDoctorToProviderDTO> Doctors { get; set; } = new List<AddDoctorToProviderDTO>();
    }
}
