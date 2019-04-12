using Bytes2you.Validation;

using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
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
            return this.bookSetWrapper.All.OrderBy(b => b.BookId);
        }
    }
}
