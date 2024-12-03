using AutoMapper;
using Medical.Data.Interface;
using Medical.Data.Repository;
using Medical.Data.UnitOfWorks;
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

            var doctors = await _unit.DoctorRepository.GetAll();
            foreach (var doctor in doctors)
            {
                if (doctor.ProviderId == _provider.Id)
                {
                    AddDoctorToProviderDTO _addDoctorToProviderDto = mapper.Map<AddDoctorToProviderDTO>(doctor);

                    _displayProviderDto.Doctors.Add(_addDoctorToProviderDto);
                }
            }

            return Ok(_displayProviderDto);
        }
    }
}