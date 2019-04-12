using LgbtiLibrary.Data.Models;

namespace LgbtiLibrary.MVC.Models
{
    public class Mapper
    {
        public static Category ToCategory(CategoryViewModel categoryViewModel)
        {
            return new Category()
            {
                CategoryId = categoryViewModel.CategoryId,
                Name = categoryViewModel.Name
            };
        }
    }
}