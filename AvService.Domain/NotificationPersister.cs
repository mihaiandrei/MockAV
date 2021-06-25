using AvService.Domain.Notifications;
using System.Collections.Generic;

namespace AvService.Domain
{
    public class NotificationPersister : INotificationPersister
    {
        private List<Notification> notifications = new List<Notification>();


        public void AddNotification(Notification notification)
        {
            notifications.Add(notification);
        }
    }
}
