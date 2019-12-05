using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAW.Models
{
    public class Category
    {
        public Category(int id, string name)
        {
            CategoryId = id;
            CategoryName = name;
            Subjects = new List<Subject>();
        }
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string CategoryName { get; set; }

        public List <Subject> Subjects { get; set; }
    }
}