using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Medical.Models;

public class AppUser : IdentityUser
{
    [StringLength(256)]
    [Required]
    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual List<Notification> Notification { get; set; } = new List<Notification>();
}
