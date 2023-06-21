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
		public BookOrdered(Book bookOrdered, int numerOrdered)
		{
			_bookOrdered = bookOrdered;
			NumerOrdered = numerOrdered;
		}

		public Book _bookOrdered { get; }
		//czy tutaj nie tworzysz nowej zmiennej dla tego samego obiektu? czy stosujesz tutaj wskaźnik?
		public int BookId { get; }
		public int NumerOrdered { get; set; }
	}
}
