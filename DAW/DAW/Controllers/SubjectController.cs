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
            subjects.Add(new Subject("Titlu 1", "Continut"));
            subjects.Add(new Subject("Titlu 2", "Continut"));
            ViewBag.Subjects = subjects;
            return View();
        }

        public ActionResult Show()
        {
            return View();
        }
    }
}