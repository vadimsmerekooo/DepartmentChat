using DepartmentChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentChat.Models
{
    public class Messages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }
        public AspNetUsers User { get; set; }

        public int ChatId { get; set; }
        public Chats Chat { get; set; }

        public ContentMessages ContentMessage { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
