using Medical.DTOs.Account;
using System.Security.Claims;

namespace Medical.Data.Interface;

public interface IAuthService
{
    public string GenerateJWTToken(IList<Claim> claims);
    public Task<AuthDTO?> LoginAsync(LoginDTO model);

}
