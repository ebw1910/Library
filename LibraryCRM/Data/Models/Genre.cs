using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Data.Models
{
    public class Genre
    {
        [Required(ErrorMessage = "Please specify а category")]
        public string Name { get; set; }
        public int Id { get; set; }
        public IEnumerable<Book> Book
        {
            get; set;
        }
    }
}
