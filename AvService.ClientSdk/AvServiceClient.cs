using AvService.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace AvService.ClientSdk
{
    public class AvServiceClient : IAvServiceClient
    {
        HubConnection connection;
        private readonly INotificationsReceiver notificationsReceiver;

        public bool IsConnected => connection?.State == HubConnectionState.Connected;
        public bool IsConnecting => connection?.State == HubConnectionState.Connecting ||
                                    connection?.State == HubConnectionState.Reconnecting;


        public AvServiceClient(INotificationsReceiver notificationsReceiver)
        {
            this.notificationsReceiver = notificationsReceiver;

            connection = new HubConnectionBuilder()
                               .WithAutomaticReconnect()
                               .WithUrl("http://localhost:5000/scanhub")
                               .Build();

            connection.On<StartScanOnDemandNotification>(nameof(IScanHubClient.SendStartScanOnDemandNotification), notification =>
            {
                notificationsReceiver?.ReceiveNotification(notification);
            });

            connection.On<StopScanSuccessNotification>(nameof(IScanHubClient.SendStopScanSuccessNotification), notification =>
            {
                notificationsReceiver?.ReceiveNotification(notification);
            });

            connection.On<StopScanOnDemandNotification>(nameof(IScanHubClient.SendStopScanOnDemandNotification), notification =>
            {
                notificationsReceiver?.ReceiveNotification(notification);
            });

            connection.On<ThreatFoundNotification>(nameof(IScanHubClient.SendThreatFoundNotification), notification =>
            {
                notificationsReceiver?.ReceiveNotification(notification);
            });

            connection.On<ScanInProgressNotification>(nameof(IScanHubClient.SendScanInProgressNotification), notification =>
            {
                notificationsReceiver?.ReceiveNotification(notification);
            });

            connection.On(nameof(IScanHubClient.DisconnectClient), async () =>
             {
                 await connection.StopAsync();
             });
        }

        public async Task Connect()
        {
            await connection.StartAsync();
            await connection.InvokeAsync(nameof(IScanHubServer.Connect));
        }

        public async Task DisableRealTimeScan()
        {
            await connection.InvokeAsync(nameof(IScanHubServer.DisableRealTimeScan));
        }

        public async Task Disconect()
        {
            await connection.InvokeAsync(nameof(IScanHubServer.Disconnect));
            await connection.StopAsync();
        }

        public async Task EnableRealTimeScan()
        {
            await connection.InvokeAsync(nameof(IScanHubServer.EnableRealTimeScan));
        }

        public async Task PublishUnsentNotifications()
        {
            await connection.InvokeAsync(nameof(IScanHubServer.PublishUnsentNotifications));
        }

        public async Task StartOnDemandScanAsync()
        {
            await connection.InvokeAsync(nameof(IScanHubServer.StartOnDemandScan));
        }

        public async Task StopOnDemandScan()
        {
            await connection.InvokeAsync(nameof(IScanHubServer.StopOnDemandScan));
        }
    }
}
