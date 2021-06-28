using Reinforced.Typings.Attributes;
using System.Threading.Tasks;

namespace AvService.Shared
{
    public interface IScanHubClient
    {
        Task SendStartScanOnDemandNotification(StartScanOnDemandNotification notification);
        Task SendStopScanOnDemandNotification(StopScanOnDemandNotification notification);
        Task SendStopScanSuccessNotification(StopScanSuccessNotification notification);
        Task SendThreatFoundNotification(ThreatFoundNotification notification);
        Task SendScanInProgressNotification(ScanInProgressNotification notification);
        Task DisconnectClient();
    }
}