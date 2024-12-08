using System.Reflection.Metadata;
using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Doctors;
using Medical.DTOs.Records;
using Medical.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordsController : ControllerBase
{
    UserManager<AppUser> _userManager;
    private IUnitOfWork _unit;
    private readonly IMapper mapper;

    public RecordsController(UserManager<AppUser> userManager, IUnitOfWork unit, IMapper mapper)
    {
        _userManager = userManager;
        _unit = unit;
        this.mapper = mapper;
    }

    [SwaggerOperation(
        Summary = "Add record",
        Description = "Add record to patient, Requires Admin Role\n\n" +
            "Example: `/api/records`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddRecord(AddRecordDTO _addRecordDto)
    {
        if (_addRecordDto == null)
            return BadRequest(new { message = "Invalid record data" });
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (User.Identity?.Name == null)
            return Unauthorized();

        Provider? provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (provider == null)
            return NotFound(new { message = "Provider not found" });


        Record _record = mapper.Map<Record>(_addRecordDto);

        await _unit.RecordRepository.Add(_record);
        await _unit.NotificationRepository.Add(provider.Id, $"New medical record added for patient {_record.PatientId}");
        await _unit.NotificationRepository.Add(_record.PatientId, "New medical record added \n\n" +
                                                            $"In {provider.Name} at {DateTime.UtcNow}" +
                                                            $"Your record ID is {_record.Id}");
        await _unit.Save();

        return Ok( new { message = "Record added successfully" });
    }
}
