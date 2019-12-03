using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public Subject(int id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }
        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(20, ErrorMessage = "Titlul nu poate avea mai mult de 20 caractere")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Continutul articolului este obligatoriu")]
        public string Content { get; set; }


    }
}