using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;

namespace DepartmentChat.Services
{
    public class ChatHub : Hub
    {
        //public void Send( string message)
        //{
        //    Clients.All.SendAsync("Send", message);
        //}
        public ChatHub()
        {
            Join();
        }

        public Task Join()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "foo");
        }

        public async Task Send(string message)
        {
            await Clients.Group("foo").SendAsync("Send", Context.User.Identity.Name + " joined.");
        }
    }
}
