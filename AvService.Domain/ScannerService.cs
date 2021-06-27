using AvService.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AvService.Domain
{
    public class ScannerService : IScannerService
    {
        private readonly IScanner scanner;
        private readonly INotifier notifier;
        private readonly IConnectedClientManager connectedClientManager;

        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;
        private bool scanInProgress;
        System.Timers.Timer timer;

        public ScannerService(IScanner scanner,
                                INotifier notifier,
                                IConnectedClientManager connectedClientManager)
        {
            this.scanner = scanner;
            this.notifier = notifier;
            this.connectedClientManager = connectedClientManager;

            timer = new System.Timers.Timer();
            timer.Interval = 5000;

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

        }

        public async Task StartOnDemandScanAsync(string connectionId)
        {
            if (!connectedClientManager.ValidateConnection(connectionId))
                return;

            if (scanInProgress)
            {
                await notifier.SendAsync(new ScanInProgressNotification());
                return;
            }
            else
            {
                scanInProgress = true;
                cancellationTokenSource = new CancellationTokenSource();
                cancellationToken = cancellationTokenSource.Token;

                await notifier.SendAsync(new StartScanOnDemandNotification());
                Scan();
            }
        }

        public void StopOnDemandScan(string connectionId)
        {
            if (!connectedClientManager.ValidateConnection(connectionId))
                return;

            cancellationTokenSource.Cancel();
        }

        public async Task PublishUnsentNotifications(string connectionId)
        {
            if (!connectedClientManager.ValidateConnection(connectionId))
                return;

            await notifier.PushUnsentNotifications();
        }

        public void EnableRealTimeScan(string connectionId)
        {
            if (!connectedClientManager.ValidateConnection(connectionId))
                return;

            timer.Enabled = true;
        }

        public void DisableRealTimeScan(string connectionId)
        {
            if (!connectedClientManager.ValidateConnection(connectionId))
                return;

            timer.Enabled = false;
        }

        public async Task Connect(string connectionId)
        {
            var successfullyConnected = connectedClientManager.Connect(connectionId);

            if (!successfullyConnected)
                await notifier.DisconectClient(connectionId);
        }
        public void Disconect(string connectionId)
        {
            if (!connectedClientManager.ValidateConnection(connectionId))
                return;

            connectedClientManager.Disconect(connectionId);
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var random = new Random();
            var threatNumber = random.Next(0, 3);

            IEnumerable<InfectedObject> infectedItems = Enumerable.Range(0, threatNumber)
               .Select(i => new InfectedObject
               {
                   ThreatName = $"Threat {i}",
                   FilePath = $"C:\\file {i}"
               });

            if (infectedItems.Any())
                await notifier.SendAsync(new ThreatFoundNotification(infectedItems));
        }

        private async Task Scan()
        {
            var infectedItems = await scanner.ScanAsync(cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                await notifier.SendAsync(new StopScanOnDemandNotification());
            else
                await notifier.SendAsync(new StopScanSuccessNotification());

            if (infectedItems.Any())
                await notifier.SendAsync(new ThreatFoundNotification(infectedItems));

            scanInProgress = false;
        }
    }
}