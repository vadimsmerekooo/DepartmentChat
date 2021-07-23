using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DepartmentChat.Services
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string room, bool join)
        {
            if (join)
            {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", room, user, user + " присоеденился к комнате " + room, DateTime.Now.ToShortTimeString()).ConfigureAwait(true);

            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", room, user, message, DateTime.Now.ToShortTimeString()).ConfigureAwait(true);

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
