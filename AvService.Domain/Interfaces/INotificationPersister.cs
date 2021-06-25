using AvService.Domain.Notifications;
using System.Collections.Generic;

namespace AvService.Domain
{
    public interface INotificationPersister
    {
        void AddNotification(Notification notification);
        void RemoveNotification(Notification notification);
        IEnumerable<Notification> GetNotifications();
    }
}