using LgbtiLibrary.MVC.Common.Contracts;
using LgbtiLibrary.Services.Models;

namespace LgbtiLibrary.MVC.Models
{
    public class AuthorViewModel : BookElementViewModel, IBookElementViewModel
    {
        public AuthorViewModel()
        {

        }
        public AuthorViewModel(AuthorModel author)
        {
            this.Id = author.Id;
            this.Name = author.Name;
        }
    }
}