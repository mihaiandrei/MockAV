using System;

namespace AvService.Repository
{
    public class NotificationDocument
    {
        public Guid Id{ get; set; }
        public string Payload { get; set; }
        public string NotificationType { get; set; }
    }
}
