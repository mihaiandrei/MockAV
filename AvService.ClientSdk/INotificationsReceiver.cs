using AvService.Shared;

namespace AvService.ClientSdk
{
    public interface INotificationsReceiver
    {
        void ReceiveNotification(Notification notification);
    }
}