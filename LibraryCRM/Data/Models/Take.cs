using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Data.Models
{
    public class Take
    {
        private List<TakeLine> lineCollection = new List<TakeLine>();

        public virtual void AddItem(Book book, int quantity)
        {
            TakeLine line = lineCollection
                .Where(p => p.Book.BookID == book.BookID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new TakeLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Book book) =>
            lineCollection.RemoveAll(l => l.Book.BookID == book.BookID);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Book.Count * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<TakeLine> Lines => lineCollection;
    }

    public class TakeLine
    {
        public int TakeLineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}

