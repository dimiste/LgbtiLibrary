using System;
using System.Linq;
using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;

namespace LgbtiLibrary.Services.Contracts
{
    public interface IAuthorService
    {
        void CreateAuthorWithNewGuid(Author author);
        void DeleteAuthor(Guid id);
        void Dispose();
        void EditCategory(Author author);
        AuthorModel FindById(Guid? id);
        IQueryable<AuthorModel> GettAll();
    }
}