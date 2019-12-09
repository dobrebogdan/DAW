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
        private MessageDbContext dbContext = new MessageDbContext();

        // GET: Message
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