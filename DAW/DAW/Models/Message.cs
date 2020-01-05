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

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Mesajul trebuie sa aiba continut")]
        public string Content { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}