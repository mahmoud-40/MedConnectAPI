using System.ComponentModel.DataAnnotations;
using Medical.Utils;

namespace Medical.DTOs.Providers
{
    public class UpdateProviderDTO
    {
        [Required]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string bio { get; set; }
        public Shift Shift { get; set; }
        public float Rate { get; set; }

        public List<UpdateDoctorDTO> Doctors { get; set; } = new List<UpdateDoctorDTO>();
    }
}
