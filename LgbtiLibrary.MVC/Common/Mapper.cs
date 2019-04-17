using LgbtiLibrary.Data.Models;
using LgbtiLibrary.MVC.Common.Contracts;

namespace LgbtiLibrary.MVC.Common
{
    public class Mapper
    {
        public static Category ToCategory(IBookElementViewModel categoryViewModel)
        {
            return new Category()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }

        public static Author ToAuthor(IBookElementViewModel authorViewModel)
        {
            return new Author()
            {
                Id = authorViewModel.Id,
                Name = authorViewModel.Name
            };
        }

        public static BookElement ToBookElement(IBookElementViewModel categoryViewModel)
        {
            return new BookElement()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }
    }
}