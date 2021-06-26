using AvService.Domain;
using AvService.Domain.Notifications;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AvService
{
    public class ContextHolder : IScanHub
    {
        IHubContext<ScanHub, IScanHubClient> hubContext;

        public ContextHolder(IHubContext<ScanHub, IScanHubClient> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task SendMessage(Notification notification)
        {
            switch (notification)
            {
                case StartScanOnDemandNotification notification1:
                    await hubContext.Clients.All.SendStartScanOnDemandNotification(notification1);
                    break;

                case StopScanSuccessNotification notification1:
                    await hubContext.Clients.All.SendStopScanSuccessNotification(notification1);
                    break;

                case StopScanOnDemandNotification notification1:
                    await hubContext.Clients.All.SendStopScanOnDemandNotification(notification1);
                    break;

                case ThreatFoundNotification notification1:
                    await hubContext.Clients.All.SendThreatFoundNotification(notification1);
                    break;

            }
        }
    }
}
