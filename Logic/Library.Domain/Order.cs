using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain
{
	public class Order
	{
		public DateTime Date { get; }
		public List<BookOrdered> BooksOrderedList;
		public Order()
		{
			Date = DateTime.Now;
			BooksOrderedList = new List<BookOrdered>();
		}
		public string BooksListToString()
		{
			StringBuilder booksListString=new StringBuilder();
			foreach(var b in BooksOrderedList)
			{
				booksListString.Append($"{b._bookOrdered.Title.PadRight(25)}{b.NumerOrdered}\n");
			}
			return booksListString.ToString();
		}
		public void returnOrder()
		{
			foreach(var b in BooksOrderedList)
			{
				b.ReturnOrderedBooks();
			}
		}
	}

}

//TODO 9  po dodaniu autentyfikacji można tutaj dodać infomację o tym kto to zaawia i wyświetlać wszystkie rezerwacje wg daty/uzytkownika/czego tylko chcesz


