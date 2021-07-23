using DepartmentChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentChat.Models
{
    public class Groups
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ChatsId { get; set; }
        public Chats Chats { get; set; }
        public string Purpose { get; set; }
        public string User { get; set; }
    }
}
