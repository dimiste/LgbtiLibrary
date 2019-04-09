using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Data.Models
{
    public class Book
    {
        public virtual Guid BookId { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Display(Name = "Описание")]
        public virtual string Description { get; set; }

        [Required]
        public virtual Author Author { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public virtual string UrlBook { get; set; }

        public virtual string UrlImage { get; set; }
    }
}
