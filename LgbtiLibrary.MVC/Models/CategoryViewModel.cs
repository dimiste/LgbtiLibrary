using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace LgbtiLibrary.MVC.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(CategoryModel category)
        {
            this.CategoryId = category.CategoryId;
            this.Name = category.Name;
        }

        public CategoryViewModel(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.Name = category.Name;
        }

        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public static Expression<Func<CategoryModel, CategoryViewModel>> Create {
            get {
                return c => new CategoryViewModel()
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                };
            }
        }
    }
}