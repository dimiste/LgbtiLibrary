using System;
using System.Linq;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;

namespace LgbtiLibrary.Services.Contracts
{
    public interface IBookElementService
    {
        void CreateBookElementWithNewGuid(BookElement bookElement);
        void DeleteBookElement(Guid id);
        void Dispose();
        void EditBookElement(BookElement bookElement);
        BookElementModel FindById(Guid? id);
        IQueryable<BookElementModel> GettAll();
        IQueryable<BookElementModel> GettAllCategories();
        IQueryable<BookElementModel> GettAllAuthors();
    }
}