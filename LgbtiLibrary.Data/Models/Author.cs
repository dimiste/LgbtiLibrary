using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Data.Models
{
    public class Author
    {
        public virtual Guid AuthorId { get; set; }
        public virtual string Name { get; set; }
    }
}
