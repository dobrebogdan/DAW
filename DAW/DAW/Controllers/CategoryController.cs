using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAW.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {

            List<Category> categories = new List<Category>();
            categories.Add(new Category("Categoria 1"));
            categories.Add(new Category("Categoria 2"));
            categories.Add(new Category("Categoria 3"));
            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult Show(int id)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category("Categoria 1"));
            categories.Add(new Category("Categoria 2"));
            categories.Add(new Category("Categoria 3"));
            Category category = categories.ElementAt(id);
            category.Subjects = new List<Subject>();
            category.Subjects.Add(new Subject("Subiect 1", "subiect 1"));
            category.Subjects.Add(new Subject("Subiect 2", "subiect 2"));
            ViewBag.Category = category;

            return View();
        }
    }
}