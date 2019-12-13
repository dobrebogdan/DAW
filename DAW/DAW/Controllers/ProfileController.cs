using DAW.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public ActionResult ShowByUser(string userId)
        {
            ApplicationUser user = dbContext.Users.Find(userId);
            var profiles = from prf in dbContext.Profiles where user.Id == prf.UserId select prf;
            return RedirectToAction("Show", new { id = profiles.First().Id });
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
            var userRoleId = profile.User.Roles.FirstOrDefault().RoleId;
            ViewBag.Profile = profile;
            ViewBag.UserRole = userRoleId;
            ViewBag.AllRoles = GetAllRoles();
            return View();
        }

        [Authorize(Roles = "Administrator,Moderator,User")]
        [HttpPut]
        public ActionResult Edit(int id, Profile updatedProfile)
        {
            try
            {
                ApplicationDbContext localDbContext = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(localDbContext));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(localDbContext));
                Profile profile = dbContext.Profiles.Find(id);
                if (TryUpdateModel(profile))
                {
                    profile.Description = updatedProfile.Description;
                    dbContext.SaveChanges();
                }

                var roleId = profile.User.Roles.FirstOrDefault().RoleId;
                IdentityRole userRole = dbContext.Roles.Find(roleId);
                var selectedRole = dbContext.Roles.Find(HttpContext.Request.Params.Get("updatedRole"));

                ApplicationUser user = profile.User;
                userManager.RemoveFromRole(user.Id, userRole.Name);
                userManager.AddToRole(user.Id, selectedRole.Name);

                return RedirectToAction("Show", new { id = id });
            } catch (Exception e)
            {
                return View();
            }
        }
    }
}