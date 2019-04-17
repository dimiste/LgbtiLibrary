using LgbtiLibrary.Data.Contracts;
using LgbtiLibrary.Data.Models;

using System.Data.Entity;

namespace LgbtiLibrary.Data.Data
{
    public class LgbtiLibraryDb : DbContext, ILgbtiLibraryDb
    {

        public LgbtiLibraryDb() : base("name=LgbtiLibraryDb")
        {
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<BookElement> BookElements { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

    }
}
