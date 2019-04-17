using System;

namespace LgbtiLibrary.Services.Models
{
    public class BookModel : IBookModel
    {
        public virtual Guid BookId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual AuthorModel Author { get; set; }

        public virtual CategoryModel Category { get; set; }

        public virtual string UrlBook { get; set; }

        public virtual string UrlImage { get; set; }
    }
}
