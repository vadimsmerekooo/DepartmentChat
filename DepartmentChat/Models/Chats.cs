using DepartmentChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentChat.Models
{
    public class Chats
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Messages> Messages { get; set; }
        [NotMapped]
        public List<string> Users { get; set; }
        public TypeChat TypeChat { get; set; } = TypeChat.Direct;
        public Groups Groups { get; set; }

    }

    public enum TypeChat
    {
        Group,
        Direct
    }
}
