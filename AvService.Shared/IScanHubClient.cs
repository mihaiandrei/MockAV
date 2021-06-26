using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService
{
    public interface IScanHubClient
    {
        Task SendStartScanOnDemandNotification(StartScanOnDemandNotification notification);
        Task SendStopScanOnDemandNotification(StopScanOnDemandNotification notification);
        Task SendStopScanSuccessNotification(StopScanSuccessNotification notification);
        Task SendThreatFoundNotification(ThreatFoundNotification notification);
        Task SendScanInProgressNotification(ScanInProgressNotification notification);
    }
}