using Bytes2you.Validation;

using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Data.Repositories;
using LgbtiLibrary.Services.Contracts;
using LgbtiLibrary.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LgbtiLibrary.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IEFRepository<Category> categorySetWrapper;

        public CategoryService(IEFRepository<Category> categorySetWrapper)
        {
            Guard.WhenArgument(categorySetWrapper, "categorySetWrapper").IsNull().Throw();

            this.categorySetWrapper = categorySetWrapper;
        }

        public CategoryModel FindById(Guid? id)
        {
            CategoryModel result = null;

            if (id.HasValue)
            {
                Category category = this.categorySetWrapper.GetById(id.Value);
                if (category != null)
                {
                    result = new CategoryModel(category);
                }
            }

            return result;
        }

        public IQueryable<CategoryModel> GettAll()
        {
            return this.categorySetWrapper.All.Select(CategoryModel.Create);
        }

        public void CreateCategoryWithNewGuid(Category category)
        {
            this.CreateGuid(category);
            this.categorySetWrapper.Add(category);
            this.categorySetWrapper.SaveChanges();
           
        }

        private Category CreateGuid(Category category)
        {
            category.CategoryId = Guid.NewGuid();
            return category;
        }

        public void EditCategory(Category category)
        {
            this.categorySetWrapper.Update(category);
            this.categorySetWrapper.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            Category category = this.categorySetWrapper.GetById(id);
            this.categorySetWrapper.Delete(category);
            this.SaveChanges();
        }

        private void SaveChanges()
        {
            this.categorySetWrapper.SaveChanges();
        }

        public void Dispose()
        {
            this.categorySetWrapper.Dispose();
        }
    }
}
