using Medical.Utils;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Patients
{
    public class AddPatientDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly BirthDay { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
    }
}
