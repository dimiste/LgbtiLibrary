using LgbtiLibrary.Services.Models;
using System;

namespace LgbtiLibrary.Services.Contracts
{
    public interface IBookModel
    {
        AuthorModel Author { get; set; }
        Guid BookId { get; set; }
        CategoryModel Category { get; set; }
        string Description { get; set; }
        string Title { get; set; }
        string UrlBook { get; set; }
        string UrlImage { get; set; }
    }
}