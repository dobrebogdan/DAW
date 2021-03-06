﻿using DAW.Models;
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
        private int _perPage = 3;
        public ActionResult Index()
        {
            var categories = from category in dbContext.Categories
                             orderby category.Name
                             select category;

            ViewBag.Categories = categories;
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.IsUserAdmin = User.IsInRole("Administrator");
            return View();
        }

        [HttpPost]
        public ActionResult Index(String searchString)
        {
            var categories = from category in dbContext.Categories
                             orderby category.Name
                             select category;

            ViewBag.Categories = categories;
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.IsUserAdmin = User.IsInRole("Administrator");
            return View();
        }

        public ActionResult IndexBySubjects()
        {
            var categories = from category in dbContext.Categories
                             orderby -(category.Subjects.Count)
                             select category;
            ViewBag.Categories = categories;
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.IsUserAdmin = User.IsInRole("Administrator");
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
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(category))
                    {
                        category.Name = updatedCategory.Name;
                        category.Description = updatedCategory.Description;
                        dbContext.SaveChanges();
                    }
                
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Category = updatedCategory;
                    return View();
                }

            } catch (Exception e)
            {
                ViewBag.Category = updatedCategory;
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
                if (ModelState.IsValid)
                {
                    dbContext.Categories.Add(category);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.UserId = category.UserId;
                    return View();
                }
            } catch(Exception e)
            {
                ViewBag.UserId = category.UserId;
                return View();
            }
        }

        public ActionResult Show(int id)
        {
            Category category = dbContext.Categories.Find(id);
            var subjects = from sub in category.Subjects
                           orderby sub.Title
                           select sub;

            var totalItems = subjects.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedSubjects = subjects.Skip(offset).Take(this._perPage);

            ViewBag.Category = category;
            ViewBag.Subjects = paginatedSubjects;
            ViewBag.Page = currentPage;
            ViewBag.LastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.ShowType = "show";
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.IsUserPrivileged = User.IsInRole("Administrator") || User.IsInRole("Moderator");
            return View();
        }

        public ActionResult ShowByMessages(int id)
        {
            Category category = dbContext.Categories.Find(id);
            var subjects = from sub in category.Subjects
                           orderby -(sub.Messages.Count)
                           select sub;

            var totalItems = subjects.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;


            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedSubjects = subjects.Skip(offset).Take(this._perPage);

            ViewBag.Category = category;
            ViewBag.Subjects = paginatedSubjects;
            ViewBag.Page = currentPage;
            ViewBag.LastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.ShowType = "showbymessages";
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.IsUserPrivileged = User.IsInRole("Administrator") || User.IsInRole("Moderator");            return View("~/Views/Category/Show.cshtml");
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
                if (ModelState.IsValid)
                {
                    dbContext.Subjects.Add(subject);
                    dbContext.SaveChanges();
                    return RedirectToAction("Show", new { id = subject.CategoryId });
                }
                else
                {
                    ViewBag.CategoryId = subject.CategoryId;
                    ViewBag.UserId = subject.UserId;
                    return View();
                }
            } catch (Exception e)
            {
                ViewBag.CategoryId = subject.CategoryId;
                ViewBag.UserId = subject.UserId;
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
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