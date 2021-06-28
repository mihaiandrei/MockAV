using AvService.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AvService
{
    public interface IScanner
    {
        Task<IEnumerable<InfectedObject>> ScanAsync(CancellationToken cancellationToken);
    }
}