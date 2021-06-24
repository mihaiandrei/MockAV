using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public class Scanner : IScanner
    {
        public async Task<IEnumerable<InfectedObject>> ScanAsync(CancellationToken cancellationToken)
        {

            var random = new Random();
            var scanDuration = random.Next(0, 3);

            await Task.Delay(scanDuration, cancellationToken);

            return Enumerable.Empty<InfectedObject>();
        }
    }
}