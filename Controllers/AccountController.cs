using AutoMapper;
using Medical.Data.Interface;
using Medical.Data.UnitOfWorks;
using Medical.DTOs.Account;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Medical.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> usermanager;
    private readonly IEmailService emailService;
    private readonly IAuthService authService;
    private readonly SignInManager<AppUser> signIn;
    private readonly IUnitOfWork unit;
    private readonly IValidator validator;
    private readonly IMapper mapper;

    public AccountController(UserManager<AppUser> _usermanager, IAuthService authService, SignInManager<AppUser> signIn, IUnitOfWork unit, IEmailService emailService, IValidator validator, IMapper mapper)
    {
        this.usermanager = _usermanager;
        this.emailService = emailService;
        this.authService = authService;
        this.signIn = signIn;
        this.unit = unit;
        this.validator = validator;
        this.mapper = mapper;
    }

    [SwaggerOperation(Summary = "Register a new user and assigns them to a role", Description ="Registers a new user, sends an email confirmation link, and assigns the user to the appropriate role.\n\n" +
        "Note:\n" +
        "- UserType of  **Patient** is `1`.\n"+
         "- UserType of  **Provider** is `2`.\n\n" +
          "- Gender of  **Male** is `1`.\n" +
         "- Gender of  **Female** is `2`.\n\n" +
         "- Shift of  **Morning** for Provider(Clinic) is `1`.\n" +
         "- Shift of  **Evening** for Provider(Clinic) is `2`.\n" +
         "- Shift of  **Night**  for Provider(Clinic) is `3`.\n\n" +
        "`/api/Account/Register`")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerPatientDTO)
    {
        if (registerPatientDTO == null)
            return BadRequest("Invalid Data");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AppUser user;

        if (registerPatientDTO.UserType == AppUserType.Patient)
        {
            if (!validator.IsBirthdayValid(registerPatientDTO.BirthDay, out Exception ex))
                return BadRequest(ex.Message);

            user = mapper.Map<Patient>(registerPatientDTO);
        }
        else if (registerPatientDTO.UserType == AppUserType.Provider)
        {
            user = mapper.Map<Provider>(registerPatientDTO);
        }
        else
        {
            return BadRequest("Invalid User Type");
        }

        IdentityResult result = await usermanager.CreateAsync(user, registerPatientDTO.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        result = await usermanager.AddToRoleAsync(user, registerPatientDTO.UserType.ToString());
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Generate email confirmation token
        string? token = await usermanager.GenerateEmailConfirmationTokenAsync(user);

        // Generate confirmation link
        string? confirmationLink = Url.Action("ConfirmEmail", "Account",
            new { userId = user.Id, token = token }, Request.Scheme);

        // await emailService.SendEmailAsync(user.Email!, "Confirm Your Email",
        //     $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");
        await emailService.SendEmailAsync("healthcaresystem878@gmail.com", "Confirm Your Email",
            $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");

        await unit.Save();

        return Ok("User Registered and Please Confirm Your Email");
    }

    [HttpPost("Login")]
    [SwaggerOperation(
        Summary = "Authenticate a user and retrieve a JWT token",
        Description = "Logs in a user by validating their credentials and returns a JWT token if successful.\n\n" +
            "`/api/Account/Login`"
    )]
    [ProducesResponseType(typeof(AuthDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        if (loginDTO == null)
            return BadRequest("Invalid Data");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AuthDTO? result = await authService.LoginAsync(loginDTO);
        if (result == null)
            return Unauthorized();

        if (!result.IsAuthenticated)
            return Unauthorized();

        return Ok(result);
    }


    [HttpGet("ConfirmEmail")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
            return BadRequest("Invalid email confirmation request.");

        var user = await usermanager.FindByIdAsync(userId);
        if (user == null)
            return NotFound($"User Not Found");

        var result = await usermanager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            return BadRequest("Email confirmed Failed.");
        
        await unit.NotificationRepository.Add(user.Id, "Email Confirmed Successfully");
        await unit.NotificationRepository.Add(user.Id, "Welcome to the Medical System");
        await unit.Save();
        return Ok("Email confirmed successfully.");
    }

    [HttpPost("ForgetPassword")]
    [SwaggerOperation(
        Summary = "Request a password reset link",
        Description = "Allows a user to request a password reset link. If the email is valid, a reset link is sent to the user's email address.\n\n" +
            "`/api/Account/ForgetPassword`"
    )]
    [ProducesResponseType(typeof(ForgetPasswordDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
    {
        if (forgetPasswordDTO == null)
            return BadRequest("Invalid Data");
        if (!ModelState.IsValid)
            return BadRequest("Invalid email format or missing fields.");

        var user = await usermanager.FindByEmailAsync(forgetPasswordDTO.Email);

        if (user != null)
        {
            if (!await usermanager.IsEmailConfirmedAsync(user))
                return BadRequest("Email is not confirmed.");
            
            var token = await usermanager.GeneratePasswordResetTokenAsync(user);
            var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordDTO.Email, Token = token }, Request.Scheme);

            // await emailService.SendEmailAsync(user.Email!, "ResetPassword", "Please reset your email." + passwordResetLink);
            await emailService.SendEmailAsync("healthcaresystem878@gmail.com", "ResetPassword", "Please reset your email." + passwordResetLink);
        }

        return Ok (new { Message = "If an account with that email exists, a password reset link has been sent." });
    }

    [HttpGet("ResetPassword")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public IActionResult ResetPassword(string token, string email)
    {
        var model = new ResetPasswordDTO { Email = email, Token = token };
        return Ok(new { model });
    }

    [HttpPost]
    [Route("ResetPassword")]
    [SwaggerOperation(
        Summary = "Reset the user's password",
        Description = "Allows a user to reset their password using a token received via email.\n\n"+
            "`/api/Account/ResetPassword`"
    )]
    [ProducesResponseType(typeof(ResetPasswordDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
    {
        if (resetPasswordDTO == null)
            return BadRequest("Invalid Data");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await usermanager.FindByEmailAsync(resetPasswordDTO.Email!);
        if (user == null)
            return NotFound("Email Not Found");

        var result = await usermanager.ResetPasswordAsync(user, resetPasswordDTO.Token ?? "", resetPasswordDTO.Password ?? "");
        if (!result.Succeeded)
            return BadRequest("Reset Password is Failed");

        await unit.NotificationRepository.Add(user.Id, "Password Reset Successfully");
        return Ok("Password has been reset successfully");
    }


    [SwaggerOperation(
        Summary = "Delete an Account",
        Description = "Deleta an Account Based on Id Need Admin Role" +
            "`/api/Account`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteAccount(string id)
    {
        AppUser? user = usermanager.FindByIdAsync(id).Result;
        if (user == null)
            return NotFound(new { message = "User not found" });

        IdentityResult res = usermanager.DeleteAsync(user).Result;

        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return Ok(new { message = "User deleted successfully" });
    }

    [SwaggerOperation(
        Summary = "Delete Profile",
        Description = "Delete Profile Based on Login User" +
            "`/api/Account`"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpDelete("`/api/profile`")]
    public async Task<IActionResult> DeleteProfile()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        AppUser? user = usermanager.FindByNameAsync(User.Identity.Name).Result;
        if (user == null)
            return NotFound();
        
        IdentityResult res = usermanager.DeleteAsync(user).Result;
        if (!res.Succeeded)
            return BadRequest(res.Errors);
        
        await signIn.SignOutAsync();
        return Ok(new { message = "User deleted successfully" });
    }
}
