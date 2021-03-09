using LibraryCRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Data
{
    public class EFBookRepository : IBookRepository
    {
        private ApplicationDbContext context;

        public EFBookRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Book> Books => context.Books;
        public IQueryable<Genre> Genres => context.Genres;

        public async Task SaveBookAsync(Book book)
        {
            if (book.BookID == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books
                    .FirstOrDefault(p => p.BookID == book.BookID);
                if (dbEntry != null)
                {
                    dbEntry.Name = book.Name;
                    dbEntry.Author = book.Author;
                    dbEntry.Count = book.Count;
                    dbEntry.GenresID = book.GenresID;
                }
            }
           await context.SaveChangesAsync();
        }
        public async Task<Book> DeleteBookAsync(int bookID)
        {
            Book dbEntry = context.Books
                .FirstOrDefault(p => p.BookID == bookID);
            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
               await context.SaveChangesAsync();
            }
            return dbEntry;
        }
    }
}
