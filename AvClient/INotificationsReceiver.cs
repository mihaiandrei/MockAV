using AvService.Domain.Notifications;

namespace AVClient
{
    public interface INotificationsReceiver
    {
        void ReceiveNotification(Notification notification);
    }
}