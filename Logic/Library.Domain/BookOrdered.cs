using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain
{
    public class BookOrdered
    {
        public BookOrdered()
        {

        }
        public BookOrdered(int bookId, int numerOrdered)
        {
            BookId = bookId;
            NumerOrdered = numerOrdered;//ilość?
        }

        public int BookId { get; }
        public int NumerOrdered { get; set; }
    }
}
