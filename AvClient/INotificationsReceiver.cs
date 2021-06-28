using AvService.Shared;

namespace AVClient
{
    public interface INotificationsReceiver
    {
        void ReceiveNotification(Notification notification);
    }
}