using LgbtiLibrary.Data.Models;
using LgbtiLibrary.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LgbtiLibrary.MVC.Models
{
    public class AuthorViewModel
    {
        public AuthorViewModel()
        {

        }

        public AuthorViewModel(AuthorModel author)
        {
            this.AuthorId = author.AuthorId;
            this.Name = author.Name;
        }

        public Guid AuthorId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}