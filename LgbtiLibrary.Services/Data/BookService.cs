using LgbtiLibrary.Data.Data;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.Services.Contracts;
using LgbtiLibrary.Services.Models;

using PagedList;

using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LgbtiLibrary.Services.Data
{
    public class BookService
    {
        private readonly IEFRepository<Book> bookSetWrapper;

        private readonly IBookElementService bookElementService;
        private LgbtiLibraryDb db = new LgbtiLibraryDb();

        public BookService(IEFRepository<Book> bookSetWrapper)
        {
            //Guard.WhenArgument(bookSetWrapper, "bookSetWrapper").IsNull().Throw();
            //Guard.WhenArgument(authorSetWrapper, "authorSetWrapper").IsNull().Throw();
            //Guard.WhenArgument(categorySetWrapper, "categorySetWrapper").IsNull().Throw();

            this.bookSetWrapper = new EFRepository<Book>(db);
            this.bookElementService = new BookElementService(new EFRepository<BookElement>(db));
        }

        public IQueryable<IBookModel> OrderById()
        {
            return this.GetAllBooks().OrderBy(b => b.BookId);
        }

        public IQueryable<IBookModel> GetAllBooks()
        {
            return this.bookSetWrapper.All.Select(BookModel.Create);
        }

        public IPagedList<IBookModel> BookListOrderById(string sortOrder, string currentFilter, string searchString, int? page, int pageSize)
        {
            int pageNumber = (page ?? 1);
            return this.OrderById().ToPagedList(pageNumber, pageSize);
        }

        public IPagedList<IBookModel> BookListSearchByCategory(string CategoryId, string sortOrder, string currentFilter, int? page, int pageSize)
        {
            int pageNumber = (page ?? 1);
            return this.OrderById().Where(b => b.Category.Id.ToString() == CategoryId).ToPagedList(pageNumber, pageSize);
        }

        public IBookModel FindById(Guid? id)
        {
            IBookModel result = null;

            if (id.HasValue)
            {
                Book bookElement = this.bookSetWrapper.GetById(id.Value);
                if (bookElement != null)
                {
                    result = new BookModel(bookElement);
                }
            }

            return result;
        }

        private void FindFiles(FormCollection formCollection, HttpFileCollectionBase files, ref HttpPostedFileBase fileBook,ref HttpPostedFileBase fileImg, BookModel bookModel, Controller controller)
        {

            foreach (string item in files)
            {

                HttpPostedFileBase file = files[item];

                if (file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);

                    string path = string.Empty;

                    if (extension.ToLower() != ".pdf")
                    {
                        path = "../imgs/" + bookModel.BookId.ToString() + extension;
                        fileImg = file;
                        bookModel.UrlImage = path;
                    }
                    else
                    {
                        path = "../Files/" + bookModel.BookId.ToString() + extension;
                        fileBook = file;
                        bookModel.UrlBook = path;
                        this.RemoveFromModelState(controller.ModelState, "UrlBook");
                    }
                }
            }
        }

        private void RemoveFromModelState(ModelStateDictionary modelState, string fildToRemove)
        {
            modelState.Remove(fildToRemove);
        }

        public void CreateBook(BookModel bookModel, FormCollection formCollection, Controller controller)
        {
            bookModel.BookId = Guid.NewGuid();

            Author author = null;
            Category category = null;
            HttpPostedFileBase fileBook = null;
            HttpPostedFileBase fileImg = null;

            this.FindFiles(formCollection, controller.Request.Files,ref fileBook,ref fileImg, bookModel, controller);

            this.FindAuthorAndCategory(ref author,ref category, formCollection, controller);


            if (controller.ModelState.IsValid)
            {
                Book book = BookModel.CreateBookWithoutAuthorAndCategory(bookModel);

                this.PoblateAuthorAndCategoryOfBook(book, author, category);

                this.SaveUrlBookAndUrlImg(book, fileBook, fileImg, controller);

                this.bookSetWrapper.Add(book);

                this.SaveChanges();

            }

            this.PoblateDropDownToAuthorAndCategory(bookModel, controller);
        }

        private void PoblateAuthorAndCategoryOfBook(Book book, Author author, Category category)
        {
            book.Author = author;
            book.Category = category;
        }

        private void SaveUrlBookAndUrlImg(Book book, HttpPostedFileBase fileBook, HttpPostedFileBase fileImg, Controller controller )
        {
            if (book.UrlBook != null)
            {
                fileBook.SaveAs(controller.Server.MapPath(book.UrlBook));
            }

            if (book.UrlImage != null)
            {
                fileImg.SaveAs(controller.Server.MapPath(book.UrlImage));
            }
            else
            {
                book.UrlImage = "../imgs/libros.jpg";
            }
        }

        private void FindAuthorAndCategory(ref Author author,ref Category category, FormCollection formCollection, Controller controller)
        {
            var authorId = formCollection["AuthorId"];
            var categoryId = formCollection["CategoryId"];

            if (!string.IsNullOrEmpty(authorId))
            {
                author = this.db.Authors.Where(a => a.Id.ToString() == authorId).Single();
                this.RemoveFromModelState(controller.ModelState, "Author");
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                category = this.db.Categories.Where(c => c.Id.ToString() == categoryId).Single();
                this.RemoveFromModelState(controller.ModelState, "Category");
            }
        }

        private void PoblateDropDownToAuthorAndCategory(BookModel bookModel, Controller controller)
        {
            if (bookModel.Author != null)
            {
                controller.ViewBag.AuthorId = new SelectList(this.bookElementService.GettAllAuthors(), "Id", "Name", bookModel.Author.Id);
            }
            else
            {
                controller.ViewBag.AuthorId = new SelectList(this.bookElementService.GettAllAuthors(), "Id", "Name");
            }

            if (bookModel.Category != null)
            {
                controller.ViewBag.CategoryId = new SelectList(this.bookElementService.GettAllCategories(), "Id", "Name", bookModel.Category.Id);
            }
            else
            {
                controller.ViewBag.CategoryId = new SelectList(this.bookElementService.GettAllCategories(), "Id", "Name");
            }
        }

        public void DeleteBook(Guid id)
        {
            Book bookElement = this.bookSetWrapper.GetById(id);

            if (bookElement != null)
            {
                this.bookSetWrapper.Delete(bookElement);
                this.SaveChanges();
            }

        }

        private void SaveChanges()
        {
            this.bookSetWrapper.SaveChanges();
        }

        public void Dispose()
        {
            this.bookSetWrapper.Dispose();
        }
    }
}
