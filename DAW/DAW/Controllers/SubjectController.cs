using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAW.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            List<Subject> subjects = new List<Subject>();
            subjects.Add(new Subject(0, "Titlu 1", "Continut"));
            subjects.Add(new Subject(1, "Titlu 2", "Continut"));
            ViewBag.Subjects = subjects;
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            List<Subject> subjects = new List<Subject>();
            subjects.Add(new Subject(0, "Titlu 1", "Continut"));
            subjects.Add(new Subject(1, "Titlu 2", "Continut"));
            ViewBag.Subject = subjects.ElementAt(id);

            List<Message> messages = new List<Message>();
            messages.Add(new Message(0, "Continut mesaj 1"));
            messages.Add(new Message(1, "Continut mesaj 2"));
            ViewBag.Messages = messages; 
            return View();
        }
    }
}