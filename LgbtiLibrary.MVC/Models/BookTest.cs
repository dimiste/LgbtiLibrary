using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LgbtiLibrary.MVC.Models
{
    public class BookTest
    {
        public Guid BookTestId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public HttpPostedFile File { get; set; }
    }
}