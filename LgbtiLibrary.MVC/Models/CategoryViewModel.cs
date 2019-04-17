using LgbtiLibrary.MVC.Common.Contracts;
using LgbtiLibrary.Services.Models;

namespace LgbtiLibrary.MVC.Models
{
    public class CategoryViewModel : BookElementViewModel, IBookElementViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(CategoryModel category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }
    }
}