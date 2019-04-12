using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Services.Models
{
    public class BookModel
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
