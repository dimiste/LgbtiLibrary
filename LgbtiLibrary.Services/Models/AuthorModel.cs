using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Contracts;

namespace LgbtiLibrary.Services.Models
{
    public class AuthorModel : BookElementModel, IBookElementModel
    {
        public AuthorModel()
        {

        }

        public AuthorModel(Author author)
        {
            this.Id = author.Id;
            this.Name = author.Name;
        }
    }
}
