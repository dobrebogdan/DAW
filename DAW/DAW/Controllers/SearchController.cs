using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAW.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Find(string searchedTerm)
        {
            if(searchedTerm == null)
            {
                searchedTerm = "";
            }
            var subjects = from subject in dbContext.Subjects
                             orderby subject.Title
                             select subject;
            var goodSubjects = new List<Subject>();

            foreach (var subject in subjects)
            {
                if (subject.Title.Contains(searchedTerm))
                {
                    goodSubjects.Add(subject);
                }
            }

            ViewBag.Subjects = goodSubjects;

            var messages = from message in dbContext.Messages
                           select message;

            var goodMessages = new List<Message>();

            foreach (var message in messages)
            {
                if (message.Content.Contains(searchedTerm))
                {
                    goodMessages.Add(message);
                }
            }
            ViewBag.Messages = goodMessages;
            return View();
        }
    }
}