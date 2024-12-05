using System.Reflection.Metadata;
using Medical.Data.Interface;
using Medical.DTOs.Patients;
using Medical.DTOs.Records;
using Medical.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        UserManager<AppUser> _userManager;
        private IUnitOfWork _unit;

        public RecordsController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            _userManager = userManager;
            _unit = unit;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord(AddRecordDTO _addRecordDto)
        {
            if (ModelState.IsValid)
            {
                Record _record = new Record
                {
                    PatientId = _addRecordDto.PatientId,
                    Treatments = _addRecordDto.Treatments,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Appointments = new List<Appointment>
                    {
                        new Appointment
                        {
                            PatientId = _addRecordDto.PatientId,
                            ProviderId = _addRecordDto.ProviderId,
                            Status = _addRecordDto.Status,
                            Reason = _addRecordDto.Reason,
                            Date = DateOnly.FromDateTime(DateTime.UtcNow),
                            Time = TimeOnly.FromDateTime(DateTime.UtcNow)
                        }
                    }
                };

                await _unit.RecordRepository.Add(_record);

                await _unit.Save();

                return Ok( new { message = "Record added successfully" });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{patient_id}/medical-records/{record_id}")]
        public async Task<IActionResult> GetRecord(string patient_id, int record_id)
        {
            if (ModelState.IsValid)
            {
                var record = await _unit.RecordRepository.GetById(record_id);

                if (record == null || record.PatientId != patient_id)
                {
                    return NotFound(new { message = "Record not found" });
                }

                var displayRecord = new DisplayRecord
                {
                    PatientName = record.Patient.Name,
                    ProviderName = record.Appointments[0].Provider.Name,
                    Treatments = record.Treatments,
                    Time = record.Appointments[0].Time,
                    Date = record.Appointments[0].Date,
                    Status = record.Appointments[0].Status,
                    Reason = record.Appointments[0].Reason
                };

                return Ok(displayRecord);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{patient_id}/medical-records/{record_id}")]
        public async Task<IActionResult> UpdateRecord(UpdateRecordDTO _updateRecordDto)
        {
            if (ModelState.IsValid)
            {
                var record = await _unit.RecordRepository.GetById(_updateRecordDto.RecordId);

                if (record == null || record.PatientId != _updateRecordDto.PatientId)
                {
                    return NotFound(new { message = "Record not found" });
                }

                record.Treatments = _updateRecordDto.Treatments;
                record.Appointments[0].Status = _updateRecordDto.Status;
                record.Appointments[0].Reason = _updateRecordDto.Reason;
                record.UpdatedAt = DateTime.UtcNow;

                await _unit.RecordRepository.Update(record);
                await _unit.Save();

                return Ok(new { message = "Record updated successfully" });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
