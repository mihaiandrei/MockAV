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
                case StartScanOnDemandNotification n:
                    await hubContext.Clients.All.SendStartScanOnDemandNotification(n);
                    break;

                case StopScanSuccessNotification n:
                    await hubContext.Clients.All.SendStopScanSuccessNotification(n);
                    break;

                case ScanInProgressNotification n:
                    await hubContext.Clients.All.SendScanInProgressNotification(n);
                    break;

                case StopScanOnDemandNotification n:
                    await hubContext.Clients.All.SendStopScanOnDemandNotification(n);
                    break;

                case ThreatFoundNotification n:
                    await hubContext.Clients.All.SendThreatFoundNotification(n);
                    break;

            }
        }
    }
}
