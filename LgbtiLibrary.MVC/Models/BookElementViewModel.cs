using LgbtiLibrary.Data.Models;
using LgbtiLibrary.MVC.Common.Contracts;
using LgbtiLibrary.Services.Contracts;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace LgbtiLibrary.MVC.Models
{
    public class BookElementViewModel : IBookElementViewModel
    {
        public BookElementViewModel()
        {

        }

        public BookElementViewModel(IBookElementModel bookElementModel)
        {
            this.Id = bookElementModel.Id;
            this.Name = bookElementModel.Name;
        }

        public BookElementViewModel(BookElement bookElement)
        {
            this.Id = Id;
            this.Name = bookElement.Name;
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public static Expression<Func<IBookElementModel, IBookElementViewModel>> Create {
            get {
                return c => new BookElementViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                };
            }
        }
    }
}