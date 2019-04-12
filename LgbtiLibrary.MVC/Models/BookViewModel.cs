using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LgbtiLibrary.MVC.Models
{
    public class BookViewModel
    {
        public BookViewModel()
        {

        }

        public BookViewModel(BookModel book)
        {
            if (book != null)
            {
                this.BookId = book.BookId;
                this.Title = book.Title;
                this.Description = book.Description;

                if (book.Author != null)
                {
                    this.Author = new AuthorViewModel(book.Author);
                }

                if (book.Category != null)
                {
                    this.Category = new CategoryViewModel(book.Category);
                }

                this.UrlBook = book.UrlBook;
                this.UrlImage = book.UrlImage;

            }

        }
        public virtual Guid BookId { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Display(Name = "Описание")]
        public virtual string Description { get; set; }

        [Required]
        public virtual AuthorViewModel Author { get; set; }

        [Required]
        public virtual CategoryViewModel Category { get; set; }

        [Required]
        public virtual string UrlBook { get; set; }

        public virtual string UrlImage { get; set; }
    }
}