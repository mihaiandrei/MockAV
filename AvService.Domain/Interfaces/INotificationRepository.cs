using AvService.Domain.Notifications;
using System.Collections.Generic;

namespace AvService.Domain
{
    public interface INotificationRepository
    {
        void AddNotification(Notification notification);
        void RemoveNotification(Notification notification);
        IEnumerable<Notification> GetNotifications();
    }
}