using Library.Domain;
using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Persistence
{
	public class BooksRepository
	{
		public Menu booksMenu = new Menu();
		public List<Book> database = new List<Book>();

		public BooksRepository()
		{ }

		public void Insert(Book book)
		{
			database.Add(book);
			MenuUpdate();
		}

		public List<Book> GetAll()
		{
			return database;
		}

		public bool RemoveTitle()
		{
			if (database.Count == 0)
			{
				Console.WriteLine("Brak pozycji w repozytorium");
				return false;
			}
			int toDelete;
			do
			{
				Console.Clear();
				toDelete = booksMenu.Wyswietl();
				if (toDelete == -1)
				{
					return false;
				}
				Book toDeleteBook = database[toDelete];
				Console.WriteLine($"Czy na pewno usunąć {toDeleteBook.Title}? [TAK/NIE]");
				string choice = Console.ReadLine();
				if (String.Compare(choice, "TAK", true) != 0)
				{
					database.Remove(database[toDelete]);
					MenuUpdate();
					return true;
				}
			} while (toDelete != -1);

			return false;
		}

		public void ChangeState(string title, int stateChange)
		{
			if (database.Count == 0)
			{
				Console.WriteLine("Brak książek w repozytorium");
				return;
			}
			var book = database.FirstOrDefault(x => x.Title == title);
			if (book != null)
			{
				book.State = stateChange;
				Console.WriteLine("Pomyślnie zmieniono status książki!");
			}
			else
			{
				Console.WriteLine("Błąd zmiany statusu książki!");
			}
			Console.ReadKey();
		}

		public Book BookInfo(int id)
		{
			return database[id];
		}

		public void GetBooksFullInfo(int id)
		{
			database[id].getFullInfo();
		}

		public List<string> ListTitleAuthorProductsAvaliable()
		{
			List<string> list = new List<string>();
			int maxTitleLength = database.OrderByDescending(s => s.Title.Length).FirstOrDefault().Title.Length;
			int maxAuthorLength = database.OrderByDescending(s => s.Author.Length).FirstOrDefault().Author.Length;

			for (int i = 0; i < database.Count; i++)
			{
				list.Add($"{database[i].Title.PadRight(maxTitleLength + 5)}" +
					$"{database[i].Author.PadRight(maxAuthorLength + 5)}" +
					$"{database[i].ProductsAvailable}");
			}
			return list;
		}

		public void MenuUpdate()
		{
			booksMenu.Konfiguruj(ListTitleAuthorProductsAvaliable());
		}

		public int DatabaseCount()
		{
			return database.Count;
		}
	}
}