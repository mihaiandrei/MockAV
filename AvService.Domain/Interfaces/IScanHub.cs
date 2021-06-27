using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public interface IScanHub
    {
        Task SendNotification(Notification notification, string connectionId);
        Task DisconnectClient(string connectionId);
    }
}
