using Bytes2you.Validation;

using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using PagedList;
using System.Linq;

namespace LgbtiLibrary.Services.Data
{
    public class BookService
    {
        private readonly IEFRepository<Book> bookSetWrapper;

        public BookService(IEFRepository<Book> bookSetWrapper)
        {
            Guard.WhenArgument(bookSetWrapper, "bookSetWrapper").IsNull().Throw();

            this.bookSetWrapper = bookSetWrapper;
        }

        public IQueryable<Book> OrderById()
        {
            return this.GetAllBooks().OrderBy(b => b.BookId);
        }

        public IQueryable<Book> GetAllBooks()
        {
            return this.bookSetWrapper.All;
        }

        public IPagedList<Book> ToPagedList(string sortOrder, string currentFilter, string searchString, int? page, int pageSize)
        {
            int pageNumber = (page ?? 1);
            return this.OrderById().ToPagedList(pageNumber, pageSize);
        }
    }
}
