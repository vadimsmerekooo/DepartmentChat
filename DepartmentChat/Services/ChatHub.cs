using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using DepartmentChat.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartmentChat.Services
{
    public class ChatHub : Hub
    {
        private ChatContext _context = new ChatContext();
        public async Task SendMessage(string user, string message, string room, bool join)
        {
            AspNetUsers userMessage = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == user);
            Chats chat = await _context.Chats.FirstOrDefaultAsync(c => c.Url == int.Parse(room));
            await _context.Entry(chat).Collection(m => m.Messages).LoadAsync();
            Messages messages = new Messages()
            {
                User = userMessage,
                ContentMessage = new ContentMessages()
                {
                    Content = message.Trim()
                }
            };
            chat.Messages.Add(messages);
            _context.Update(chat);
            await _context.SaveChangesAsync();


            string iconBlock = String.Empty;
            string userName = userMessage.Email.Split(' ')[0];

            if (userMessage.Icon is null)
            {
                iconBlock = "<span class=\"avatar-text\">" + userMessage.Email.First() + "</span>";
            }
            else
            {
                iconBlock = "<img class=\"avatar-img\" src=\"data:image/png;base64," + @System.Convert.ToBase64String(userMessage.Icon) + ">";
            }
            await Clients.All.SendAsync("ReceiveMessage", room, user, userName, iconBlock, message.Trim(), DateTime.Now.ToShortTimeString())
                .ConfigureAwait(true);
                
            
        }

        public async Task JoinRoom(string roomName, AspNetUsers user)
        {
            if (_context.Chats.Any(c => c.Url == int.Parse(roomName)))
            {
                // Chats chat = await _context.Chats.FirstOrDefaultAsync(c => c.Url == int.Parse(roomName));
                // chat.AspNetUsers.Add(user);
                // _context.Chats.Update(chat);
                // await _context.SaveChangesAsync();
            }
            else
            {
                Chats chat = new Chats()
                {
                    Url = new Random().Next(int.MaxValue),
                    AspNetUsers = new List<AspNetUsers>() {user}
                };
                await _context.Chats.AddAsync(chat);
                await _context.SaveChangesAsync();
            }
            
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
