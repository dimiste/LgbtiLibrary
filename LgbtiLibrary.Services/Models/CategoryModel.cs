using LgbtiLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Services.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {

        }

        public CategoryModel(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.Name = category.Name;
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Category, CategoryModel>> Create {
            get {
                return c => new CategoryModel()
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                };
            }
        }
    }
}
