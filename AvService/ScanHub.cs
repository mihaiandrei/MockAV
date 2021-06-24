using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AvService
{
    public class ScanHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}