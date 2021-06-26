using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService
{
    public interface IScanHubClient
    {
        Task SendAsync(Notification notification);
    }
}