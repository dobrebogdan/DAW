using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;

namespace DAW.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

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

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Category category = dbContext.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        [Authorize(Roles = "Administrator")]
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

        [Authorize(Roles = "Administrator")]
        public ActionResult New()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        [Authorize(Roles = "Administrator")]
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
            var subjects = from sub in category.Subjects select sub;

            ViewBag.Category = category;
            ViewBag.Subjects = subjects;

            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        public ActionResult AddSubject(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = dbContext.Categories.Find(id);
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}