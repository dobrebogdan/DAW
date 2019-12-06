using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace DAW.Controllers
{
    class CategoryNameComparer : IComparer<Category>
    {
        public int Compare(Category x, Category y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    class CategorySubjectComparer : IComparer<Category>
    {
        public int Compare(Category x, Category y)
        {
            return y.Subjects.Count - x.Subjects.Count;
        }
    }

    public class CategoryController : Controller
    {
        private MessageDbContext dbContext = new MessageDbContext();
        
        public ActionResult Index()
        {
//            List<Category> categories = new List<Category>();
//            categories.Add(new Category(0, "Categoria 1"));
//            categories.Add(new Category(1, "Categoria 2"));
//            categories.Add(new Category(2, "Categoria 3"));
//            ViewBag.Categories = categories;
//            return View();
            var categories = from category in dbContext.Categories
                             orderby category.Name
                             select category;

            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Index(String searchString)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category(0, "Categoria 1"));
            categories.Add(new Category(1, "Categoria 2"));
            categories.Add(new Category(2, "Categoria 3"));
            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult IndexByName()
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category(0, "Categoria 1"));
            categories.Add(new Category(1, "Categoria 2"));
            categories.Add(new Category(2, "Categoria 3"));
            categories.Sort(new CategoryNameComparer());
            ViewBag.Categories = categories;
            return View("~/Views/Category/Index.cshtml");
        }
        
        public ActionResult IndexBySubjects()
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category(0, "Categoria 1"));
            categories.Add(new Category(1, "Categoria 2"));
            categories.Add(new Category(2, "Categoria 3"));
            categories.ElementAt(2).Subjects.Add(new Subject(1, "Titlu", "Continut"));
            categories.Sort(new CategorySubjectComparer());
            ViewBag.Categories = categories;
            return View("~/Views/Category/Index.cshtml");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            } catch(Exception e)
            {
                return View();
            }
        }

        public ActionResult Show(int id)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category(0, "Categoria 1"));
            categories.Add(new Category(1, "Categoria 2"));
            categories.Add(new Category(2, "Categoria 3"));
            Category category = categories.ElementAt(id);
            category.Subjects = new List<Subject>();
            category.Subjects.Add(new Subject(0, "Subiect 1", "subiect 1"));
            category.Subjects.Add(new Subject(1, "Subiect 2", "subiect 2"));
            ViewBag.Category = category;

            return View();
        }
    }
}