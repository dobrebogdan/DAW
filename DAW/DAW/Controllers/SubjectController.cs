using DAW.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAW.Controllers
{
    public class SubjectController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        public ActionResult Edit(int id)
        {
            Subject subject = dbContext.Subjects.Find(id);
            ViewBag.Subject = subject;
            ViewBag.Categories = from category in dbContext.Categories select category;
            ViewBag.Category = subject.Category;
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        [HttpPut]
        public ActionResult Edit(int id, Subject updatedSubject)
        {
            try
            {
                Subject subject = dbContext.Subjects.Find(id);
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(subject))
                    {
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator") || User.Identity.GetUserId() == subject.UserId)
                        {
                            subject.Title = updatedSubject.Title;
                            subject.Content = updatedSubject.Content;
                            subject.CategoryId = updatedSubject.CategoryId;
                        }
                        dbContext.SaveChanges();
                    }
    
                    return RedirectToAction("Show", new { id = id });
                }
                else
                {
                    ViewBag.Subject = subject;
                    ViewBag.Categories = from category in dbContext.Categories select category;
                    ViewBag.Category = subject.Category;
                    return View();
                }
            } catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Show(int id)
        {
            Subject subject = dbContext.Subjects.Find(id);
            var messages = from msg in subject.Messages select msg;
            ViewBag.Subject = subject;
            ViewBag.Messages = messages;

            var user = System.Web.HttpContext.Current.User;
            ViewBag.UserId = user.Identity.GetUserId();
            ViewBag.IsUserPrivileged = true;

            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        public ActionResult AddMessage(int subjectId)
        {
            ViewBag.SubjectId = subjectId;
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        [HttpPost]
        public ActionResult AddMessage(Message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Messages.Add(message);
                    dbContext.SaveChanges();
                    return RedirectToAction("Show", new { id = message.SubjectId });
                }
                else
                {
                    ViewBag.SubjectId = message.SubjectId;
                    ViewBag.UserId = User.Identity.GetUserId();
                    return View();
                }
            } catch (Exception e)
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Subject subject = dbContext.Subjects.Find(id);
            int categoryId = subject.CategoryId;
            
            if (User.IsInRole("Administrator") || User.IsInRole("Moderator") || User.Identity.GetUserId() == subject.UserId)
            {
                dbContext.Subjects.Remove(subject);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Show", "Category", new { id = categoryId });
        }
    }
}