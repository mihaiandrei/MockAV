using AvService.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task RemoveNotificationsAsync();
        Task<IEnumerable<Notification>> GetNotificationsAsync();
    }
}