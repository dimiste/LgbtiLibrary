using LgbtiLibrary.Data.Data;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.MVC.Common;
using LgbtiLibrary.MVC.Common.Contracts;
using LgbtiLibrary.MVC.Models;
using LgbtiLibrary.Services.Contracts;
using LgbtiLibrary.Services.Data;

using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LgbtiLibrary.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private LgbtiLibraryDb db = new LgbtiLibraryDb();

        private readonly BookElementService bookElementService;

        public CategoriesController()
        {
            this.bookElementService = new BookElementService(new EFRepository<BookElement>(this.db));
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(this.bookElementService.GettAllCategories().Select(BookElementViewModel.Create));
        }

        // GET: Categories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBookElementModel bookElementModel = this.bookElementService.FindById(id);
            IBookElementViewModel bookElementViewModel = new BookElementViewModel(bookElementModel);

            if (bookElementViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bookElementViewModel);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookElementService.CreateBookElementWithNewGuid(Mapper.ToCategory(categoryViewModel));
                
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IBookElementModel bookElementModel = this.bookElementService.FindById(id);
            IBookElementViewModel bookElementViewModel = new BookElementViewModel(bookElementModel);

            if (bookElementViewModel == null)
            {
                return HttpNotFound();
            }

            return View(bookElementViewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] BookElementViewModel bookElementViewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookElementService.EditBookElement(Mapper.ToBookElement(bookElementViewModel));
                return RedirectToAction("Index");
            }
            return View(bookElementViewModel);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IBookElementModel bookElementModel = this.bookElementService.FindById(id);
            IBookElementViewModel bookElementViewModel = new BookElementViewModel(bookElementModel);

            if (bookElementViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bookElementViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.bookElementService.DeleteBookElement(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.bookElementService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
