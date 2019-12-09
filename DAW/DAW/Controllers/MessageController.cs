using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAW.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            Message message = dbContext.Messages.Find(id);
            ViewBag.Message = message;
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        public ActionResult Edit(int id)
        {
            Message message = dbContext.Messages.Find(id);
            ViewBag.Message = message;
            ViewBag.Subject = message.Subject;
            ViewBag.Subjects = from sub in message.Subject.Category.Subjects select sub;
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        [HttpPut]
        public ActionResult Edit(int id, Message updatedMessage)
        {
            try
            {
                Message message = dbContext.Messages.Find(id);
                if (TryUpdateModel(message))
                {
                    message.Content = updatedMessage.Content;
                    message.SubjectId = updatedMessage.SubjectId;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Show", new { id = id });
            } catch (Exception e)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Message message = dbContext.Messages.Find(id);
            int subjectId = message.SubjectId;

            dbContext.Messages.Remove(message);
            dbContext.SaveChanges();

            return RedirectToAction("Show", "Subject", new { id = subjectId });
        }

        public ActionResult New()
        {
            return View();
        }
    }
}