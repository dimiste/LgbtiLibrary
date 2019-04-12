using LgbtiLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LgbtiLibrary.MVC.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.Name = category.Name;
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}