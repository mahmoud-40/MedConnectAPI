using System;
using Medical.Models;

namespace Medical.Data.Interface;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserId(string userId, bool? isSeen = null, DateTime? from = null, DateTime? to = null);

    Task MarkAsRead(int id);

    Task MarkAllAsIsRead(string userId, bool isSeen);

    Task<Notification> Add(string userId, string? message, DateTime? at = null);
}
