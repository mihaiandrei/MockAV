using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class ScannerManager : IScannerManager
    {
        IScanner scanner;
        private INotifier notifier;

        public bool ScanInProgress { get; set; }
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

        public async Task StartOnDemandScanAsync()
        {
            if (ScanInProgress)
            {
                await notifier.SendAsync(new Notification());
            }
            else
            {
                scanner.StartAsync();
                await notifier.SendAsync(new Notification());
            }
        }

        public async Task StopOnDemandScanAsync()
        {
            scanner.Stop();
            await notifier.SendAsync(new Notification());
        }
    }
}