using Reinforced.Typings.Attributes;
using System.Threading.Tasks;

namespace AvService.Shared
{
    [TsInterface]
    public interface IScanHubServer
    {
        void Connect();
        void Disconnect();

        void DisableRealTimeScan();
        void EnableRealTimeScan();

        Task StartOnDemandScan();
        void StopOnDemandScan();

        Task PublishUnsentNotifications();
    }
}