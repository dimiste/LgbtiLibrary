using Bytes2you.Validation;
using LgbtiLibrary.Data.Data;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.Services.Contracts;
using LgbtiLibrary.Services.Models;

using System;
using System.Linq;

namespace LgbtiLibrary.Services.Data
{
    public class BookElementService : IBookElementService
    {
        private readonly EFRepository<BookElement> bookElementSetWrapper;

        private LgbtiLibraryDb db = new LgbtiLibraryDb();

        public BookElementService(EFRepository<BookElement> bookElementSetWrapper)
        {
            Guard.WhenArgument(bookElementSetWrapper, "bookElementSetWrapper").IsNull().Throw();

            this.bookElementSetWrapper = bookElementSetWrapper;
        }

        public BookElementModel FindById(Guid? id)
        {
            BookElementModel result = null;

            if (id.HasValue)
            {
                IBookElement bookElement = this.bookElementSetWrapper.GetById(id.Value);
                if (bookElement != null)
                {
                    result = new BookElementModel(bookElement);
                }
            }

            return result;
        }

        public IQueryable<BookElementModel> GettAll()
        {
            return this.bookElementSetWrapper.All.Select(BookElementModel.Create);
        }

        public IQueryable<BookElementModel> GettAllCategories()
        {
            return this.bookElementSetWrapper.All.Where(b => b is Category).Select(BookElementModel.Create);
        }

        public IQueryable<BookElementModel> GettAllAuthors()
        {
            return this.bookElementSetWrapper.All.Where(b => b is Author).Select(BookElementModel.Create);
        }

        public void CreateBookElementWithNewGuid(BookElement bookElement)
        {
            this.CreateGuid(bookElement);
            this.bookElementSetWrapper.Add(bookElement);
            this.SaveChanges();

        }

        private IBookElement CreateGuid(IBookElement bookElement)
        {
            bookElement.Id = Guid.NewGuid();
            return bookElement;
        }

        public void EditBookElement(BookElement bookElement)
        {
            this.bookElementSetWrapper.Update(bookElement);
            this.SaveChanges();
        }

        public void DeleteBookElement(Guid id)
        {
            BookElement bookElement = this.bookElementSetWrapper.GetById(id);

            if (bookElement != null)
            {
                this.bookElementSetWrapper.Delete(bookElement);
                this.SaveChanges();
            }

        }

        private void SaveChanges()
        {
            this.bookElementSetWrapper.SaveChanges();
        }

        public void Dispose()
        {
            this.bookElementSetWrapper.Dispose();
        }
    }
}
