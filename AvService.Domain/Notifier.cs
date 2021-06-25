using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class Notifier : INotifier
    {
        private readonly IConnectedClientManager connectedClientManager;
        private readonly INotificationPersister notificationPersister;

        public Notifier(IConnectedClientManager connectedClientManager,
                        INotificationPersister notificationPersister)
        {
            this.connectedClientManager = connectedClientManager;
            this.notificationPersister = notificationPersister;
        }

        public async Task SendAsync(Notification notification)
        {
            if (connectedClientManager.IsClientConected)
                await Task.Yield();
            else
                notificationPersister.AddNotification(notification);
        }
    }
}
