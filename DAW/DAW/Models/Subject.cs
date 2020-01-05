﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Subject
    {
        public Subject() { }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Subiectul trebuie sa aiba titlu")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subiectul trebuie sa aiba continut")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}