using AutoMapper;
using Medical.Data.Interface;
using Medical.Data.Repository;
using Medical.Data.UnitOfWorks;
using Medical.DTOs.Patients;
using Medical.DTOs.Providers;
using Medical.DTOs.ProvidersDTOs;
using Medical.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterProviderDTO _registerProviderDto)
    {
        if (ModelState.IsValid)
        {
            Provider _provider = new Provider
            {
                UserName = _registerProviderDto.UserName,
                Email = _registerProviderDto.Email,
                PhoneNumber = _registerProviderDto.PhoneNumber,
                bio = _registerProviderDto.bio,
                Shift = _registerProviderDto.Shift,
                Rate = _registerProviderDto.Rate
            };

            foreach (var doctor in _registerProviderDto.Doctors)
            {
                Doctor _doctor = new Doctor
                {
                    FullName = doctor.FullName,
                    Title = doctor.Title,
                    HireDate = doctor.HireDate,
                    YearExperience = doctor.YearExperience,
                    ProviderId = _provider.Id
                };

                _provider.Doctors.Add(_doctor);
                await _unit.DoctorRepository.Add(_doctor);
            }

            var result = _userManager.CreateAsync(_provider, _registerProviderDto.Password).Result;

            if (result.Succeeded)
            {
                var roleResult = _userManager.AddToRoleAsync(_provider, "Provider").Result;

                if (roleResult.Succeeded)
                {
                    await _unit.Save();
                    return Ok(new { message = "Provider registered successfully" });
                }
                else
                {
                    return BadRequest(roleResult.Errors);
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProvider(string id)
    {
        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        DisplayProviderDTO _displayProviderDto = mapper.Map<DisplayProviderDTO>(_provider);

        return Ok(_displayProviderDto);
    }

    [HttpGet("{id}/doctors")]
    public async Task<IActionResult> GetProviderDoctors(string id)
    {
        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        List<AddDoctorToProviderDTO> _doctorsDto = mapper.Map<List<AddDoctorToProviderDTO>>(_provider.Doctors);

        return Ok(_doctorsDto);
    }

    [Authorize (Roles = "Provider")]
    [HttpGet("doctors")]
    public async Task<IActionResult> GetMyProviderDoctor()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        List<AddDoctorToProviderDTO> _doctorsDto = mapper.Map<List<AddDoctorToProviderDTO>>(_provider.Doctors);

        return Ok(_doctorsDto);
    }

    [Authorize(Roles = "Provider")]
    [HttpPut("{id}")]
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

        #region  not nessesary to update doctors here
        foreach (var doctorDTO in _providerDto.Doctors)
        {
            var doctor = provider.Doctors.FirstOrDefault(d => d.Id == doctorDTO.Id);

            if (doctor == null)
            {
                return NotFound(new { message = "Doctor not found" });
            }

            doctor.FullName = doctorDTO.FullName;
            doctor.Title = doctorDTO.Title;
            doctor.HireDate = doctorDTO.HireDate;
            doctor.YearExperience = doctorDTO.YearExperience;

            await _unit.DoctorRepository.Update(doctor);
        }
        #endregion
        await _unit.ProviderRepository.Update(provider);
        await _unit.Save();

        return Ok(new { message = "Provider updated successfully" });
    }

    #region Done Before in AccountController
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

    //     if (_provider == null)
    //     {
    //         return NotFound(new { message = "Provider not found" });
    //     }

    //     var result = _userManager.DeleteAsync(_provider).Result;

    //     if (result.Succeeded)
    //     {
    //         await _unit.ProviderRepository.Delete(_provider);
    //         await _unit.Save();
    //         return Ok(new { message = "Provider deleted successfully" });
    //     }
    //     else
    //     {
    //         return BadRequest(result.Errors);
    //     }
    // }
    #endregion

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}/schedule")]
    public async Task<IActionResult> GetProviderSchedule(string id)
    {
        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        var _providerAppointments = _provider.Appointments;

        List<DisplayProviderSceduleDTO> _providerScheduleDto = new List<DisplayProviderSceduleDTO>();

        foreach (var appointment in _providerAppointments)
        {
            var patientId = appointment.PatientId;
            DisplayProviderSceduleDTO _displayProviderSceduleDto = new DisplayProviderSceduleDTO
            {

                PatientId = patientId,
                PatientName = _unit.PatientRepository.GetById(patientId).Result?.Name,
                Status = appointment.Status,
                Reason = appointment.Reason,
                Date = appointment.Date,
                Time = appointment.Time,
            };

            _providerScheduleDto.Add(_displayProviderSceduleDto);
        }

        return Ok(_providerScheduleDto);
    }

    [Authorize (Roles = "Provider")]
    [HttpGet("schedule")]
    public async Task<IActionResult> GetProviderSchedule()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        var _providerAppointments = _provider.Appointments;

        List<DisplayProviderSceduleDTO> _providerScheduleDto = new List<DisplayProviderSceduleDTO>();

        foreach (var appointment in _providerAppointments)
        {
            var patientId = appointment.PatientId;
            DisplayProviderSceduleDTO _displayProviderSceduleDto = new DisplayProviderSceduleDTO
            {

                PatientId = patientId,
                PatientName = _unit.PatientRepository.GetById(patientId).Result?.Name,
                Status = appointment.Status,
                Reason = appointment.Reason,
                Date = appointment.Date,
                Time = appointment.Time,
            };

            _providerScheduleDto.Add(_displayProviderSceduleDto);
        }

        return Ok(_providerScheduleDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/doctors")]
    public async Task<IActionResult> AddDoctorToProvider(string id, AddDoctorToProviderDTO _addDoctorToProviderDto)
    {
        if (_addDoctorToProviderDto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();

        Provider? _provider = (await _userManager.GetUsersInRoleAsync("Provider")).OfType<Provider>().SingleOrDefault(e => e.Id == id);
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        Doctor doctor = mapper.Map<Doctor>(_addDoctorToProviderDto);
        doctor.ProviderId = _provider.Id;

        _provider.Doctors.Add(doctor);
        await _unit.DoctorRepository.Add(doctor);
        await _unit.Save();

        AddDoctorToProviderDTO view = mapper.Map<AddDoctorToProviderDTO>(doctor);
        return CreatedAtAction(nameof(GetProviderDoctors), new { id = _provider.Id }, view);
    }

    [Authorize(Roles = "Provider")]
    [HttpPost("doctors")]
    public async Task<IActionResult> AddDoctor(string id, AddDoctorToProviderDTO _addDoctorToProviderDto)
    {
        if (_addDoctorToProviderDto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();
        if (User.Identity?.Name == null)
            return Unauthorized();
        
        Provider? _provider = await _userManager.FindByNameAsync(User.Identity.Name) as Provider;
        if (_provider == null)
            return NotFound(new { message = "Provider not found" });

        Doctor doctor = mapper.Map<Doctor>(_addDoctorToProviderDto);
        doctor.ProviderId = _provider.Id;

        _provider.Doctors.Add(doctor);
        await _unit.DoctorRepository.Add(doctor);
        await _unit.Save();

        AddDoctorToProviderDTO view = mapper.Map<AddDoctorToProviderDTO>(doctor);
        return CreatedAtAction(nameof(GetProviderDoctors), new { id = _provider.Id }, view);
    }


    // isn't this a bad practice? should be a separate endpoint
    // also not making it as making an appointment?!
    // [HttpPost("{id}/patients")] 
    // public async Task<IActionResult> AddPatientToProvider(string id, AddPatientToProviderDTO _addPatientToProviderDto)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

    //         if (_provider == null)
    //         {
    //             return NotFound(new { message = "Provider not found" });
    //         }

    //         Patient _patient = new Patient
    //         {
    //             Email = _addPatientToProviderDto.Email,
    //             PhoneNumber = _addPatientToProviderDto.PhoneNumber,
    //             Name = _addPatientToProviderDto.Name,
    //             BirthDay = _addPatientToProviderDto.BirthDay,
    //             Address = _addPatientToProviderDto.Address,
    //             Gender = _addPatientToProviderDto.Gender,
    //         };

    //         await _unit.PatientRepository.Add(_patient);
    //         await _unit.Save();

    //         var result = _userManager.AddToRoleAsync(_patient, "Patient").Result;

    //         return throw new NotImplementedException();
    //     }
    // }
}