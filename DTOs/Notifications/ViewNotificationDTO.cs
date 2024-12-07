using System;

namespace Medical.DTOs.Notifications;

public class ViewNotificationDTO
{
    public int Id { get; set; }
    public string? Message { get; set; }
    public TimeSpan Since { get; set; }
    public bool IsSeen { get; set; }
}
