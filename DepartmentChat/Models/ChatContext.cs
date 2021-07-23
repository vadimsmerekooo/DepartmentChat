using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentChat.Models
{
    public partial class ChatContext : DbContext
    {
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<ContentMessages> ContentMessages { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
