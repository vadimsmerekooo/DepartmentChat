using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentChat.Models;
using Microsoft.AspNetCore.Identity;

namespace DepartmentChat.Areas.Identity.Data
{
    public class DepartmentUser : IdentityUser
    {
        public byte[] Icon { get; set; }
        public int UserChatId { get; set; }


    }
}
