using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Subject
    {
        public Subject() { }

//        public Subject(int id, string title, string content)
//        {
//            Id = id;
//            Title = title;
//            Content = content;
//        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(20, ErrorMessage = "Titlul nu poate avea mai mult de 20 caractere")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Descrierea subiectului este obligatorie")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}