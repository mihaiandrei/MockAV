using AvService.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain
{
    public interface IScanner
    {
        Task<IEnumerable<InfectedObject>> ScanAsync(CancellationToken cancellationToken);
    }
}