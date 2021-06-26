using System.Threading.Tasks;

namespace AvService
{
    public interface IScannerManager
    {
        Task<bool> StartOnDemandScanAsync();
        void StopOnDemandScan();

        Task PublishUnsentNotifications();
    }
}