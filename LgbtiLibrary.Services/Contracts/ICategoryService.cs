using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LgbtiLibrary.Services.Contracts
{
    public interface ICategoryService
    {
        CategoryModel FindById(Guid? id);

        IQueryable<CategoryModel> GettAll();

        void DeleteCategory(Guid id);

        void CreateCategoryWithNewGuid(Category category);

        void EditCategory(Category category);

        void Dispose();
    }
}
