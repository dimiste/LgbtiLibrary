using Bytes2you.Validation;

using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.Services.Contracts;
using LgbtiLibrary.Services.Models;

using System;
using System.Linq;

namespace LgbtiLibrary.Services.Data
{
    public class AuthorService : IAuthorService
    {
        private readonly IEFRepository<Author> authorSetWrapper;

        public AuthorService(IEFRepository<Author> authorSetWrapper)
        {
            Guard.WhenArgument(authorSetWrapper, "authorSetWrapper").IsNull().Throw();

            this.authorSetWrapper = authorSetWrapper;
        }

        public AuthorModel FindById(Guid? id)
        {
            AuthorModel result = null;

            if (id.HasValue)
            {
                Author author = this.authorSetWrapper.GetById(id.Value);
                if (author != null)
                {
                    result = new AuthorModel(author);
                }
            }

            return result;
        }

        public IQueryable<AuthorModel> GettAll()
        {
            return this.authorSetWrapper.All.Select(AuthorModel.Create);
        }

        public void CreateAuthorWithNewGuid(Author author)
        {
            this.CreateGuid(author);
            this.authorSetWrapper.Add(author);
            this.SaveChanges();

        }

        private Author CreateGuid(Author author)
        {
            author.AuthorId = Guid.NewGuid();
            return author;
        }

        public void EditCategory(Author author)
        {
            this.authorSetWrapper.Update(author);
            this.SaveChanges();
        }

        public void DeleteAuthor(Guid id)
        {
            Author author = this.authorSetWrapper.GetById(id);

            if (author != null)
            {
                this.authorSetWrapper.Delete(author);
                this.SaveChanges();
            }
            
        }

        private void SaveChanges()
        {
            this.authorSetWrapper.SaveChanges();
        }

        public void Dispose()
        {
            this.authorSetWrapper.Dispose();
        }
    }
}
