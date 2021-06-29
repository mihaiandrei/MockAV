using AvService.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationRepository
{
    public class NotificationRepository
    {
        public async Task AddNotification(Notification notification)
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

        public async Task RemoveNotification(Notification notification)
        {
            using (var context = new DatabaseContext())
            {
                context.Documents.Remove(new NotificationDocument());
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Notification>> GetNotifications()
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
    }
}