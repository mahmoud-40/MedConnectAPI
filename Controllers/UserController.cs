﻿using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IConverter converter;

    public UserController(UserManager<AppUser> userManager, IConverter converter)
    {
        this.userManager = userManager;
        this.converter = converter;
    }

    [HttpDelete]
    public IActionResult DeleteProfile(int id)
    {
        AppUser? user = userManager.FindByIdAsync(id.ToString()).Result;
        if (user == null)
            return NotFound();

        IdentityResult res = userManager.DeleteAsync(user).Result;

        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return Ok();
    }
}
