using LgbtiLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Services.Models
{
    public class AuthorModel
    {
        public AuthorModel()
        {

        }

        public AuthorModel(Author author)
        {
            this.AuthorId = author.AuthorId;
            this.Name = author.Name;
        }

        public Guid AuthorId { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Author, AuthorModel>> Create {
            get {
                return c => new AuthorModel()
                {
                    AuthorId = c.AuthorId,
                    Name = c.Name
                };
            }
        }
    }
}
