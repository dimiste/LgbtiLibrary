using LgbtiLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Data.Contracts
{
    public interface ILgbtiLibraryDb : ILgbtiLibraryBaseDb
    {
        IDbSet<Book> Books { get; set; }

        IDbSet<Author> Authors { get; set; }

        IDbSet<Category> Categories { get; set; }

        DbEntityEntry Entry(object entity);

        IDbSet<T> Set<T>() where T : class;
    }
}
