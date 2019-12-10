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

//        public Message(int id, string content)
//        {
//            Id = id;
//            Content = content;
//        }

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Content { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}