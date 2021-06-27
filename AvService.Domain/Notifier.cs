using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class Notifier : INotifier
    {
        private readonly IConnectedClientManager connectedClientManager;
        private readonly INotificationRepository notificationPersister;
        private readonly IScanHub scanHub;

        public Notifier(IScanHub scanHub,
                        IConnectedClientManager connectedClientManager,
                        INotificationRepository notificationPersister)
        {
            this.connectedClientManager = connectedClientManager;
            this.notificationPersister = notificationPersister;
            this.scanHub = scanHub;
        }

        public async Task SendAsync(Notification notification)
        {
            if (connectedClientManager.IsClientConected)
                await scanHub.SendMessage(notification, connectedClientManager.ConnectionId);
            else
                notificationPersister.AddNotification(notification);
        }

        public async Task PushUnsentNotifications()
        {
            if (!connectedClientManager.IsClientConected)
                return;

            var unsentNotifications = notificationPersister.GetNotifications();
            foreach (var notification in unsentNotifications)
            {
                await scanHub.SendMessage(notification, connectedClientManager.ConnectionId);
                notificationPersister.RemoveNotification(notification);
            }
        }
    }
}
