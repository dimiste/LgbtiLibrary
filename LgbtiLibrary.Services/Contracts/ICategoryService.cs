using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LgbtiLibrary.Services.Contracts
{
    public interface ICategoryService
    {
        void CreateCategoryWithNewGuid(Category category);
        void DeleteCategory(Guid id);
        void Dispose();
        void EditCategory(Category category);
        CategoryModel FindById(Guid? id);
        IQueryable<CategoryModel> GettAll();
    }
}
