using LgbtiLibrary.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LgbtiLibrary.MVC.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: Users
        public ActionResult Index()
        {
            var usersWithRoles = (from user in db.Users
                                  from userRole in user.Roles
                                  join role in db.Roles on userRole.RoleId equals
                                  role.Id
                                  select new User()
                                  {
                                      UserId = user.Id,
                                      Name = user.UserName,
                                      Role = role.Name
                                  }).ToList();

            return View(usersWithRoles);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);

            this.ViewBag.IsAdmin = false;


            if (user == null)
            {
                return HttpNotFound();
            }

            //db.Users.Join(db.Roles, u => u.UserName, r => r.Name, (u, r) => new User() { Name = u.UserName, Role = r.Name});

            var roles = user.Roles.Where(r => r.UserId == id);

            foreach (var role in roles)
            {
                if (role.RoleId == "1")
                {
                    this.ViewBag.IsAdmin = true;
                }
            }

            

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,UserName")] ApplicationUser userPosted)
        {
            var user = db.Users.Find(userPosted.Id);
            user.PhoneNumber = userPosted.PhoneNumber.Trim();
            user.UserName = userPosted.UserName.Trim();

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}