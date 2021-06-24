using AvService.Domain.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class Scanner : IScanner
    {
        private INotifier notifier;
        CancellationTokenSource source;
        CancellationToken cancellationToken;

        public Scanner(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task StartAsync()
        {
            source = new CancellationTokenSource();
            cancellationToken = source.Token;

            var random = new Random();
            var scanDuration = random.Next(0, 3);

            await Task.Delay(scanDuration, cancellationToken);
            await notifier.SendAsync(new Notification());
        }

        public void Stop()
        {
            source.Cancel();
        }
    }
}