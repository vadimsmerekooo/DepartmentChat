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

        public async Task SendMessage(string user, string message, string room, bool join)
        {
            if (join)
            {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", user, " присоеденился к комнате " + room).ConfigureAwait(true);

            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", user, message).ConfigureAwait(true);

            }
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
