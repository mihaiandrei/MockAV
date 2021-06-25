using AvService.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace AvService.Domain
{
    public class RealTimeScanner : IRealTimeScanner
    {
        private INotifier notifier;
        Timer timer;


        public RealTimeScanner(INotifier notifier)
        {
            this.notifier = notifier;
            timer = new Timer();
            timer.Interval = 5000;

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IEnumerable<InfectedObject> infectedItems = Enumerable.Empty<InfectedObject>();
            await notifier.SendAsync(new ThreatFoundNotification(infectedItems));
        }

        public void EnableRealTimeScan()
        {
            timer.Enabled = true;
        }

        public void DisableRealTimeScan()
        {
            timer.Enabled = false;
        }
    }
}
