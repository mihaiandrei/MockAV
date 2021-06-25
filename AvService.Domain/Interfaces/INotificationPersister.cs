using AvService.Domain.Notifications;

namespace AvService.Domain
{
    public interface INotificationPersister
    {
        void AddNotification(Notification notification);
    }
}