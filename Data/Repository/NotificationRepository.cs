using System;
using Medical.Data.Interface;
using Medical.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical.Data.Repository;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    private readonly MedicalContext db;

    public NotificationRepository(MedicalContext _db) : base(_db)
    {
        db = _db;
    }

    public async Task<IEnumerable<Notification>> GetByUserId(string userId, bool? isSeen = null, DateTime? from = null, DateTime? to = null)
    {
        from ??= DateTime.UtcNow.AddDays(-7);
        to ??= DateTime.UtcNow;

        if (isSeen is null)
            return await db.Notifications.Where(e => e.UserId == userId && e.ReleaseDate <= DateTime.UtcNow && e.CreatedAt >= from && e.CreatedAt <= to).ToListAsync();
        return await db.Notifications.Where(e => e.UserId == userId && e.IsSeen == isSeen && e.ReleaseDate <= DateTime.UtcNow && e.CreatedAt >= from && e.CreatedAt <= to).ToListAsync();
    }

    public async Task MarkAsRead(int id)
    {
        Notification? notification = await GetById(id);
        if (notification != null)
        {
            notification.IsSeen = true;
            await Update(notification);
        }
    }

    public async Task MarkAllAsIsRead(string userId, bool isSeen)
    {
        IEnumerable<Notification> notifications = await GetByUserId(userId);
        foreach (Notification notification in notifications)
        {
            notification.IsSeen = isSeen;
            await Update(notification);
        }
    }

    public async Task<Notification> Add(string userId, string? message, DateTime? at = null)
    {
        if (at == null)
        {
            at = DateTime.UtcNow;
        }

        Notification notification = new()
        {
            UserId = userId,
            Message = message,
            ReleaseDate = at.Value
        };

        await Add(notification);
        return notification;
    }
}
