using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class Notifier : INotifier
    {
        public async Task SendAsync(Notification notification)
        {
            await Task.Yield();
        }
    }
}
