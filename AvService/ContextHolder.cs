using AvService.Domain;
using AvService.Domain.Notifications;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AvService
{
    public class ContextHolder : IScanHub
    {
        IHubContext<ScanHub> hubContext;

        public ContextHolder(IHubContext<ScanHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task SendMessage(Notification notification)
        {
            await hubContext.Clients.All.SendAsync(nameof(IScanHubClient.SendAsync), notification); ;
        }
    }
}
