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
	}
}

//TODO 4 po dodaniu autentyfikacji można tutaj dodać infomację o tym kto to zaawia i wyświetlać wszystkie rezerwacje wg daty/uzytkownika/czego tylko chcesz


