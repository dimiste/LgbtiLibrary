using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace LgbtiLibrary.MVC.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "БЛАГОДАРЯ! БЛАГОДАРЯ! БЛАГОДАРЯ!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "За да контактувате с нас имате следните методи:";

            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase filePost)
        {
            foreach (string upload in Request.Files)
            {
                if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;

                HttpPostedFileBase file = Request.Files[upload];

                if (ModelState.IsValid)
                {
                    if (file == null)
                    {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    }
                    else if (file.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 3; //3 MB
                        string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                        if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                        {
                            ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                        }

                        else if (file.ContentLength > MaxContentLength)
                        {
                            ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        }
                        else
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
                            file.SaveAs(path);
                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully";
                        }
                    }
                }
            }
            return View();
        }
    }
}