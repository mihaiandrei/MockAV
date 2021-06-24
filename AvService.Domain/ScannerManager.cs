using AvService.Domain.Notifications;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class ScannerManager : IScannerManager
    {
        IScanner scanner;
        private INotifier notifier;


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
            if (notifier.ScanInProgress)
            {
                return false;
            }
            else
            {
                scanner.StartAsync();
                await notifier.SendAsync(new StartScanOnDemandNotification());
                return true;
            }
        }

        public async Task StopOnDemandScanAsync()
        {
            scanner.Stop();
            await notifier.SendAsync(new Notification());
        }
    }
}