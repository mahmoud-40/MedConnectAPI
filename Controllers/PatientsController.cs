using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Appointments;
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

    #region Get Patients
    [SwaggerOperation(
        summary: "Get all patients",
        description: "Get all patients\n\n" +
            "Example: `/api/patients`"
    )]
    [ProducesResponseType(typeof(List<ViewPatientDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {   
        List<Patient> patients = (await userManager.GetUsersInRoleAsync("Patient")).OfType<Patient>().ToList();

        List<ViewPatientDTO> patientsDTO = mapper.Map<List<ViewPatientDTO>>(patients);
        
        return Ok(patientsDTO);
    }

    [SwaggerOperation(
        summary: "Get patient by id",
        description: "Get patient by id\n\n" +
            "Example: `/api/patients/1`"
    )]
    [ProducesResponseType(typeof(ViewPatientDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        Patient? patient = (await userManager.GetUsersInRoleAsync("Patient")).OfType<Patient>().Where(e => e.Id == id).SingleOrDefault();
        if (patient == null)
            return NotFound();

        ViewPatientDTO patientDTO = mapper.Map<ViewPatientDTO>(patient);

        return Ok(patientDTO);
    }

    [SwaggerOperation(
        summary: "Get patient profile",
        description: "Get patient profile, Requires Patient Role\n\n" +
            "Example: `/api/patients/profile`"
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
    #endregion

    #region Edit Profile
    [SwaggerOperation(
        summary: "Edit patient profile",
        description: "Edit patient profile, Requires Patient Role\n\n" +
            "Example: `/api/patients/profile`"
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

        if (patientDTO.BirthDay != null)
            if (!validator.IsBirthdayValid(patientDTO.BirthDay.Value, out Exception ex))
                return BadRequest(ex.Message);
        else
            patientDTO.BirthDay = patient.BirthDay;

        mapper.Map(patientDTO, patient);

        IdentityResult res = await userManager.UpdateAsync(patient);
        if (!res.Succeeded)
            return BadRequest(res.Errors);

        await unit.NotificationRepository.Add(patient.Id, "Your profile has been updated.");
        await unit.Save();
        return NoContent();
    }
    #endregion

    #region Manage Medical Records
    [SwaggerOperation(
        summary: "Get medical records",
        description: "Get medical records of a patient, Requires Patient Role\n\n" +
            "Query parameters: doctorId or providerId\n\n" +
            "You can only use one of the query parameters\n\n" +
            "Example: `/api/patients/medical-records?doctorId=1`"
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

        Patient? patient = await userManager.FindByNameAsync(User.Identity.Name) as Patient;
        if (patient == null)
            return NotFound(new { message = "Patient not found" });

        return await GetMedicalRecordsGerneral(patient.Id, doctorId, providerId);
    }

    [SwaggerOperation(
        summary: "Get medical records",
        description: "Get medical records of a patient, Requires Admin or Patient Role\n\n" +
            "If Admin Role, you can get medical records of any patient\n\n" +
            "If Patient Role, you can get medical records of yourself\n\n" +
            "Query parameters: doctorId or providerId\n\n" +
            "You can only use one of the query parameters\n\n" +
            "Example: `/api/patients/1/medical-records?doctorId=1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin, Patient")]
    [HttpGet("{id}/medical-records")]
    public async Task<IActionResult> GetMedicalRecordsGerneral([FromRoute] string id, [FromQuery] int? doctorId, [FromQuery] string? providerId)
    {
        if (doctorId != null && providerId != null)
            return BadRequest(new { message = "Invalid query parameters only one should use" });

        Patient? patient = (await userManager.GetUsersInRoleAsync("Patient")).OfType<Patient>().SingleOrDefault(e => e.Id == id);
        if (patient == null)
            return NotFound(new { message = "Patient not found" });

        if (User.IsInRole("Patient") && User.Identity?.Name != patient.UserName)
            return Unauthorized(new { message = "Unauthorized" });

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

    [SwaggerOperation(
        summary: "Add medical record",
        description: "Add medical record to a patient in current Provider, Requires Provider Role\n\n" +
            "Example: `/api/patients/1/medical-records`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpPost("{patient_id}/medical-records")]
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
        await unit.NotificationRepository.Add(patient_id, "New medical record added to your profile\n\n" +
                                                            $"In {provider.Name} at {DateTime.UtcNow}" +
                                                            $"Your record ID is {record.Id}");
        await unit.Save();

        return Ok(new { message = "Record added successfully" });
    }

    [SwaggerOperation(
        summary: "Edit medical record",
        description: "Edit medical record of a patient in current Provider, Requires Provider Role\n\n" +
            "Example: `/api/patients/1/medical-records/1`"
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
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
        record.UpdatedAt = DateTime.UtcNow;
        await unit.RecordRepository.Update(record);
        await unit.NotificationRepository.Add(patient_id, $"Your medical record in {provider.Name} has been updated" +
                                                            $" at {DateTime.UtcNow}" +
                                                            $"Your record ID is {record.Id}");
        await unit.Save();

        return NoContent();
    }

    [SwaggerOperation(
        summary: "Delete medical record",
        description: "Delete medical record of a patient in current Provider, Requires Provider Role\n\n" +
            "Example: `/api/patients/1/medical-records/1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
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
        await unit.NotificationRepository.Add(patient_id, $"Your medical record in {provider.Name} has been deleted" +
                                                            $" at {DateTime.UtcNow}" +
                                                            $"Your record ID is {record.Id}");
        await unit.Save();

        return Ok(new { message = "Record deleted successfully" });
    }
    #endregion

    #region Manage Appointments
    [SwaggerOperation(
        summary: "Get appointments",
        description: "Get appointments of a patient by id, Requires Admin or Patient Role\n\n" +
            "If Admin Role, you can get appointments of any patient\n\n" +
            "If Patient Role, you can get appointments of yourself\n\n" +
            "Example: `/api/patients/1/appointments`"
    )]
    [ProducesResponseType(typeof(List<ViewAppointmentDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin, Patient")]
    [HttpGet("{id}/appointments")]
    public async Task<IActionResult> GetAppointments(string id)
    {
        Patient? patient = (await userManager.GetUsersInRoleAsync("Patient")).OfType<Patient>().SingleOrDefault(e => e.Id == id);
        if (patient == null)
            return NotFound(new { message = "Patient not found" });
        
        if (User.IsInRole("Patient") && User.Identity?.Name != patient.UserName)
            return Unauthorized(new { message = "Unauthorized" });

        List<Appointment> appointments = await unit.AppointmentRepository.GetByPatientId(patient.Id) as List<Appointment>;
        List<ViewAppointmentDTO> appointmentsDTO = mapper.Map<List<ViewAppointmentDTO>>(appointments);
        return Ok(appointmentsDTO);
    }

    [SwaggerOperation(
        summary: "Get appointments",
        description: "Get appointments of a patient who log in, Requires Patient Role\n\n" +
            "Example: `/api/patients/appointments`"
    )]
    [ProducesResponseType(typeof(List<ViewAppointmentDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Patient")]
    [HttpGet("/api/appointments")]
    public async Task<IActionResult> GetMyAppointments()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        Patient? patient = await userManager.FindByNameAsync(User.Identity.Name) as Patient;
        if (patient == null)
            return NotFound(new { message = "Patient not found" });

        List<Appointment> appointments = await unit.AppointmentRepository.GetByPatientId(patient.Id) as List<Appointment>;
        List<ViewAppointmentDTO> appointmentsDTO = mapper.Map<List<ViewAppointmentDTO>>(appointments);
        return Ok(appointmentsDTO);
    }
    #endregion
}