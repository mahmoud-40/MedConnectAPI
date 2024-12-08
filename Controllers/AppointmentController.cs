using AutoMapper;
using Medical.Data.Interface;
using Medical.Data.UnitOfWorks;
using Medical.DTOs.AppointmentDTOs;
using Medical.DTOs.Appointments;
using Medical.DTOs.Doctors;
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
public class AppointmentsController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    private readonly IValidator validator;

    public AppointmentsController(IUnitOfWork unit, UserManager<AppUser> userManager, IMapper mapper, IValidator validator)
    {
        _unit = unit;
        this.userManager = userManager;
        this.mapper = mapper;
        this.validator = validator;
    }

    [SwaggerOperation(
        Summary = "Book appointment",
        Description = "Book appointment by patient Requires Patient role\n\n" +
                      "Status: Waiting\n\n" +
                      "Example: POST `/api/appointments`"
    )]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<IActionResult> BookAppointment([FromBody] AddAppointmentDTO appointmentDto)
    {
        if (appointmentDto == null)
            return BadRequest(new { message = "Invalid appointment data" });
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Patient? patient = await userManager.FindByNameAsync(User.Identity.Name) as Patient;
        if (patient == null)
            return NotFound(new { message = "Patient not found" });

        Appointment appointment = mapper.Map<Appointment>(appointmentDto);
        appointment.PatientId = patient.Id;
        appointment.Patient = patient;
        appointment.Doctor = await _unit.DoctorRepository.GetById(appointmentDto.DoctorId);

        if (!validator.IsAppointmentValid(appointment, out Exception exception))
            return BadRequest(new { message = exception.Message });

        await _unit.AppointmentRepository.Add(appointment);
        await _unit.NotificationRepository.Add(patient.Id, "Appointment booked successfully waiting for confirmation");
        await _unit.NotificationRepository.Add(appointment.Doctor!.ProviderId, $"New appointment({appointment.Id}) has been booked by {patient.Name}" +
                                                                                $"Date: {appointment.Date} Time: {appointment.Time}" +
                                                                                $"Doctor: {appointment.Doctor.FullName}\n" +
                                                                                "Please confirm it as soon as possible");
        await _unit.Save();

        ViewAppointmentDTO view = mapper.Map<ViewAppointmentDTO>(appointment);
    
        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, view);
    }

    [SwaggerOperation(
        Summary = "Get appointment by id",
        Description = "Get appointment by id Requires Login\n\n" +
                      "Example: GET `/api/appointments/1`"
    )]
    [ProducesResponseType(typeof(ViewAppointmentDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        var appointment = await _unit.AppointmentRepository.GetById(id);

        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });

        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });

        if (User.IsInRole("Patient"))
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        else if (User.IsInRole("Provider"))
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });

        ViewAppointmentDTO view = mapper.Map<ViewAppointmentDTO>(appointment);

        return Ok(view);
    }

    [SwaggerOperation(
        Summary = "Get Patient of appointment",
        Description = "Get Patient of an appointment Requires Login\n\n" +
                      "Example: GET `/api/appointments/1/patient`"
    )]
    [ProducesResponseType(typeof(ViewPatientDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet("{id}/patient")]
    public async Task<IActionResult> GetPatientAppointments(int id)
    {
        Appointment? appointment = await _unit.AppointmentRepository.GetById(id);
        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });
        
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });
        
        if (User.IsInRole("Patient"))
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        else if (User.IsInRole("Provider"))
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        
        ViewPatientDTO view = mapper.Map<ViewPatientDTO>(appointment.Patient);

        return Ok(view);
    }

    [SwaggerOperation(
        Summary = "Get Doctor of appointment",
        Description = "Get Doctor of an appointment Requires Login\n\n" +
                      "Example: GET `/api/appointments/1/doctor`"
    )]
    [ProducesResponseType(typeof(ViewDoctorDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet("{id}/doctor")]
    public async Task<IActionResult> GetDoctorAppointments(int id)
    {
        Appointment? appointment = await _unit.AppointmentRepository.GetById(id);
        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });
        
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });
        
        if (User.IsInRole("Patient"))
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        else if (User.IsInRole("Provider"))
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        
        ViewDoctorDTO view = mapper.Map<ViewDoctorDTO>(appointment.Doctor);

        return Ok(view);
    }

    [SwaggerOperation(
        Summary = "Get Record of Appointment Patient",
        Description = "Get Record of Appointment Patient Requires Login\n\n" +
                      "Example: GET `/api/appointments/1/record`"
    )]
    [ProducesResponseType(typeof(ViewRecordDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet("{id}/record")]
    public async Task<IActionResult> GetRecordAppointments(int id)
    {
        Appointment? appointment = await _unit.AppointmentRepository.GetById(id);
        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });
        
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });
        
        if (User.IsInRole("Patient"))
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        else if (User.IsInRole("Provider"))
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to view this appointment" });
        
        Record? record = appointment.Patient!.Records.SingleOrDefault(r => r.ProviderId == appointment.Doctor!.ProviderId);
        if (record == null)
            return NotFound(new { message = "No Record yet for this Patient" });

        ViewRecordDTO view = mapper.Map<ViewRecordDTO>(record);

        return Ok(view);
    }

    [SwaggerOperation(
        Summary = "Reschedule appointment",
        Description = "Reschedule appointment by patient or provider Requires Patient or Provider Role\n\n" +
                      "If Patient -> Status.Rescheduled And Need Confirm From Provider\n\n" +
                      "If Provider -> Status.Updated And Need Confirm From Patient\n\n" +
                      "Example: PUT `/api/appointments/1/reschedule`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider, Patient")]
    [HttpPut("{id}/reschedule")]
    public async Task<IActionResult> RescheduleAppointment(int id, [FromBody] RescheduleAppointmentDTO rescheduleDto)
    {
        if (rescheduleDto == null)
            return BadRequest(new { message = "Invalid reschedule data" });
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var appointment = await _unit.AppointmentRepository.GetById(id);
        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });

        if (User.Identity?.Name == null)
            return Unauthorized();
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });
        
        if (User.IsInRole("Patient"))
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to reschedule this appointment" });
        else if (User.IsInRole("Provider"))
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to reschedule this appointment" });
        
        if (rescheduleDto.NewDate > appointment.Date.AddDays(1))
            return BadRequest(new { message = "Can't reschedule an appointment that in 1 day" });

        appointment.Date = rescheduleDto.NewDate;
        appointment.Time = rescheduleDto.NewTime;

        if (!validator.IsAppointmentValid(appointment, out Exception exception))
            return BadRequest(new { message = exception.Message });
        
        if (User.IsInRole("Patient"))
        {
            appointment.Status = Status.Rescheduled;
            await _unit.NotificationRepository.Add(appointment.Doctor!.ProviderId, $"Appointment({appointment.Id}) has been rescheduled by patient\n" +
                                                                                $"Date: {appointment.Date} Time: {appointment.Time}\n" +
                                                                                $"Doctor: {appointment.Doctor.FullName}\n" +
                                                                                "Please confirm it as soon as possible");
        }
        else if (User.IsInRole("Provider"))
        {
            appointment.Status = Status.Updated;
            await _unit.NotificationRepository.Add(appointment.PatientId, $"Appointment({appointment.Id}) has been rescheduled\n" +
                                                                            $"Date: {appointment.Date} Time: {appointment.Time}\n" +
                                                                            $"Doctor: {appointment.Doctor!.FullName}\n" +
                                                                            "Please Accept it as soon as possible");
        }

        await _unit.AppointmentRepository.Update(appointment);
        await _unit.Save();

        return Ok(new { message = "Appointment rescheduled successfully" });
    }

    [SwaggerOperation(
        summary: "Cancel appointment",
        description: "Cancel appointment by patient, Requires Patient or Provider Role\n\n" +
            "Example: `/api/appointments/1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Patient, Provider")]
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var appointment = await _unit.AppointmentRepository.GetById(id);
        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });
        
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });
        
        if (User.IsInRole("Patient"))
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to cancel this appointment" });
        else if (User.IsInRole("Provider"))
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to cancel this appointment" });
        
        appointment.Status = Status.Canceled;

        appointment.Status = Status.Canceled;
        await _unit.AppointmentRepository.Update(appointment);
        await _unit.NotificationRepository.Add(appointment.PatientId, $"Appointment({appointment.Id}) canceled successfully");
        await _unit.NotificationRepository.Add(appointment.Doctor!.ProviderId, $"Appointment({appointment.Id}) canceled");
        await _unit.Save();

        return Ok(new { message = "Appointment canceled successfully" });
    }

    [SwaggerOperation(
        Summary = "Appointment Need to Confirm",
        Description = "Get all appointments that need to be confirmed by provider or Patient Requires Provider or Patient role\n\n" +
                      "If Patient -> appointments that updated by provider\n\n" +
                      "If Provider -> appointments that waiting for confirmation or rescheduled by patient\n\n" +
                      "Example: GET `/api/appointments/toconfirm`"
    )]
    [ProducesResponseType(typeof(List<ViewAppointmentDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider, Patient")]
    [HttpGet("toconfirm")]
    public async Task<IActionResult> GetAppointmentsToConfirm()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });

        IEnumerable<Appointment> appointments = new List<Appointment>();
        if (User.IsInRole("Patient"))
            appointments = (await _unit.AppointmentRepository.GetByPatientId(user.Id)).Where(a => a.Status == Status.Updated);
        else if (User.IsInRole("Provider"))
            appointments = (await _unit.AppointmentRepository.GetByProviderId(user.Id)).Where(a => a.Status == Status.Waiting || a.Status == Status.Rescheduled);

        List<ViewAppointmentDTO> view = mapper.Map<List<ViewAppointmentDTO>>(appointments);

        return Ok(view);
    }

    [SwaggerOperation(
        Summary = "Confirm appointment",
        Description = "Confirm appointment by provider or patient Requires Provider or Patient role\n\n" +
                      "If Patient -> Confirm Updated Appointment\n\n" +
                      "If Provider -> Confirm Rescheduled or Waiting Appointment\n\n" +
                      "Example: PUT `/api/appointments/1/confirm`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider, Patient")]
    [HttpPut("{id}/confirm")]
    public async Task<IActionResult> ConfirmAppointment(int id)
    {
        var appointment = await _unit.AppointmentRepository.GetById(id);
        if (appointment == null)
            return NotFound(new { message = "Appointment not found" });

        if (User.Identity?.Name == null)
            return Unauthorized();
        
        AppUser? user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(new { message = "User not found" });

        if (User.IsInRole("Patient"))
        {
            if (appointment.PatientId != user.Id)
                return Unauthorized(new { message = "You are not authorized to manage this appointment" });
            if (appointment.Status != Status.Updated)
                return BadRequest(new { message = "This appointment is not in the updated list" });
        }
        else if (User.IsInRole("Provider"))
        {
            if (appointment.Doctor?.ProviderId != user.Id)
                return Unauthorized(new { message = "You are not authorized to manage this appointment" });
            if (appointment.Status != Status.Waiting && appointment.Status != Status.Rescheduled)
                return BadRequest(new { message = "This appointment is not in in the waiting list or Rescheduled" });
        }

        appointment.Status = Status.Confirmed;

        await _unit.AppointmentRepository.Update(appointment);
        await _unit.Save();

        if (User.IsInRole("Patient"))
            await _unit.NotificationRepository.Add(appointment.Doctor!.ProviderId, $"Appointment({appointment.Id}) has been confirmed by patient\n" +
                                                                                $"Date: {appointment.Date} Time: {appointment.Time}\n" +
                                                                                $"Doctor: {appointment.Doctor?.FullName}");
        else if (User.IsInRole("Provider"))
            await _unit.NotificationRepository.Add(appointment.PatientId, $"Appointment({appointment.Id})  at ({appointment.Doctor?.Provider?.Name}) has been confirmed\n" +
                                                                            $"Date: {appointment.Date} Time: {appointment.Time}\n" +
                                                                            $"Doctor: {appointment.Doctor?.FullName}");
                                                                      
        //Sent Reminder to patient before 1 day of appointment
        await _unit.NotificationRepository.Add(appointment.PatientId, $"Reminder: Your appointment({appointment.Id}) At ({appointment.Doctor?.Provider?.Name}) is tomorrow" +
                                                $"Date: {appointment.Date} Time: {appointment.Time}" +
                                                $"Doctor: {appointment.Doctor?.FullName}", appointment.Date.ToDateTime(TimeOnly.MinValue).AddDays(-1).AddHours(10));
        await _unit.Save();

        return Ok(new { message = "Appointment confirmed successfully" });
    }

}


// Patient Can Reschedule appointment then Provider Confirm it -> Status.Reschedule
// Provider Can Reschedule appointment then Patient Confirm it -> Status.Updated
