using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Models.ViewModels
{
    public class BookListViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentGenres { get; set; }
    }
}
