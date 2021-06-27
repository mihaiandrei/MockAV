using System.Threading.Tasks;

namespace AvService
{
    public interface IScannerService
    {
        Task StartOnDemandScanAsync(string connectionId);
        void StopOnDemandScan(string connectionId);
        void DisableRealTimeScan(string connectionId);
        void EnableRealTimeScan(string connectionId);
        Task PublishUnsentNotifications(string connectionId);
        bool Connect(string connectionId);
        void Disconect(string connectionId);
    }
}