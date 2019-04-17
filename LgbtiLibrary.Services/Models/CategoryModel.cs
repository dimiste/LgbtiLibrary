using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Contracts;

namespace LgbtiLibrary.Services.Models
{
    public class CategoryModel : BookElementModel, IBookElementModel
    {
        public CategoryModel()
        {

        }

        public CategoryModel(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }

    }
}
