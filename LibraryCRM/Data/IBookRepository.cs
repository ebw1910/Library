using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Data
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
        Task SaveBookAsync(Book book);
        IQueryable<Genre> Genres { get; }

        Task <Book> DeleteBookAsync(int bookID);
    }
}
