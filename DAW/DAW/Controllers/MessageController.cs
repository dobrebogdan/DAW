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
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show(int id)
        {
            List<Message> messages = new List<Message>();
            messages.Add(new Message(0, "Continut mesaj 1"));
            messages.Add(new Message(1, "Continut mesaj 2"));
            ViewBag.Message = messages.ElementAt(id);
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
    }
}