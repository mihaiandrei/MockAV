using System;

namespace AvService.Domain.Notifications
{
    public class Notification
    {
        public Notification()
        {
            NotificationTime = DateTime.Now;
        }
        public DateTime NotificationTime { get; set; }
    }
}
