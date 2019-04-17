using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgbtiLibrary.Data.Models
{
    public class BookElement : IBookElement
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }
}
