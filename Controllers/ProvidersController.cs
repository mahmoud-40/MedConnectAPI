using AutoMapper;
using Medical.Data.Interface;
using Medical.Data.Repository;
using Medical.Data.UnitOfWorks;
using Medical.DTOs.Patients;
using Medical.DTOs.Providers;
using Medical.DTOs.ProvidersDTOs;
using Medical.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers
{
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
            Provider? _provider = _userManager.GetUsersInRoleAsync("Provider").Result.OfType<Provider>().SingleOrDefault();
            if (_provider == null)
                return NotFound(new { message = "Provider not found" });

            DisplayProviderDTO _displayProviderDto = mapper.Map<DisplayProviderDTO>(_provider);

            // DisplayProviderDTO _displayProviderDto = new DisplayProviderDTO
            // {
            //     UserName = _provider.UserName,
            //     Email = _provider.Email,
            //     PhoneNumber = _provider.PhoneNumber,
            //     bio = _provider.bio,
            //     Shift = _provider.Shift,
            //     Rate = _provider.Rate,
            //     Doctors = _provider.Doctors.Select(d => new AddDoctorToProviderDTO
            //     {
            //         FullName = d.FullName,
            //         Title = d.Title,
            //         HireDate = d.HireDate,
            //         YearExperience = d.YearExperience
            //     }).ToList()
            // };

            return Ok(_displayProviderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateProviderDTO _providerDto)
        {
            var _provider = (Provider)_userManager.FindByIdAsync(_providerDto.Id).Result;

            if (_provider == null)
            {
                return NotFound(new { message = "Provider not found" });
            }

            _provider.UserName = _providerDto.UserName;
            _provider.NormalizedUserName = _providerDto.UserName.ToUpper();
            _provider.Email = _providerDto.Email;
            _provider.NormalizedEmail = _providerDto.Email.ToUpper();
            _provider.PhoneNumber = _providerDto.PhoneNumber;
            _provider.bio = _providerDto.bio;
            _provider.Shift = _providerDto.Shift;
            _provider.Rate = _providerDto.Rate;

            foreach (var doctorDTO in _providerDto.Doctors)
            {
                var doctor = _provider.Doctors.FirstOrDefault(d => d.Id == doctorDTO.Id);

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

            await _unit.ProviderRepository.Update(_provider);
            await _unit.Save();

            return Ok(new { message = "Provider updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

            if (_provider == null)
            {
                return NotFound(new { message = "Provider not found" });
            }

            var result = _userManager.DeleteAsync(_provider).Result;

            if (result.Succeeded)
            {
                await _unit.ProviderRepository.Delete(_provider);
                await _unit.Save();
                return Ok(new { message = "Provider deleted successfully" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("{id}/schedule")]
        public async Task<IActionResult> GetProviderSchedule(string id)
        {
            var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

            if (_provider == null)
            {
                return NotFound(new { message = "Provider not found" });
            }

            var _providerAppointments = _provider.Appointments;

            List<DisplayProviderSceduleDTO> _providerScheduleDto = new List<DisplayProviderSceduleDTO>();

            foreach (var appointment in _providerAppointments)
            {
                var patientId = appointment.PatientId;
                DisplayProviderSceduleDTO _displayProviderSceduleDto = new DisplayProviderSceduleDTO
                {

                    PatientId = patientId,
                    PatientName = _unit.PatientRepository.GetById(patientId).Result.Name,
                    Status = appointment.Status,
                    Reason = appointment.Reason,
                    Date = appointment.Date,
                    Time = appointment.Time,
                };

                _providerScheduleDto.Add(_displayProviderSceduleDto);
            }

            return Ok(_providerScheduleDto);
        }

        [HttpPost("{id}/doctors")]
        public async Task<IActionResult> AddDoctorToProvider(string id, AddDoctorToProviderDTO _addDoctorToProviderDto)
        {
            if(ModelState.IsValid)
            {
                var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

                if (_provider == null)
                {
                    return NotFound(new { message = "Provider not found" });
                }

                Doctor _doctor = new Doctor
                {
                    FullName = _addDoctorToProviderDto.FullName,
                    Title = _addDoctorToProviderDto.Title,
                    HireDate = _addDoctorToProviderDto.HireDate,
                    YearExperience = _addDoctorToProviderDto.YearExperience,
                    ProviderId = _provider.Id
                };

                _provider.Doctors.Add(_doctor);
                await _unit.DoctorRepository.Add(_doctor);
                await _unit.Save();

                return Ok(new { message = "Doctor added successfully" });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // isn't this a bad practice? should be a separate endpoint
        // also not making it as making an appointment?!
        [HttpPost("{id}/patients")] 
        public async Task<IActionResult> AddPatientToProvider(string id, AddPatientToProviderDTO _addPatientToProviderDto)
        {
            if (ModelState.IsValid)
            {
                var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

                if (_provider == null)
                {
                    return NotFound(new { message = "Provider not found" });
                }

                Patient _patient = new Patient
                {
                    Email = _addPatientToProviderDto.Email,
                    PhoneNumber = _addPatientToProviderDto.PhoneNumber,
                    Name = _addPatientToProviderDto.Name,
                    BirthDay = _addPatientToProviderDto.BirthDay,
                    Address = _addPatientToProviderDto.Address,
                    Gender = _addPatientToProviderDto.Gender,
                };

                await _unit.PatientRepository.Add(_patient);
                await _unit.Save();

                var result = _userManager.AddToRoleAsync(_patient, "Patient").Result;

                return throw new NotImplementedException();
            }
        }
    }
}