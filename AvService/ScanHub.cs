using AvService.Domain;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AvService
{
    public class ScanHub : Hub<IScanHubClient>, IServerScanHub
    {
        private readonly IScannerManager scannerManager;
        private readonly IRealTimeScanner realTimeScanner;
        private readonly IConnectedClientManager connectedClientManager;

        public ScanHub(IScannerManager scannerManager,
            IRealTimeScanner realTimeScanner,
            IConnectedClientManager connectedClientManager)
        {
            this.scannerManager = scannerManager;
            this.realTimeScanner = realTimeScanner;
            this.connectedClientManager = connectedClientManager;
        }

        public async Task StartOnDemandScanAsync()
        {
            await scannerManager.StartOnDemandScanAsync();
        }

        public async Task PublishUnsentNotifications()
        {
            await scannerManager.PublishUnsentNotifications();
        }

        public void StopOnDemandScan()
        {
            scannerManager.StopOnDemandScan();
        }

        public void EnableRealTimeScan()
        {
            realTimeScanner.EnableRealTimeScan();
        }

        public void DisableRealTimeScan()
        {
            realTimeScanner.DisableRealTimeScan();
        }

        public void Connect()
        {
            connectedClientManager.Connect();
        }

        public void Disconect()
        {
            connectedClientManager.Disconect();
        }
    }
}