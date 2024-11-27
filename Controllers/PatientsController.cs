using Medical.DTOs.Patients;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IConverter converter;
    private readonly IValidator validator;

    public PatientsController(UserManager<AppUser> userManager, IConverter converter, IValidator validator)
    {
        this.userManager = userManager;
        this.converter = converter;
        this.validator = validator;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Patient> patients = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().ToList();
        List<ViewPatientDTO> patientsDTO = new List<ViewPatientDTO>();

        foreach (Patient patient in patients)
            patientsDTO.Add(converter.ToPatientDTO(patient));

        return Ok(patientsDTO);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        Patient? patient = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().Where(e => e.Id == id).SingleOrDefault();
        if (patient == null)
            return NotFound();

        ViewPatientDTO patientDTO = converter.ToPatientDTO(patient);

        return Ok(patientDTO);
    }

    [HttpPut("{profile}")]
    public IActionResult EditProfile(UpdatePatientDTO patientDTO)
    {
        if (patientDTO == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();

        #region GetUser
        Patient? patient = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().Where(e => e.Id == patientDTO.Id).SingleOrDefault();
        if (patient == null)
            return NotFound();
        #endregion

        if (!validator.IsBirthdayValid(patientDTO.BirthDay, out Exception ex))
            return BadRequest(ex.Message);

        patient.Name = patientDTO.Name;
        patient.Email = patientDTO.Email;
        patient.BirthDay = patientDTO.BirthDay;
        patient.Address = patientDTO.Address;
        patient.Gender = patientDTO.Gender;
        patient.PhoneNumber = patientDTO.PhoneNumber;

        IdentityResult res = userManager.UpdateAsync(patient).Result;
        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return NoContent();
    }

    [HttpGet("{id}/medical-records")]
    public IActionResult GetMedicalRecordsGeneral(int id) { throw new NotImplementedException(); }

    [HttpGet("medical-records")]
    public IActionResult GetMedicalRecords() { throw new NotImplementedException(); }

    [HttpGet("{id}/appointments")]
    public IActionResult GetAppointmentsGeneral(int id) { throw new NotImplementedException(); }

    [HttpGet("appointments")]
    public IActionResult GetAppointments() { throw new NotImplementedException(); }
}
