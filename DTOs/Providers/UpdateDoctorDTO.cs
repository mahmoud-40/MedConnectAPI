using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Providers
{
    public class UpdateDoctorDTO
    {
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Title { get; set; }
        public DateOnly HireDate { get; set; }
        public int YearExperience { get; set; }
    }
}
