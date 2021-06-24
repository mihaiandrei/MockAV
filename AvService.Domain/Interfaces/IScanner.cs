using System.Threading.Tasks;

namespace AvService
{
    public interface IScanner
    {
        Task StartAsync();
        void Stop();
    }
}