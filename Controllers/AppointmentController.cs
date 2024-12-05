using Medical.Data.Interface;
using Medical.Data.UnitOfWorks;
using Medical.DTOs.AppointmentDTOs;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public AppointmentsController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentDTO appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = new Appointment
            {
                Date = appointmentDto.Date,
                Time = appointmentDto.Time,
                Reason = appointmentDto.Reason,
                PatientId = appointmentDto.PatientId,
                ProviderId = appointmentDto.ProviderId,
                Status = Status.Waiting
            };

            await _unit.AppointmentRepository.Add(appointment);
            await _unit.Save();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _unit.AppointmentRepository.GetById(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            var appointmentDto = new AppointmentDetailsDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                Reason = appointment.Reason,
                PatientId = appointment.PatientId,
                ProviderId = appointment.ProviderId,
                RecordId = appointment.RecordId
            };

            return Ok(appointmentDto);
        }

        [HttpPut("{id}/reschedule")]
        public async Task<IActionResult> RescheduleAppointment(int id, [FromBody] RescheduleAppointmentDTO rescheduleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = await _unit.AppointmentRepository.GetById(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            appointment.Date = rescheduleDto.NewDate;
            appointment.Time = rescheduleDto.NewTime;
            appointment.Status = Status.Resceduled;

            _unit.AppointmentRepository.Update(appointment);
            await _unit.Save();

            return Ok(new { message = "Appointment rescheduled successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await _unit.AppointmentRepository.GetById(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            _unit.AppointmentRepository.Delete(appointment); 
            await _unit.Save();

            return Ok(new { message = "Appointment canceled successfully" });
        }

    }
}
