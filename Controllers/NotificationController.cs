using AutoMapper;
using Medical.Data.Interface;
using Medical.DTOs.Notifications;
using Medical.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [SwaggerOperation(
        Summary = "Get all notifications",
        Description = "Get all notifications for the current user\n" +
            "Query parameters: _from (DateTime), _to (DateTime)\n" +
            "_from (optional): get notifications from this date default -> Last 7 Days\n" +
            "_to (optional): get notifications to this date default -> Today" +
            "example: /api/notification?_from=2024-12-01&_to=2024-12-10"
    )]
    [ProducesResponseType(typeof(List<ViewNotificationDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? _from = null, [FromQuery] DateTime? _to = null)
    {
       if (User.Identity?.Name == null)
           return Unauthorized();

       AppUser? user = userManager.FindByNameAsync(User.Identity.Name).Result;
       if (user == null)
           return NotFound(new { message = "User not found" });

        var notifications = await unit.NotificationRepository.GetByUserId(user.Id, from: _from, to: _to);
        foreach (var notification in notifications)
            notification.IsSeen = true;
        List<ViewNotificationDTO> views = mapper.Map<List<ViewNotificationDTO>>(notifications);
        return Ok(views);
    }

    [SwaggerOperation(
        Summary = "Get new notifications",
        Description = "Get new notifications for the current user" +
            "example: /api/notification/new"
    )]
    [ProducesResponseType(typeof(List<ViewNotificationDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet("new")]
    public async Task<IActionResult> GetNew()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        AppUser? user = userManager.FindByNameAsync(User.Identity.Name).Result;
        if (user == null)
            return NotFound(new { message = "User not found" });

        var notifications = await unit.NotificationRepository.GetByUserId(user.Id, false);
        foreach (var notification in notifications)
            notification.IsSeen = true;

        List<ViewNotificationDTO> views = mapper.Map<List<ViewNotificationDTO>>(notifications);
        return Ok(views);
    }

    [SwaggerOperation(
        Summary = "Get notification by id",
        Description = "Get notification by id required Admin role" +
            "example: /api/notification/1"
    )]
    [ProducesResponseType(typeof(ViewNotificationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Notification? notification = await unit.NotificationRepository.GetById(id);
        if (notification == null)
            return NotFound(new { message = "Notification not found" });

        if (!User.IsInRole("Admin"))
        {
            AppUser? user = await userManager.FindByNameAsync(User.Identity?.Name ?? "");
            if (user == null)
                return NotFound(new { message = "User not found" });
            if (notification.UserId != user.Id)
                return Unauthorized();
            notification.IsSeen = true;
        }
        ViewNotificationDTO view = mapper.Map<ViewNotificationDTO>(notification);
        return Ok(view);
    }

    [SwaggerOperation(
        Summary = "Sent notification",
        Description = "Sent notification to the user required Admin role" +
            "example: /api/notification/sent"
    )]
    [ProducesResponseType(typeof(ViewNotificationDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
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
            return NotFound(new { message = "User not found" });
        
        Notification notification = await unit.NotificationRepository.Add(dto.UserId, dto.Message, dto.ReleaseDate);
        await unit.Save();

        ViewNotificationDTO view = mapper.Map<ViewNotificationDTO>(notification);
        return CreatedAtAction(nameof(GetById), new { id = notification.Id }, view);
    }
}
