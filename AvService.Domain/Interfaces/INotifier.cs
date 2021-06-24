using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService
{
    public interface INotifier
    {
        Task SendAsync(Notification notification);
        bool ScanInProgress { get; }
    }
}