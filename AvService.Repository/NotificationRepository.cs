using AvService.Domain;
using AvService.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvService.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public async Task AddNotificationAsync(Notification notification)
        {
            using (var context = new DatabaseContext())
            {
                context.Documents.Add(new NotificationDocument()
                {
                    Id = Guid.NewGuid(),
                    NotificationType = notification.GetType().AssemblyQualifiedName,
                    Payload = JsonConvert.SerializeObject(notification)
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return (await context.Documents.ToListAsync()).Select(document =>
                {
                    Type protocolType = Type.GetType(document.NotificationType);
                    return (Notification)JsonConvert.DeserializeObject(document.Payload, protocolType);
                });
            }
        }

        public async Task RemoveNotificationsAsync()
        {
            using (var context = new DatabaseContext())
            {
                var documents = await context.Documents.ToListAsync();
                context.RemoveRange(documents);
                await context.SaveChangesAsync();
            }
        }
    }
}