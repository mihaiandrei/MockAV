using AvService.Domain;
using AvService.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AvService
{
    public class ScanHub : Hub<IScanHubClient>, IScanHubServer
    {
        private readonly IScannerService scannerService;
        private readonly IConnectedClientManager connectedClientManager;

        public ScanHub(IScannerService scannerManager,
                       IConnectedClientManager connectedClientManager)
        {
            this.scannerService = scannerManager;
            this.connectedClientManager = connectedClientManager;
        }

        public async Task StartOnDemandScan()
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

        public void Disconnect()
        {
            scannerService.Disconect(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            connectedClientManager.Disconect(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}