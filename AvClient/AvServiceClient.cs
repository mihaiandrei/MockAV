using AvService;
using AvService.Domain.Notifications;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace AVClient
{
    public class AvServiceClient
    {
        HubConnection connection;
        private readonly INotificationsReceiver notificationsReceiver;

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
        }

        public async Task Connect()
        {
            await connection.StartAsync();
            await connection.InvokeAsync(nameof(IServerScanHub.Connect));
        }

        public async Task DisableRealTimeScan()
        {
            await connection.InvokeAsync(nameof(IServerScanHub.DisableRealTimeScan));
        }

        public async Task Disconect()
        {
            await connection.InvokeAsync(nameof(IServerScanHub.Disconect));
            await connection.StopAsync();
        }

        public async Task EnableRealTimeScan()
        {
            await connection.InvokeAsync(nameof(IServerScanHub.EnableRealTimeScan));
        }

        public async Task PublishUnsentNotifications()
        {
            await connection.InvokeAsync(nameof(IServerScanHub.PublishUnsentNotifications));
        }

        public async Task StartOnDemandScanAsync()
        {
            await connection.InvokeAsync(nameof(IServerScanHub.StartOnDemandScanAsync));
        }

        public async Task StopOnDemandScan()
        {
            await connection.InvokeAsync(nameof(IServerScanHub.StopOnDemandScan));
        }
    }
}
