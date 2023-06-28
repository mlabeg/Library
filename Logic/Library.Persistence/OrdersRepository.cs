using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence
{
	public class OrdersRepository
	{
		private List<Order> database = new List<Order>();

		public void Insert(Order order)
		{
			database.Add(order);
		}

		public List<Order> GetAll()
		{
			return database;
		}

		public List<string> GetOrders()
		{
			List<string> orders = new List<string>();
			foreach (Order o in database)
			{
				orders.Add($"{o.Date}\n{o.BooksListToString()}");
			}
			return orders;
		}
		public void ReturnWholeOrder(int toReturn)
		{
			database[toReturn].returnOrder();
			database.RemoveAt(toReturn);
		}
		public int BooksAndOrdersCount()		//przesuwanie w dół wyświetlanego komunikatu
		{
			int booksAndOrdersCount = 0;

			foreach (Order o in database)
			{
				booksAndOrdersCount += o.BooksOrderedList.Count;
			}
			booksAndOrdersCount+= 2*database.Count;
			return booksAndOrdersCount;
		}
	}
}
