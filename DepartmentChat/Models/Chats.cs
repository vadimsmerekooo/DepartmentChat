using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentChat.Areas.Identity.Data;

namespace DepartmentChat.Models
{
    public class Chats
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Messages> Messages { get; set; } 
        public int Url { get; set; }
        public List<AspNetUsers> AspNetUsers { get; set; } = new List<AspNetUsers>();
        public TypeChat TypeChat { get; set; }
        public Groups Groups { get; set; }
        

    }

    public enum TypeChat
    {
        Direct,
        Group
    }
}
