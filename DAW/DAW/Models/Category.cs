using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Category
    {
        public Category()
        {
//            Subjects = new List<Subject>();
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
//            Subjects = new List<Subject>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}