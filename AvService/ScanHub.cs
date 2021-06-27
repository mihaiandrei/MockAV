using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AvService
{
    public class ScanHub : Hub<IScanHubClient>, IServerScanHub
    {
        private readonly IScannerService scannerService;

        public ScanHub(IScannerService scannerManager)
        {
            this.scannerService = scannerManager;
        }

        public async Task StartOnDemandScanAsync()
        {
            await scannerService.StartOnDemandScanAsync(Context.ConnectionId);
        }

        public async Task PublishUnsentNotifications()
        {
            await scannerService.PublishUnsentNotifications(Context.ConnectionId);
        }

        public void StopOnDemandScan()
        {
            scannerService.StopOnDemandScan(Context.ConnectionId);
        }

        public void EnableRealTimeScan()
        {
            scannerService.EnableRealTimeScan(Context.ConnectionId);
        }

        public void DisableRealTimeScan()
        {
            scannerService.DisableRealTimeScan(Context.ConnectionId);
        }

        public void Connect()
        {
            scannerService.Connect(Context.ConnectionId);
        }

        public void Disconect()
        {
            scannerService.Disconect(Context.ConnectionId);
        }
    }
}