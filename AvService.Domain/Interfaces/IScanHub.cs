using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public interface IScanHub
    {
        Task SendMessage(Notification notification, string connectionId);
    }
}
