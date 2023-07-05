using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
	public class Order
	{
		public DateTime Date { get; }
		public List<BookOrdered> BooksOrderedList;

		public Order(List<BookOrdered> booksOrderedList)
		{
			BooksOrderedList = booksOrderedList;
			Date = DateTime.Now;
		}

		public Order()
		{
			Date = DateTime.Now;
			BooksOrderedList = new List<BookOrdered>();
		}

		public string BooksListToString()
		{
			StringBuilder booksListString = new StringBuilder();
			foreach (var b in BooksOrderedList)
			{
				booksListString.Append($"{b._bookOrdered.Title.PadRight(25)}{b.Amount}\n");
			}
			return booksListString.ToString();
		}

		public void returnOrder()
		{
			foreach (var b in BooksOrderedList)
			{
				b.ReturnOrderedBooks();
			}
		}

		public void returnBooksFromOrder(bool[] toReturn)
		{
			if (toReturn.Length > BooksOrderedList.Count)
			{
				Console.WriteLine("zjebalo sie");
				Console.ReadKey();
			}
			for (int i = 0; i < BooksOrderedList.Count; i++)
			{
				if (toReturn[i])
				{
					BooksOrderedList[i].ReturnOrderedBooks();
					BooksOrderedList.RemoveAt(i);
					i--;
				}
			}
		}

		public List<string> getBooksFromOrder()
		{
			List<string> list = new List<string>();
			foreach (var item in BooksOrderedList)
			{
				list.Add(item.GetBookAndAmount());
			}
			return list;
		}
	}
}