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

    public NotificationController(UserManager<AppUser> userManager)
    {
        this.userManager = userManager;
    }

    //[HttpGet]
    //public IActionResult Get()
    //{
    //    if (User.Identity?.Name == null)
    //        return Unauthorized();
        
    //    AppUser? user = userManager.FindByNameAsync(User.Identity.Name).Result;
    //    if (user == null)
    //        return NotFound();
        
    //    List<Notification> notifications = user.Notifications.ToList();
    //}
}
