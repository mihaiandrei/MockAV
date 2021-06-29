using AvService.Shared;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class Notifier : INotifier
    {
        private readonly IConnectedClientManager connectedClientManager;
        private readonly INotificationRepository notificationRepository;
        private readonly IScanHub scanHub;

        public Notifier(IScanHub scanHub,
                        IConnectedClientManager connectedClientManager,
                        INotificationRepository notificationRepository)
        {
            this.connectedClientManager = connectedClientManager;
            this.notificationRepository = notificationRepository;
            this.scanHub = scanHub;
        }

        public async Task SendAsync(Notification notification)
        {
            if (connectedClientManager.IsClientConected)
                await scanHub.SendNotification(notification, connectedClientManager.ConnectionId);
            else
                await notificationRepository.AddNotificationAsync(notification);
        }

        public async Task PushUnsentNotifications()
        {
            if (!connectedClientManager.IsClientConected)
                return;

            var unsentNotifications = await notificationRepository.GetNotificationsAsync();

            foreach (var notification in unsentNotifications)
            {
                await scanHub.SendNotification(notification, connectedClientManager.ConnectionId);
            }
            await notificationRepository.RemoveNotificationsAsync();
        }

        public async Task DisconectClient(string connectionId)
        {
            await scanHub.DisconnectClient(connectionId);
        }
    }
}
