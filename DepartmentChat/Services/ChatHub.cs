using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;

namespace DepartmentChat.Services
{
    public class ChatHub : Hub
    {
        public void Send( string message)
        {
            Clients.All.SendAsync("Send", message);
        }
    }
}
