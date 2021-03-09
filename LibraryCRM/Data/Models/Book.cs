using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Data.Models
{
    public class Book
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название книги")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите автора")]
        public string Author { get; set; }

        public int Count { get; set; }
        public int GenresID { get; set; }
        public Genre Genres { get; set; }
    }
}
