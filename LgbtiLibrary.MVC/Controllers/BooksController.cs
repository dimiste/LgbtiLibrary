using LgbtiLibrary.Data.Data;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.Services.Data;
using PagedList;

using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LgbtiLibrary.MVC.Controllers
{
    public class BooksController : Controller
    {
        private LgbtiLibraryDb db = new LgbtiLibraryDb();

        private BookService bookService;

        public BooksController()
        {
            this.ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            this.ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name");

            this.bookService = new BookService(new EFRepository<Book>(db));
        }

        // GET: Books
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetBooks(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //int pageSize = 4;
            //int pageNumber = (page ?? 1);
            //return View(db.Books.OrderBy(b => b.BookId).ToPagedList(pageNumber, pageSize));

            return View(this.bookService.ToPagedList(sortOrder, currentFilter, searchString, page, 4));
        }

        public ActionResult CategorySearch(string CategoryId, string sortOrder, string currentFilter, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var result = db.Books.OrderBy(b => b.BookId).Where(b => b.Category.Id.ToString() == CategoryId).ToPagedList(pageNumber, pageSize);
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
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
                author = db.Authors.Where(a => a.Id.ToString() == authorId).Single();
                bookPost.Author = author;
                ModelState.Remove("Author");
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                category = db.Categories.Where(c => c.Id.ToString() == categoryId).Single();
                bookPost.Category = category;
                ModelState.Remove("Category");
            }


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
                this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", bookPost.Author.Id);
            }
            else
            {
                this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            }

            if (bookPost.Category != null)
            {
                this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", bookPost.Category.Id);
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
            this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.Author.Id);
            this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", book.Category.Id);
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
            Book book = db.Books.Where(b => b.BookId == bookPost.BookId).Single();
            book.Title = bookPost.Title;
            book.Description = bookPost.Description;

            Author author = null;
            Category category = null;

            ModelState.Remove("UrlBook");

            var authorId = formCollection["AuthorId"];
            var categoryId = formCollection["CategoryId"];

            if (!string.IsNullOrEmpty(authorId))
            {
                ModelState.Remove("Author");
                author = db.Authors.Where(a => a.Id.ToString() == authorId).Single();

                book.Author = author;
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                ModelState.Remove("Category");
                category = db.Categories.Where(a => a.Id.ToString() == categoryId).Single();

                book.Category = category;
            }

            if (ModelState.IsValid)
            {
                string path = string.Empty;

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

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (author != null)
            {
                this.ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", author.Id);
            }


            if (category != null)
            {
                this.ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", category.Id);
            }

            return View(book);
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
