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
            var scanDuration = random.Next(10, 30);


            await Task.Delay(TimeSpan.FromSeconds(scanDuration), cancellationToken);
            var threatNumber = random.Next(0, 3);

            return Enumerable.Range(0, threatNumber)
               .Select(i => new InfectedObject
               {
                   ThreatName = $"Threat {i}",
                   FilePath = $"C:\\file {i}"
               });
        }
    }
}