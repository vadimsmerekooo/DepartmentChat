using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentChat.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepartmentChat.Data
{
    public class ChatIdentityContext : IdentityDbContext<DepartmentUser>
    {
        public ChatIdentityContext(DbContextOptions<ChatIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
