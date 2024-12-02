using Medical.Data.Interface;
using Medical.DTOs.Account;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace Medical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> usermanager;
        private readonly RoleManager<IdentityRole> identityrole;
        private readonly IEmailService emailService;
        private readonly IAuthService authService;

        public AccountController(UserManager<AppUser> _usermanager, RoleManager<IdentityRole> _identityrole, IEmailService emailService, IAuthService authService)
        {
            this.usermanager = _usermanager;
            this.identityrole = _identityrole;
            this.emailService = emailService;
            this.authService = authService;
        }



        #region Register
        [HttpPost]
        [SwaggerOperation(Summary = "Register a new user and assigns them to a role", Description ="Registers a new user, sends an email confirmation link, and assigns the user to the appropriate role.\n\n" +
            "Note:\n" +
            "- UserType of  **Patient** is `1`.\n"+
             "- UserType of  **Provider** is `2`.\n\n" +
              "- Gender of  **Male** is `1`.\n" +
             "- Gender of  **Female** is `2`.\n\n" +
             "- Shift of  **Morning** for Provider(Clinic) is `1`.\n" +
             "- Shift of  **Evening** for Provider(Clinic) is `2`.\n" +
             "- Shift of  **Night**  for Provider(Clinic) is `3`.\n\n" +
            "https://localhost:7024/api/Account/Register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerPatientDTO)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }


            else
            {

                AppUser user;

                if (registerPatientDTO.UserType == AppUserType.Patient) { 

                    user = new Patient
                    {
                        UserName = registerPatientDTO.FullName,
                        Email = registerPatientDTO.Email,
                        BirthDay = registerPatientDTO.BirthDay,
                        Address = registerPatientDTO.Address,
                        Gender = registerPatientDTO.Gender
                    };
            }

                else if (registerPatientDTO.UserType == AppUserType.Provider)
                {
                    user = new Provider
                    {
                        UserName = registerPatientDTO.FullName,
                        Email = registerPatientDTO.Email,
                        Shift = registerPatientDTO.Shift
                    };
                }
                else
                {
                    return BadRequest("Invalid User");
                }


                var result = await usermanager.CreateAsync(user, registerPatientDTO.Password);

                if (result.Succeeded)
                {

                    // Generate email confirmation token
                    var token = await usermanager.GenerateEmailConfirmationTokenAsync(user);

                    // Generate confirmation link
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    // Send email
                    await emailService.SendEmailAsync("healthcaresystem878@gmail.com", "Confirm Your Email",
                        $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");

                    //Assigned To Role
                    var role = await usermanager.AddToRoleAsync(user,registerPatientDTO.UserType.ToString());
                    if (role.Succeeded)
                    {
                        return Ok("User Registered and Please Confirm Your Email");

                    }
                    else
                    {
                        return BadRequest(role.Errors);
                    }

                }
                else
                {
                    return BadRequest(result.Errors);

                }
            

            }

        }
        #endregion

        #region Login
        [HttpPost("Login")]
        [SwaggerOperation(
    Summary = "Authenticate a user and retrieve a JWT token",
    Description = "Logs in a user by validating their credentials and returns a JWT token if successful.\n\n" +
            "https://localhost:7024/api/Account/Login"
       )]
        [ProducesResponseType(typeof(AuthDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.LoginAsync(loginDTO);

            if (!result.IsAuthenticated)
                return Unauthorized();

            return Ok(result);
        }


        #endregion



        #region ConfrimEmail


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
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }
            else
            {
                return BadRequest("Email confirmed Failed.");
            }

        }
        #endregion

        #region ForgetPassword
        [HttpPost("ForgetPassword")]
        [SwaggerOperation(
    Summary = "Request a password reset link",
    Description = "Allows a user to request a password reset link. If the email is valid, a reset link is sent to the user's email address.\n\n" +
            "https://localhost:7024/api/Account/ForgetPassword"
        )]
          [ProducesResponseType(typeof(ForgetPasswordDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid email format or missing fields.");
            }

            var user = await usermanager.FindByEmailAsync(forgetPasswordDTO.Email);


            if (user != null)
            {
                var token = await usermanager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordDTO.Email, Token = token }, Request.Scheme);

                emailService.SendEmailAsync("healthcaresystem878@gmail.com", "ResetPassword", "Please reset your email."+passwordResetLink );
                return Ok(new { Message = "If an account with that email exists, a password reset link has been sent." });



            }
            else
            {
                return NotFound("Email Not Found");

            }

        }
        #endregion


        #region ResetPassword

        [HttpGet("ResetPassword")]
        [ApiExplorerSettings(IgnoreApi = true)]



        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDTO { Email = email, Token = token };

            return Ok(

                new
                {
                    model
                });

        }
        [HttpPost]
        [Route("ResetPassword")]
        [SwaggerOperation(
    Summary = "Reset the user's password",
    Description = "Allows a user to reset their password using a token received via email.\n\n"+
            "https://localhost:7024/api/Account/ResetPassword"
      )]
        [ProducesResponseType(typeof(ResetPasswordDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await usermanager.FindByEmailAsync(resetPasswordDTO.Email!);

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    

                    var result = await usermanager.ResetPasswordAsync(user, resetPasswordDTO.Token!, resetPasswordDTO.Password!);

                    if (result.Succeeded)
                    {
                        return Ok("Password Is Reseted");
                    }



                    else
                    {
                        return BadRequest("Reset Password is Failed ");
                    }


                }
                else
                {
                    return NotFound("Email Not Found");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
        #endregion

    }
}
    

