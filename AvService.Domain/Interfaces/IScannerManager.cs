using System.Threading.Tasks;

namespace AvService
{
    public interface IScannerManager
    {
        void DisableRealTimeScan();
        void EnableRealTimeScan();
        Task<bool> StartOnDemandScanAsync();
        void StopOnDemandScan();
    }
}