using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Doctors;
using Medical.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IUnitOfWork unit;
    private readonly IMapper mapper;

    public DoctorsController(UserManager<AppUser> userManager, IUnitOfWork unit, IMapper mapper)
    {
        this.userManager = userManager;
        this.unit = unit;
        this.mapper = mapper;
    }

    [SwaggerOperation(
        summary: "Get all doctors",
        description: "Add doctor to provider, Requires Admin Role\n\n" +
            "Example: `/api/doctors`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await unit.DoctorRepository.GetAll();
        List<ViewDoctorDTO> views = mapper.Map<List<ViewDoctorDTO>>(doctors);
        return Ok(views);
    }

    [SwaggerOperation(
        summary: "Get doctor by id",
        description: "Get doctor by id, Requires Admin Role\n\n" +
            "Example: `/api/doctors/1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetDoctor(int doctorId)
    {
        Doctor? doctor = await unit.DoctorRepository.GetById(doctorId);
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        ViewDoctorDTO? view = mapper.Map<ViewDoctorDTO>(doctor);
        return Ok(view);
    }

    [SwaggerOperation(
        summary: "Edit doctor data",
        description: "Edit doctor data in provider, Requires Admin Role\n\n" +
            "Example: `/api/doctors/1`"
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin,Provider")]
    [HttpPut("{doctorId}")]
    public async Task<IActionResult> UpdateDoctor(int doctorId, UpdateDoctorDTO doctorDTO)
    {
        if (doctorDTO == null)
            return BadRequest(new { message = "Doctor data is required" });
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid doctor data" });

        Doctor? doctor = await unit.DoctorRepository.GetById(doctorId);
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        if (User.IsInRole("Provider"))
        {
            if (User.Identity?.Name == null)
                return Unauthorized(new { message = "Unauthorized" });
            Provider? provider = await userManager.FindByNameAsync(User.Identity.Name) as Provider;
            if (provider == null)
                return NotFound(new { message = "Provider not found" });

            if (doctor.ProviderId != provider.Id)
                return Unauthorized(new { message = "Unauthorized" });
        }

        if (doctorDTO.HireDate ==  null)
            doctorDTO.HireDate = doctor.HireDate;

        mapper.Map(doctorDTO, doctor);
        await unit.DoctorRepository.Update(doctor);
        await unit.NotificationRepository.Add(doctor.ProviderId, $"Doctor {doctor.FullName} data updated");
        await unit.Save();

        return NoContent();
    }

    [SwaggerOperation(
        summary: "Delete doctor",
        description: "Delete doctor, Requires Admin Role\n\n" +
            "Example: `/api/doctors/1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin,Provider")]
    [HttpDelete("doctors/{doctorId}")]
    public async Task<IActionResult> DeleteDoctor(int doctorId)
    {
        Doctor? doctor = await unit.DoctorRepository.GetById(doctorId);
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        await unit.DoctorRepository.Delete(doctor);
        await unit.NotificationRepository.Add(doctor.ProviderId, $"Doctor {doctor.FullName} deleted");
        await unit.Save();

        return Ok(new { message = "Doctor deleted successfully" });
    }

    [SwaggerOperation(
        summary: "Change doctor provider",
        description: "Change doctor provider, Requires Admin Role\n\n" +
            "Example: `/api/doctors/1/provider/2`"
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpPut("{doctorId}/provider/{providerId}")]
    public async Task<IActionResult> ChangeProvider(int doctorId, string providerId)
    {
        Doctor? doctor = await unit.DoctorRepository.GetById(doctorId);
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        Provider? provider = await unit.ProviderRepository.GetById(providerId);
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        string prevProvider = doctor.ProviderId;
        doctor.ProviderId = provider.Id;
        await unit.DoctorRepository.Update(doctor);
        await unit.NotificationRepository.Add(provider.Id, $"Doctor {doctor.FullName} assigned to you");
        await unit.NotificationRepository.Add(prevProvider, $"Doctor {doctor.FullName} removed from you");
        await unit.Save();

        return NoContent();
    }
}
