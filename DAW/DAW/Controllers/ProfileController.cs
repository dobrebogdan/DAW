using DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAW.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult New(string userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public ActionResult New(Profile profile)
        {
            try
            {
                dbContext.Profiles.Add(profile);
                dbContext.SaveChanges();
                return RedirectToAction("Show", new { id = profile.Id });
            } catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Show(int id)
        {
            Profile profile = dbContext.Profiles.Find(id);
            ViewBag.Profile = profile;
            ViewBag.Messages = from user in dbContext.Users
                               where user.Id == profile.UserId
                               join msg in dbContext.Messages on user.Id equals msg.UserId
                               select msg;
            return View();
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllRoles()
        {
            var selectList = new List<SelectListItem>();

            var roles = from role in dbContext.Roles select role;
            foreach (var role in roles)
            {
                selectList.Add(new SelectListItem
                {
                    Value = role.Id,
                    Text = role.Name
                });
            }

            return selectList;
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        public ActionResult Edit(int id)
        {
            Profile profile = dbContext.Profiles.Find(id);
            ViewBag.Profile = profile;
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        [HttpPut]
        public ActionResult Edit(int id, Profile updatedProfile)
        {
            try
            {
                Profile profile = dbContext.Profiles.Find(id);
                if (TryUpdateModel(profile))
                {
                    profile.Description = updatedProfile.Description;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Show", new { id = id });
            } catch (Exception e)
            {
                return View();
            }
        }
    }
}