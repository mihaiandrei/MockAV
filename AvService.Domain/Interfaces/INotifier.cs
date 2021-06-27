using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService
{
    public interface INotifier
    {
        Task PushUnsentNotifications();
        Task SendAsync(Notification notification);
        Task DisconectClient(string connectionId);
    }
}