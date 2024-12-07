using Medical.Data.Interface;
using Medical.DTOs.Account;
using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical.Data.Repository;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration config;
    private readonly JWTHelper jwt;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> _signInManager, IConfiguration config, IOptions<JWTHelper> jwt)
    {
        this.userManager = userManager;
        this._signInManager = _signInManager;
        this.config = config;
        this.jwt = jwt.Value;
    }

    public async Task<AuthDTO?> LoginAsync(LoginDTO model)
    {
        AppUser? user = await userManager.FindByNameAsync(model.UserName);
        if (user == null)
            return null;

        List<Claim>? claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name,user.UserName ?? ""),
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

        SignInResult? result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
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

    public string GenerateJWTToken(IList<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(jwt.Key);
        var claimsidentity = new ClaimsIdentity(claims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsidentity,
            // Add any additional claims as needed

            Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int?>("JWT:DurationInMinutes") ?? 5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwt.Issuer,
            Audience = jwt.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenstring = tokenHandler.WriteToken(token);

        return tokenstring;
    }
}
