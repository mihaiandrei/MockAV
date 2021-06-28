using AvService.Shared;
using System.Collections.Generic;
using System.Linq;

namespace AvService.Domain
{
    public class NotificationRepository : INotificationRepository
    {
        private List<Notification> notifications = new List<Notification>();

        public void AddNotification(Notification notification)
        {
            notifications.Add(notification);
        }

        public void RemoveNotification(Notification notification)
        {
            notifications.Remove(notification);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return notifications.ToList();
        }
    }
}
