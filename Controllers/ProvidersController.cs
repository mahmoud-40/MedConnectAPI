using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Doctors;
using Medical.DTOs.Providers;
using Medical.DTOs.Records;
using Medical.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProvidersController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unit;
    private readonly IMapper mapper;

    public ProvidersController(UserManager<AppUser> userManager, IUnitOfWork unit, IMapper mapper)
    {
        _userManager = userManager;
        _unit = unit;
        this.mapper = mapper;
    }

    #region  Get Providers
    [SwaggerOperation(
        Summary = "Get all providers",
        Description = "Get all providers, Requires Admin Role\n\n" +
            "Example: `/api/providers`"
    )]
    [ProducesResponseType(typeof(List<ViewProviderDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetProviders()
    {
        List<Provider> _providers = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().ToList();
        List<ViewProviderDTO> _providersDto = mapper.Map<List<ViewProviderDTO>>(_providers);

        return Ok(_providersDto);
    }

    [SwaggerOperation(
        Summary = "Get provider by id",
        Description = "Get provider by id, Requires Admin Role\n\n" +
            "Example: `/api/providers/1`"
    )]
    [ProducesResponseType(typeof(ViewProviderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProviderById(string id)
    {
        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        ViewProviderDTO _displayProviderDto = mapper.Map<ViewProviderDTO>(_provider);

        return Ok(_displayProviderDto);
    }

    [SwaggerOperation(
        Summary = "Get Provider Profile",
        Description = "Get Provider Profile who log in, Requires Provider Role\n\n" +
            "Example: `/api/providers/profile`"
    )]
    [ProducesResponseType(typeof(ViewProviderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProviderProfile()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        ViewProviderDTO _displayProviderDto = mapper.Map<ViewProviderDTO>(_provider);

        return Ok(_displayProviderDto);
    }

    #endregion

    #region Update Provider
    [SwaggerOperation(
        Summary = "Update provider data",
        Description = "Update provider data, Requires Provider Role\n\n" +
            "Example: `/api/providers/1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateProviderDTO _providerDto)
    {
        if (_providerDto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();
        if (User.Identity?.Name == null)
            return Unauthorized();

        Provider? provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        mapper.Map(_providerDto, provider);

        await _unit.ProviderRepository.Update(provider);
        await _unit.NotificationRepository.Add(provider.Id, "Profile updated successfully");
        await _unit.Save();

        return Ok(new { message = "Provider updated successfully" });
    }

    #endregion

    #region Get Doctors By Provider
    [SwaggerOperation(
        Summary = "Get all doctors of provider",
        Description = "Get all doctors of provider\n\n" +
            "Example: `/api/providers/1/doctors`"
    )]
    [ProducesResponseType(typeof(List<ViewDoctorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}/doctors")]
    public async Task<IActionResult> GetProviderDoctors(string id)
    {
        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        List<ViewDoctorDTO> _doctorsDto = mapper.Map<List<ViewDoctorDTO>>(_provider.Doctors);

        return Ok(_doctorsDto);
    }

    [SwaggerOperation(
        Summary = "Get all doctors of provider",
        Description = "Get all doctors of provider who log in, Requires Provider Role\n\n" +
            "Example: `/api/providers/doctors`"
    )]
    [ProducesResponseType(typeof(List<ViewDoctorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize (Roles = "Provider")]
    [HttpGet("doctors")]
    public async Task<IActionResult> GetMyProviderDoctor()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        List<ViewDoctorDTO> _doctorsDto = mapper.Map<List<ViewDoctorDTO>>(_provider.Doctors);

        return Ok(_doctorsDto);
    }
    #endregion

    #region Add Doctor To Provider
    [SwaggerOperation(
        Summary = "Add doctor to provider",
        Description = "Add doctor to provider, Requires Admin Role\n\n" +
            "Example: `/api/providers/1/doctors`"
    )]
    [ProducesResponseType(typeof(AddDoctorDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/doctors")]
    public async Task<IActionResult> AddDoctorToProvider(string id, AddDoctorDTO _addDoctorToProviderDto)
    {
        if (_addDoctorToProviderDto == null)
            return BadRequest(new { message = "Doctor data is required" });
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid doctor data" });

        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        Doctor doctor = mapper.Map<Doctor>(_addDoctorToProviderDto);
        doctor.ProviderId = _provider.Id;

        _provider.Doctors.Add(doctor);
        await _unit.DoctorRepository.Add(doctor);
        await _unit.NotificationRepository.Add(_provider.Id, $"Doctor {doctor.FullName} has been added");
        await _unit.Save();

        ViewDoctorDTO view = mapper.Map<ViewDoctorDTO>(doctor);
        return CreatedAtAction(nameof(GetProviderDoctors), new { id = _provider.Id }, view);
    }

    [SwaggerOperation(
        Summary = "Add doctor to provider",
        Description = "Add doctor to provider whose log in, Requires Provider Role\n\n" +
            "Example: `/api/providers/doctors`"
    )]
    [ProducesResponseType(typeof(AddDoctorDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpPost("doctors")]
    public async Task<IActionResult> AddDoctor(AddDoctorDTO _addDoctorToProviderDto)
    {
        if (_addDoctorToProviderDto == null)
            return BadRequest(new { message = "Doctor data is required" });
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid doctor data" });
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        return await AddDoctorToProvider(_provider.Id, _addDoctorToProviderDto);
    }
    #endregion

    #region  Update Doctor In Provider
    [SwaggerOperation(
        summary: "Edit doctor data in provider",
        description: "Edit doctor data in provider who log in, Requires Provider Role\n\n" +
            "Example: `/api/providers/doctors/1`"
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpPut("doctors/{doctorId}")]
    public async Task<IActionResult> UpdateDoctor(int doctorId, UpdateDoctorDTO doctorDTO)
    {
        if (doctorDTO == null)
            return BadRequest(new { message = "Doctor data is required" });
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid doctor data" });
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        Doctor? doctor = _provider.Doctors.SingleOrDefault(d => d.Id == doctorId);
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        mapper.Map(doctorDTO, doctor);
        await _unit.DoctorRepository.Update(doctor);
        await _unit.NotificationRepository.Add(_provider.Id, $"Doctor {doctor.FullName} data has been updated");
        await _unit.Save();

        return NoContent();
    }
    #endregion

    #region Delete Doctor From Provider
    [SwaggerOperation(
        summary: "Delete doctor from provider",
        description: "Delete doctor from provider who log in, Requires Provider Role\n\n" +
            "Example: `/api/providers/doctors/1`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpDelete("doctors/{doctorId}")]
    public async Task<IActionResult> DeleteDoctor(int doctorId)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        Doctor? doctor = _provider.Doctors.SingleOrDefault(d => d.Id == doctorId);
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        await _unit.DoctorRepository.Delete(doctor);
        await _unit.NotificationRepository.Add(_provider.Id, $"Doctor {doctor.FullName} has been deleted");
        await _unit.Save();

        return Ok(new { message = "Doctor deleted successfully" });
    }
    #endregion

    #region Scheduling
    // [Authorize(Roles = "Admin")]
    // [HttpGet("{id}/schedule")]
    // public async Task<IActionResult> GetProviderSchedule(string id)
    // {
    //     Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
    //     if (_provider == null)
    //         return NotFound(new { message = "Provider not found" });

    //     var _providerAppointments = await _unit.AppointmentRepository.GetAppointmentsByProviderId(_provider.Id);

    //     List<DisplayProviderSceduleDTO> _providerScheduleDto = new List<DisplayProviderSceduleDTO>();

    //     foreach (var appointment in _providerAppointments)
    //     {
    //         var patientId = appointment.PatientId;
    //         DisplayProviderSceduleDTO _displayProviderSceduleDto = new DisplayProviderSceduleDTO
    //         {

    //             PatientId = patientId,
    //             PatientName = _unit.PatientRepository.GetById(patientId).Result?.Name,
    //             Status = appointment.Status,
    //             Reason = appointment.Reason,
    //             Date = appointment.Date,
    //             Time = appointment.Time,
    //         };

    //         _providerScheduleDto.Add(_displayProviderSceduleDto);
    //     }

    //     return Ok(_providerScheduleDto);
    // }

    // [Authorize (Roles = "Provider")]
    // [HttpGet("schedule")]
    // public async Task<IActionResult> GetProviderSchedule()
    // {
    //     if (User.Identity?.Name == null)
    //         return Unauthorized();
        
    //     Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
    //     if (_provider == null)
    //         return NotFound(new { message = "Provider not found" });

    //     var _providerAppointments = await _unit.AppointmentRepository.GetAppointmentsByProviderId(_provider.Id);

    //     List<DisplayProviderSceduleDTO> _providerScheduleDto = new List<DisplayProviderSceduleDTO>();

    //     foreach (var appointment in _providerAppointments)
    //     {
    //         var patientId = appointment.PatientId;
    //         DisplayProviderSceduleDTO _displayProviderSceduleDto = new DisplayProviderSceduleDTO
    //         {

    //             PatientId = patientId,
    //             PatientName = _unit.PatientRepository.GetById(patientId).Result?.Name,
    //             Status = appointment.Status,
    //             Reason = appointment.Reason,
    //             Date = appointment.Date,
    //             Time = appointment.Time,
    //         };

    //         _providerScheduleDto.Add(_displayProviderSceduleDto);
    //     }

    //     return Ok(_providerScheduleDto);
    // }
    #endregion

    #region Get Records By Provider
    [SwaggerOperation(
        summary: "Get Records of Provider",
        description: "Get Records of Provider, Requires Admin Role\n\n" +
            "Example: `/api/providers/1/medical-records`"
    )]
    [ProducesResponseType(typeof(List<ViewRecordByProviderDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}/medical-records")]
    public async Task<IActionResult> GetProviderRecords(string id)
    {
        Provider? provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        List<Record> records = await _unit.RecordRepository.GetRecordsByProviderId(provider.Id);
        List<ViewRecordByProviderDTO> view = mapper.Map<List<ViewRecordByProviderDTO>>(records);

        return Ok(view);
    }

    [SwaggerOperation(
        summary: "Get Records of Provider",
        description: "Get Records of Provider who log in, Requires Provider Role\n\n" +
            "Example: `/api/providers/medical-records`"
    )]
    [ProducesResponseType(typeof(List<ViewRecordByProviderDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Provider")]
    [HttpGet("medical-records")]
    public async Task<IActionResult> GetMyProviderRecords()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (provider == null)
            return NotFound(new { message = "Provider not found" });

        List<Record> records = await _unit.RecordRepository.GetRecordsByProviderId(provider.Id);
        List<ViewRecordByProviderDTO> view = mapper.Map<List<ViewRecordByProviderDTO>>(records);

        return Ok(view);
    }
    #endregion
}
