using LgbtiLibrary.Data.Data;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.MVC.Models;
using LgbtiLibrary.Services.Contracts;
using LgbtiLibrary.Services.Data;
using LgbtiLibrary.Services.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LgbtiLibrary.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private LgbtiLibraryDb db = new LgbtiLibraryDb();

        private readonly ICategoryService categoryService;

        public CategoriesController()
        {
            this.categoryService = new CategoryService(new EFRepository<Category>(this.db));
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(this.categoryService.GettAll().Select(CategoryViewModel.Create));
        }

        // GET: Categories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryModel categoryModel = this.categoryService.FindById(id);
            CategoryViewModel categoryViewModel = new CategoryViewModel(categoryModel);

            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
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
        public ActionResult Create([Bind(Include = "CategoryId,Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                this.categoryService.CreateCategoryWithNewGuid(Mapper.ToCategory(categoryViewModel));
                
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
            CategoryModel categoryModel = this.categoryService.FindById(id);
            CategoryViewModel categoryViewModel = new CategoryViewModel(categoryModel);

            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }

            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                this.categoryService.EditCategory(Mapper.ToCategory(categoryViewModel));
                return RedirectToAction("Index");
            }
            return View(categoryViewModel);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categoryModel = this.categoryService.FindById(id);
            CategoryViewModel categoryViewModel = new CategoryViewModel(categoryModel);

            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
