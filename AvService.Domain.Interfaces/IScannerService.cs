using System.Threading.Tasks;

namespace AvService.Domain
{
    public interface IScannerService
    {
        Task StartOnDemandScanAsync(string connectionId);
        void StopOnDemandScan(string connectionId);
        void DisableRealTimeScan(string connectionId);
        void EnableRealTimeScan(string connectionId);
        Task PublishUnsentNotifications(string connectionId);
        Task Connect(string connectionId);
        void Disconect(string connectionId);
    }
}