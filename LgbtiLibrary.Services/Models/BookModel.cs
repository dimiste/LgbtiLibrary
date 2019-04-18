using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace LgbtiLibrary.Services.Models
{
    public class BookModel : IBookModel
    {
        public BookModel()
        {

        }

        public BookModel(Book book)
        {
            this.BookId = book.BookId;
            this.Title = book.Title;
            this.Description = book.Description;
            this.Author = new AuthorModel()
            {
                Id = book.Author.Id,
                Name = book.Author.Name
            };
            this.Category = new CategoryModel()
            {
                Id = book.Category.Id,
                Name = book.Category.Name
            };
            this.UrlBook = book.UrlBook;
            this.UrlImage = book.UrlImage;
        }

        public virtual Guid BookId { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Display(Name = "Описание")]
        public virtual string Description { get; set; }

        [Required]
        public virtual AuthorModel Author { get; set; }

        [Required]
        public virtual CategoryModel Category { get; set; }

        [Required]
        public virtual string UrlBook { get; set; }

        public virtual string UrlImage { get; set; }

        public static Book CreateBookWithoutAuthorAndCategory(BookModel bookModel)
        {
            return new Book()
            {
                BookId = bookModel.BookId,
                Title = bookModel.Title,
                Description = bookModel.Description,
                UrlBook = bookModel.UrlBook,
                UrlImage = bookModel.UrlImage
            };
        }

        public static Expression<Func<Book, IBookModel>> Create {
            get {
                return c => new BookModel()
                {
                    BookId = c.BookId,
                    Title = c.Title,
                    Description = c.Description,
                    Author = new AuthorModel()
                    {
                        Id = c.Author.Id,
                        Name = c.Author.Name
                    },
                    Category = new CategoryModel()
                    {
                        Id = c.Category.Id,
                        Name = c.Category.Name
                    },
                    UrlBook = c.UrlBook,
                    UrlImage = c.UrlImage

                };
            }
        }
    }
}
