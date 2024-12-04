using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Notification;
using Medical.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IUnitOfWork unit;
    private readonly IMapper mapper;

    public NotificationController(UserManager<AppUser> userManager, IUnitOfWork unit, IMapper mapper)
    {
        this.userManager = userManager;
        this.unit = unit;
        this.mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? _from = null, [FromQuery] DateTime? _to = null)
    {
       if (User.Identity?.Name == null)
           return Unauthorized();

       AppUser? user = userManager.FindByNameAsync(User.Identity.Name).Result;
       if (user == null)
           return NotFound();

        var notifications = await unit.NotificationRepository.GetByUserId(user.Id, from: _from, to: _to);
        List<ViewNotificationDTO> views = mapper.Map<List<ViewNotificationDTO>>(notifications);
        return Ok(views);
    }

    [HttpGet("new")]
    public async Task<IActionResult> GetNew()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        AppUser? user = userManager.FindByNameAsync(User.Identity.Name).Result;
        if (user == null)
            return NotFound();

        var notifications = await unit.NotificationRepository.GetByUserId(user.Id, false);
        List<ViewNotificationDTO> views = mapper.Map<List<ViewNotificationDTO>>(notifications);
        return Ok(views);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Notification? notification = await unit.NotificationRepository.GetById(id);
        if (notification == null)
            return NotFound();

        ViewNotificationDTO view = mapper.Map<ViewNotificationDTO>(notification);
        return Ok(view);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("sent")]
    public async Task<IActionResult> Sent([FromBody] AddNotificationDTO dto)
    {
        if (dto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (dto.ReleaseDate != null && dto.ReleaseDate < DateTime.UtcNow)
            return BadRequest("Release date must be in the future");

        AppUser? user = userManager.FindByIdAsync(dto.UserId).Result;
        if (user == null)
            return NotFound();
        
        Notification notification = await unit.NotificationRepository.Add(dto.UserId, dto.Message, dto.ReleaseDate);
        await unit.Save();

        ViewNotificationDTO view = mapper.Map<ViewNotificationDTO>(notification);
        return CreatedAtAction(nameof(GetById), new { id = notification.Id }, view);
    }
}
