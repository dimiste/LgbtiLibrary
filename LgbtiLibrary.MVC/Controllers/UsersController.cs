﻿using LgbtiLibrary.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LgbtiLibrary.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        private static bool IsAdmin = false;

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
            IsAdmin = false;

            if (user == null)
            {
                return HttpNotFound();
            }


            var roles = user.Roles.Where(r => r.UserId == id);

            foreach (var role in roles)
            {
                if (role.RoleId == "1")
                {
                    IsAdmin = true;
                    this.ViewBag.IsAdmin = IsAdmin;
                }
            }

            

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,UserName")] ApplicationUser userPosted, bool AdminPosted)
        {

            var user = db.Users.Find(userPosted.Id);

            if (!string.IsNullOrWhiteSpace(userPosted.PhoneNumber))
            {
                user.PhoneNumber = userPosted.PhoneNumber.Trim();
            }

            user.UserName = userPosted.UserName.Trim();


            if (AdminPosted == true && IsAdmin == false)
            {
                var adminRoleToAdd = new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { RoleId = "1", UserId = user.Id };
                var userRoleToRemove = user.Roles.Where(r => r.RoleId == "2").First();

                user.Roles.Add(adminRoleToAdd);
                user.Roles.Remove(userRoleToRemove);
            }

            if (AdminPosted == false && IsAdmin == true)
            {
                var userRoleToAdd = new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { RoleId = "2", UserId = user.Id };
                var adminRoleToRemove = user.Roles.Where(r => r.RoleId == "1").First();
                
                user.Roles.Add(userRoleToAdd);
                user.Roles.Remove(adminRoleToRemove);
            }



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