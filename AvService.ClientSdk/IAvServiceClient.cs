using System.Threading.Tasks;

namespace AvService.ClientSdk
{
    public interface IAvServiceClient
    {
        bool IsConnected { get; }
        bool IsConnecting { get; }

        Task Connect();
        Task DisableRealTimeScan();
        Task Disconect();
        Task EnableRealTimeScan();
        Task PublishUnsentNotifications();
        Task StartOnDemandScanAsync();
        Task StopOnDemandScan();
    }
}