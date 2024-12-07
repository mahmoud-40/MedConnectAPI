using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Patients;
using Medical.DTOs.Records;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IUnitOfWork unit;
    private readonly IValidator validator;
    private readonly IMapper mapper;

    public PatientsController(UserManager<AppUser> userManager, IUnitOfWork unit, IValidator validator, IMapper mapper)
    {
        this.userManager = userManager;
        this.unit = unit;
        this.validator = validator;
        this.mapper = mapper;
    }

    [SwaggerOperation(
        summary: "Get all patients",
        description: "Get all patients\n" +
            "Example: /api/patients"
    )]
    [ProducesResponseType(typeof(List<ViewPatientDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public IActionResult GetAll()
    {   
        List<Patient> patients = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().ToList();

        List<ViewPatientDTO> patientsDTO = mapper.Map<List<ViewPatientDTO>>(patients);
        
        return Ok(patientsDTO);
    }

    [SwaggerOperation(
        summary: "Get patient by id",
        description: "Get patient by id\n" +
            "Example: /api/patients/1"
    )]
    [ProducesResponseType(typeof(ViewPatientDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        Patient? patient = userManager.GetUsersInRoleAsync("Patient").Result.OfType<Patient>().Where(e => e.Id == id).SingleOrDefault();
        if (patient == null)
            return NotFound();

        ViewPatientDTO patientDTO = mapper.Map<ViewPatientDTO>(patient);

        return Ok(patientDTO);
    }

    [SwaggerOperation(
        summary: "Get patient profile",
        description: "Get patient profile, Requires Patient Role\n" +
            "Example: /api/patients/profile"
    )]
    [ProducesResponseType(typeof(ViewPatientDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Patient")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        Patient? patient = await userManager.FindByNameAsync(User.Identity.Name) as Patient;
        if (patient == null)
            return NotFound();

        ProfilePatientDTO patientDTO = mapper.Map<ProfilePatientDTO>(patient);

        return Ok(patientDTO);
    }

    [SwaggerOperation(
        summary: "Edit patient profile",
        description: "Edit patient profile, Requires Patient Role\n" +
            "Example: /api/patients/profile"
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize (Roles = "Patient")]
    [HttpPut("profile")]
    public async Task<IActionResult> EditProfile(UpdatePatientDTO patientDTO)
    {
        if (patientDTO == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();
        if (User.Identity?.Name == null)
            return Unauthorized();

        Patient? patient = await userManager.FindByNameAsync(User.Identity.Name) as Patient;
        if (patient == null)
            return NotFound();

        if (!validator.IsBirthdayValid(patientDTO.BirthDay, out Exception ex))
            return BadRequest(ex.Message);

        mapper.Map(patientDTO, patient);

        IdentityResult res = userManager.UpdateAsync(patient).Result;
        if (!res.Succeeded)
            return BadRequest(res.Errors);

        await unit.NotificationRepository.Add(patient.Id, "Your profile has been updated.");
        return NoContent();
    }

    [SwaggerOperation(
        summary: "Get medical records",
        description: "Get medical records of a patient, Requires Patient Role\n" +
            "Query parameters: doctorId or providerId\n" +
            "You can only use one of the query parameters\n" +
            "Example: /api/patients/medical-records?doctorId=1"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Patient")]
    [HttpGet("medical-records")]
    public async Task<IActionResult> GetMedicalRecords([FromQuery] int? doctorId, [FromQuery] string? providerId)
    {
        if (doctorId != null && providerId != null)
            return BadRequest(new { message = "Invalid query parameters only one should use" });

        if (User.Identity?.Name == null)
            return Unauthorized();

        Patient? patient = userManager.FindByNameAsync(User.Identity.Name).Result as Patient;
        if (patient == null)
            return NotFound(new { message = "Patient not found" });

        return await GetMedicalRecordsGerneral(patient.Id, doctorId, providerId);

        // Doctor? doctor = await unit.DoctorRepository.GetById(doctorId ?? -1);
        // if (doctorId != null && doctor == null)
        //     return NotFound(new { message = "Doctor not found" });
        // Provider? provider = null;
        // if (providerId == null)
        //     provider = await userManager.FindByIdAsync(doctor?.ProviderId ?? "-1") as Provider;
        // else
        //     provider = await userManager.FindByIdAsync(providerId) as Provider;

        // if (providerId != null && provider == null)
        //     return NotFound(new { message = "Provider not found" });

        // if (provider is not null)
        // {
        //     Record? record = await unit.RecordRepository.Get(patient.Id, provider.Id);
        //     if (record is null)
        //         return NotFound(new { message = "Record not found" });
        //     DisplayRecord recordDTO = mapper.Map<DisplayRecord>(record);
        //     return Ok(recordDTO);
        // }
        // else
        // {
        //     List<Record> records = (await unit.RecordRepository.GetByPatientId(patient.Id)).ToList();
        //     List<DisplayRecord> recordsDTO = mapper.Map<List<DisplayRecord>>(records);
        //     return Ok(recordsDTO);
        // }
    }

    [SwaggerOperation(
        summary: "Get medical records",
        description: "Get medical records of a patient, Requires Admin Role\n" +
            "Query parameters: doctorId or providerId\n" +
            "You can only use one of the query parameters\n" +
            "Example: /api/patients/1/medical-records?doctorId=1"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}/medical-records")]
    public async Task<IActionResult> GetMedicalRecordsGerneral([FromRoute] string id, [FromQuery] int? doctorId, [FromQuery] string? providerId)
    {
        if (doctorId != null && providerId != null)
            return BadRequest(new { message = "Invalid query parameters only one should use" });

        Patient? patient = (await userManager.GetUsersInRoleAsync("Patient")).OfType<Patient>().SingleOrDefault(e => e.Id == id);
        if (patient == null)
            return NotFound(new { message = "Patient not found" });

        Doctor? doctor = await unit.DoctorRepository.GetById(doctorId ?? -1);
        if (doctorId != null && doctor == null)
            return NotFound(new { message = "Doctor not found" });

        Provider? provider = null;
        if (providerId == null)
            provider = await userManager.FindByIdAsync(doctor?.ProviderId ?? "-1") as Provider;
        else
            provider = await userManager.FindByIdAsync(providerId) as Provider;

        if (providerId != null && provider == null)
            return NotFound(new { message = "Provider not found" });

        if (provider is not null)
        {
            Record? record = await unit.RecordRepository.Get(patient.Id, provider.Id);
            if (record is null)
                return NotFound(new { message = "Record not found" });
            ViewRecordByPatientDTO recordDTO = mapper.Map<ViewRecordByPatientDTO>(record);
            return Ok(recordDTO);
        }
        else
        {
            List<Record> records = (await unit.RecordRepository.GetByPatientId(patient.Id)).ToList();
            List<ViewRecordByPatientDTO> recordsDTO = mapper.Map<List<ViewRecordByPatientDTO>>(records);
            return Ok(recordsDTO);
        }
    }

    [Authorize(Roles = "Provider")]
    [HttpPost("{id}/medical-records")]
    public async Task<IActionResult> AddRecord(string patient_id, AddRecordByProviderDTO recordDTO)
    {
        if (recordDTO == null)
            return BadRequest(new { message = "Invalid record data" });
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (User.Identity?.Name == null)
            return Unauthorized();

        Provider? provider = await userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        Record record = mapper.Map<Record>(recordDTO);
        record.ProviderId = provider.Id;
        record.PatientId = patient_id;

        await unit.RecordRepository.Add(record);
        await unit.Save();

        return Ok(new { message = "Record added successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpPut("{patient_id}/medical-records/{record_id}")]
    public async Task<IActionResult> EditRecord(string patient_id, int record_id, UpdateRecordByProviderDTO recordDTO)
    {
        if (recordDTO == null)
            return BadRequest(new { message = "Invalid record data" });
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (User.Identity?.Name == null)
            return Unauthorized();

        Provider? provider = await userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        Record? record = await unit.RecordRepository.GetById(record_id);
        if (record == null || record.PatientId != patient_id)
            return NotFound(new { message = "Record not found" });

        mapper.Map(recordDTO, record);
        await unit.RecordRepository.Update(record);
        await unit.Save();

        return NoContent();
    }

    [Authorize (Roles = "Provider")]
    [HttpDelete("{patient_id}/medical-records/{record_id}")]
    public async Task<IActionResult> DeleteRecord(string patient_id, int record_id)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        Provider? provider = await userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        Record? record = await unit.RecordRepository.GetById(record_id);
        if (record == null || record.PatientId != patient_id)
            return NotFound(new { message = "Record not found" });

        await unit.RecordRepository.Delete(record);
        await unit.Save();

        return Ok(new { message = "Record deleted successfully" });
    }

    //[Authorize(Roles = "Admin")]
    //[HttpGet("{id}/appointments")]
    //public IActionResult GetAppointmentsGeneral(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //[Authorize(Roles = "Patient")]
    //[HttpGet("appointments")]
    //public IActionResult GetAppointments() { throw new NotImplementedException(); }
}