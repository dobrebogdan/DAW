using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Message
    {
        public Message() { }

        public Message(int id, string content)
        {
            Id = id;
            Content = content;
        }

        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual Subject Subject { get; set; }
    }

    public class MessageDbContext : DbContext
    {
        public MessageDbContext() : base("DBConnectionString") { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}