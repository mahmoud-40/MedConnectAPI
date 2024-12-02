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
        UserManager<AppUser> _userManager;
        private IUnitOfWork _unit;
        public ProvidersController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            _userManager = userManager;
            _unit = unit;
        }

        // POST /providers/register
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

        // POST /providers/login
        //[HttpPost("login")]
        //public IActionResult Login()
        //{
        //    return Ok(new { message = "Login successful" });
        //}

        // GET /providers/{id}:
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvider(string id)
        {
            var _provider = (Provider)_userManager.FindByIdAsync(id).Result;

            if (_provider == null)
            {
                return NotFound(new { message = "Provider not found" });
            }

            DisplayProviderDTO _displayProviderDto = new DisplayProviderDTO
            {
                UserName = _provider.UserName,
                Email = _provider.Email,
                PhoneNumber = _provider.PhoneNumber,
                bio = _provider.bio,
                Shift = _provider.Shift,
                Rate = _provider.Rate,
            };

            var doctors = await _unit.DoctorRepository.GetAll();
            foreach (var doctor in doctors)
            {
                if (doctor.ProviderId == _provider.Id)
                {
                    AddDoctorToProviderDTO _addDoctorToProviderDto = new AddDoctorToProviderDTO
                    {
                        FullName = doctor.FullName,
                        Title = doctor.Title,
                        HireDate = doctor.HireDate,
                        YearExperience = doctor.YearExperience
                    };

                    _displayProviderDto.Doctors.Add(_addDoctorToProviderDto);
                }
            }

            return Ok(_displayProviderDto);
        }
    }
}