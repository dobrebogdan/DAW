using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public Message(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}