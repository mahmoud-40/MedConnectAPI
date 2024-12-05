using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models;

// triger to get remenders
public class Notification : BaseEntity
{
    public bool IsSeen { get; set; } = false;
    public string? Message { get; set; }
    public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;

    [ForeignKey("User")]
    public required string UserId { get; set; }

    public virtual AppUser? User { get; set; }
}
