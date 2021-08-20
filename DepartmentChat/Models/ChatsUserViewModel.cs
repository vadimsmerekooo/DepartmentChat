using System.Collections.Generic;

namespace DepartmentChat.Models
{
    public class ChatsUserViewModel
    {
        public AspNetUsers UserView { get; set; }
        public List<AspNetUsers> UsersList { get; set; } = new List<AspNetUsers>();
        public List<Chats> Chats { get; set; } = new List<Chats>();
        public Chats ViewModel { get; set; }
    }
}