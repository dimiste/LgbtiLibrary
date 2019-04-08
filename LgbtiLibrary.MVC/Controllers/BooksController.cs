using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LgbtiLibrary.MVC.Models;
using PagedList;

namespace LgbtiLibrary.MVC.Controllers
{
    public class BooksController : Controller
    {
        private LgbtiLibraryDb db = new LgbtiLibraryDb();

        // GET: Books
        public ActionResult Index()
        {
            this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return this.View();
        }

        public ActionResult GetBooks(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(db.Books.OrderBy(b => b.BookId).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MyIndex()
        {
            return View(db.Books.Include(b => b.Author).ToList());
        }

        public ActionResult CategorySearch(string CategoryId, string sortOrder, string currentFilter, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var result = db.Books.OrderBy(b => b.BookId).Where(b => b.Category.CategoryId.ToString() == CategoryId).ToPagedList(pageNumber, pageSize);
            return View("_GetBooksByCategory", result);
        }

        // GET: Books/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        // GET: Books/Create
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "BookId,Title,Description,UrlBook,UrlImage")] Book bookPost, FormCollection formCollection)
        {
            bookPost.BookId = Guid.NewGuid();

            Author author;
            Category category;
            HttpPostedFileBase fileBook = null;
            HttpPostedFileBase fileImg = null;

            bookPost.Description = null;

            var authorId = formCollection["AuthorId"];
            var categoryId = formCollection["CategoryId"];

            foreach (string item in Request.Files)
            {

                HttpPostedFileBase file = Request.Files[item];

                if (file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);

                    string path = string.Empty;

                    if (extension.ToLower() != ".pdf")
                    {
                        path = "../imgs/" + bookPost.BookId.ToString() + extension;
                        fileImg = file; 
                        bookPost.UrlImage = path;
                    }
                    else
                    {
                        path = "../Files/" + bookPost.BookId.ToString() + extension;
                        fileBook = file;
                        bookPost.UrlBook = path;
                        ModelState.Remove("UrlBook");
                    }
                }
            }

            if (!string.IsNullOrEmpty(authorId))
            {
                author = db.Authors.Where(a => a.AuthorId.ToString() == authorId).Single();
                bookPost.Author = author;
                ModelState.Remove("Author");
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                category = db.Categories.Where(c => c.CategoryId.ToString() == categoryId).Single();
                bookPost.Category = category;
                ModelState.Remove("Category");
            }


            //if (bookPost.UrlBook == null)
            //{
            //    ModelState.AddModelError("authorAndCategory", "Author and Category is requierds");
            //}

            if (ModelState.IsValid)
            {
                if (fileBook != null)
                {
                    fileBook.SaveAs(this.Server.MapPath(bookPost.UrlBook));
                }

                if (fileImg != null)
                {
                    fileImg.SaveAs(this.Server.MapPath(bookPost.UrlImage));
                }
                else
                {
                    bookPost.UrlImage = "../imgs/libros.jpg";
                }

                db.Books.Add(bookPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (bookPost.Author != null)
            {
                this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", bookPost.Author.AuthorId);
            }
            else
            {
                this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            }

            if (bookPost.Category != null)
            {
                this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", bookPost.Category.CategoryId);
            }
            else
            {
                this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            }

            return View();
        }


        // GET: Books/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "BookId,Title,Description,UrlBook,UrlImage")] Book bookPost, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                Book book = db.Books.Where(b => b.BookId == bookPost.BookId).Single();



                var authorId = formCollection["AuthorId"];

                if (!string.IsNullOrEmpty(authorId))
                {
                    Author author = db.Authors.Where(a => a.AuthorId.ToString() == authorId).Single();

                    book.Author = author;
                }


                foreach (string item in Request.Files)
                {

                    HttpPostedFileBase file = Request.Files[item];
                    string extension = Path.GetExtension(file.FileName);

                    if (!string.IsNullOrEmpty(extension))
                    {
                        if (extension.ToLower() != ".pdf")
                        {
                            path = "../imgs/" + Guid.NewGuid().ToString() + extension;
                            file.SaveAs(this.Server.MapPath("../" + path));
                            book.UrlImage = path;
                        }
                        else
                        {
                            path = "../Files/" + Guid.NewGuid().ToString() + extension;
                            file.SaveAs(this.Server.MapPath("../" + path));
                            book.UrlBook = path;
                        }
                    }



                }

                //this.db.Entry(book).State = EntityState.Detached;
                //this.db.Entry(author).State = EntityState.Detached;

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookPost);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
