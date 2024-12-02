using Azure.Core;
using Medical.Data.Interface;
using Medical.DTOs.Account;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical.Data.Repository
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTHelper jwt;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> _signInManager, IOptions<JWTHelper> jwt, IEmailService emailService,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this._signInManager = _signInManager;
            this.emailService = emailService;
            this.roleManager = roleManager;
            this.jwt = jwt.Value;
        }


        #region Login 

        public async Task<AuthDTO> LoginAsync(LoginDTO model)
        {



            var user = await userManager.FindByNameAsync(model.UserName);

            var claims = new List<Claim>
               {
             new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
             new Claim(ClaimTypes.Name,user.UserName),

        };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var itemrole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, itemrole)); // Include role claim here
            }

            if (user == null)
            {
                return new AuthDTO { Message = "Invalid User", IsAuthenticated = false };

            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                var jwtsecurtiytoken = GenerateJWTToken(claims);

                return new AuthDTO
                {
                    Message = "Login succeed",
                    IsAuthenticated = true,
                    Token = jwtsecurtiytoken
                };
            }
            else
            {
                return new AuthDTO { Message = "Invalid Data", IsAuthenticated = false };
            }




        }
        #endregion

        #region Generate Token

        public string GenerateJWTToken(IList<Claim> claims)
        {
            // Create a new instance of JwtSecurityTokenHandler to handle JWT token creation and validation
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwt.Key);
            var claimsidentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsidentity,
                // Add any additional claims as needed

                Expires = DateTime.UtcNow.AddMinutes(5), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwt.Issuer,
                Audience = jwt.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenstring = tokenHandler.WriteToken(token);

            return tokenstring;
        }
        #endregion

       




    }



}


