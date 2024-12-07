using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Doctors;

public class UpdateDoctorDTO : AddDoctorDTO
{
    public new DateOnly? HireDate { get; set; } = null;
}
