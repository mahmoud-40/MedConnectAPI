using AutoMapper;
using Medical.DTOs.Patients;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IValidator validator;
    private readonly IMapper mapper;

    public PatientsController(UserManager<AppUser> userManager, IValidator validator, IMapper mapper)
    {
        this.userManager = userManager;
        this.validator = validator;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {   
        List<Patient> patients = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().ToList();

        List<ViewPatientDTO> patientsDTO = mapper.Map<List<ViewPatientDTO>>(patients);
        
        return Ok(patientsDTO);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        Patient? patient = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().Where(e => e.Id == id).SingleOrDefault();
        if (patient == null)
            return NotFound();

        ViewPatientDTO patientDTO = mapper.Map<ViewPatientDTO>(patient);

        return Ok(patientDTO);
    }

    [Authorize (Roles = "Patient")]
    [HttpPut("profile")]
    public IActionResult EditProfile(UpdatePatientDTO patientDTO)
    {
        if (patientDTO == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();
        if (User.Identity?.Name == null)
            return Unauthorized();

        Patient? patient = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().SingleOrDefault(e => e.UserName == User.Identity.Name);
        if (patient == null)
            return NotFound();

        if (!validator.IsBirthdayValid(patientDTO.BirthDay, out Exception ex))
            return BadRequest(ex.Message);

        mapper.Map(patientDTO, patient);

        IdentityResult res = userManager.UpdateAsync(patient).Result;
        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}/medical-records")]
    public IActionResult GetMedicalRecordsGeneral(int id) { throw new NotImplementedException(); }

    [Authorize(Roles = "Patient")]
    [HttpGet("medical-records")]
    public IActionResult GetMedicalRecords() { throw new NotImplementedException(); }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}/appointments")]
    public IActionResult GetAppointmentsGeneral(int id) { throw new NotImplementedException(); }

    [Authorize(Roles = "Patient")]
    [HttpGet("appointments")]
    public IActionResult GetAppointments() { throw new NotImplementedException(); }
}
