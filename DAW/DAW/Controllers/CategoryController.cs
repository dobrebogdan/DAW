using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace DAW.Controllers
{
    public class CategoryController : Controller
    {
        private MessageDbContext dbContext = new MessageDbContext();
        
        public ActionResult Index()
        {
            var categories = from category in dbContext.Categories
                             orderby category.Name
                             select category;

            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Index(String searchString)
        {
            var categories = from category in dbContext.Categories
                             orderby category.Name
                             select category;

            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult IndexBySubjects()
        {
            var categories = from category in dbContext.Categories
                             orderby category.Subjects.Count
                             select category;

            ViewBag.Categories = categories;
            return View("~/Views/Category/Index.cshtml");
        }

        public ActionResult Edit(int id)
        {
            Category category = dbContext.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Category updatedCategory)
        {
            try
            {
                Category category = dbContext.Categories.Find(id);
                if (TryUpdateModel(category))
                {
                    category.Name = updatedCategory.Name;
                    category.Description = updatedCategory.Description;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            } catch (Exception e)
            {
                return View();
            }
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
            Category category = dbContext.Categories.Find(id);
            var subjects = from sub in category.Subjects
                           select sub;

            ViewBag.Category = category;
            ViewBag.Subjects = subjects;

            return View();
        }

        public ActionResult AddSubject(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            try
            {
                dbContext.Subjects.Add(subject);
                dbContext.SaveChanges();
                return RedirectToAction("Show", new { id = subject.CategoryId });
            } catch (Exception e)
            {
                return View();
            }
        }
    }
}