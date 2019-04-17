using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Contracts;

using System;
using System.Linq.Expressions;

namespace LgbtiLibrary.Services.Models
{
    public class BookElementModel : IBookElementModel
    {
        public BookElementModel()
        {

        }

        public BookElementModel(IBookElement bookElement)
        {
            this.Id = bookElement.Id;
            this.Name = bookElement.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<IBookElement, BookElementModel>> Create {
            get {
                return c => new BookElementModel()
                {
                    Id = c.Id,
                    Name = c.Name
                };
            }
        }
    }
}
