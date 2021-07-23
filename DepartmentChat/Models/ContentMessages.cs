using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentChat.Models
{
    public class ContentMessages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }

        public int MessageId { get; set; }
        public Messages Message { get; set; }
    }
}
