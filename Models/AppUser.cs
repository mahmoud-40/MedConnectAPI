using Microsoft.AspNetCore.Identity;

namespace Medical.Models;

public class AppUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
