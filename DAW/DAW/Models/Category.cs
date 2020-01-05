using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Category
    {
        public Category() { }

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrierea categoriei este obligatorie")]
        public string Description { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}