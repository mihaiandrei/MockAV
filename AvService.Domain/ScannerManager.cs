using AvService.Domain.Notifications;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class ScannerManager : IScannerManager
    {
        private readonly IScanner scanner;
        private readonly INotifier notifier;

        private CancellationTokenSource source;
        private CancellationToken cancellationToken;
        private bool scanInProgress;

        public ScannerManager(IScanner scanner,
                                INotifier notifier)
        {
            this.scanner = scanner;
            this.notifier = notifier;

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
                await notifier.SendAsync(new StopScanOnDemandNotification());
            else
                await notifier.SendAsync(new StopScanSuccessNotification());

            if (infectedItems.Any())
                await notifier.SendAsync(new ThreatFoundNotification(infectedItems));

            scanInProgress = false;
        }

        public void StopOnDemandScan()
        {
            source.Cancel();
        }

    }
}