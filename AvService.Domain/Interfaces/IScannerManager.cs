using System.Threading.Tasks;

namespace AvService
{
    public interface IScannerManager
    {
        Task StartOnDemandScanAsync();
        void StopOnDemandScan();

        Task PublishUnsentNotifications();
    }
}