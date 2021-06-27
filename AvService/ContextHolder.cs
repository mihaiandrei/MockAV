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

        public async Task SendNotification(Notification notification, string connectionId)
        {
            switch (notification)
            {
                case StartScanOnDemandNotification n:
                    await hubContext.Clients.Clients(connectionId).SendStartScanOnDemandNotification(n);
                    break;

                case StopScanSuccessNotification n:
                    await hubContext.Clients.Clients(connectionId).SendStopScanSuccessNotification(n);
                    break;

                case ScanInProgressNotification n:
                    await hubContext.Clients.Clients(connectionId).SendScanInProgressNotification(n);
                    break;

                case StopScanOnDemandNotification n:
                    await hubContext.Clients.Clients(connectionId).SendStopScanOnDemandNotification(n);
                    break;

                case ThreatFoundNotification n:
                    await hubContext.Clients.Clients(connectionId).SendThreatFoundNotification(n);
                    break;
            }
        }

        public async Task DisconnectClient(string connectionId)
        {
            await hubContext.Clients.Clients(connectionId).DisconnectClient();
        }
    }
}
