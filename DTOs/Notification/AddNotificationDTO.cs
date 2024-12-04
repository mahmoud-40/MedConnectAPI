using System;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Notification;

public class AddNotificationDTO
{
    [Required]
    [StringLength(256, ErrorMessage = "max len 256")]
    public required string UserId { get; set; }

    public string? Message { get; set; }

    public DateTime? ReleaseDate { get; set; } = null;
}
