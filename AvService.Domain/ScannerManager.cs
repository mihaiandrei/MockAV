using AvService.Domain.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class ScannerManager : IScannerManager
    {
        IScanner scanner;
        private INotifier notifier;
        CancellationTokenSource source;
        CancellationToken cancellationToken;
        private bool scanInProgress;

        bool RealTimeScanEnabled { get; set; } = true;

        public ScannerManager(IScanner scanner, INotifier notifier)
        {
            this.scanner = scanner;
            this.notifier = notifier;
        }

        public void EnableRealTimeScan()
        {
            RealTimeScanEnabled = true;
        }

        public void DisableRealTimeScan()
        {
            RealTimeScanEnabled = false;
        }

        public async Task<bool> StartOnDemandScanAsync()
        {
            if (scanInProgress)
            {
                return false;
            }
            else
            {
                scanInProgress = true;
                source = new CancellationTokenSource();
                cancellationToken = source.Token;

                await notifier.SendAsync(new StartScanOnDemandNotification());

                Scan();
            }
            return true;
        }

        private async Task Scan()
        {
            var infectedItems = await scanner.ScanAsync(cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                await notifier.SendAsync(new StopScanOnDemandNotification(infectedItems));
            else
                await notifier.SendAsync(new StopScanSuccessNotification(infectedItems));

            scanInProgress = false;
        }

        public void StopOnDemandScan()
        {
            source.Cancel();
        }
    }
}