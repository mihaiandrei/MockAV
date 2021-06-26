using System.Threading.Tasks;

namespace AvService
{
    public interface IServerScanHub
    {
        void Connect();
        void Disconect();

        void DisableRealTimeScan();
        void EnableRealTimeScan();

        Task StartOnDemandScanAsync();
        void StopOnDemandScan();
        Task PublishUnsentNotifications();
    }
}