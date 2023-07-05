﻿using Library.Domain;
using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Persistence
{
	public class BooksRepository
	{
		public Menu menu = new Menu();
		private readonly List<Book> _database = new List<Book>();

		public BooksRepository()
		{
			_database.Add(new Book("Stary człowiek i morze", "Ernest Hemingway", 1986, "AAAA", 10, 19.99m));
			_database.Add(new Book("Komu bije dzwon", "Ernest Hemingway", 1997, "BBBB", 10, 119.99m));
			_database.Add(new Book("Alicja w krainie czarów", "C.K. Lewis", 1998, "CCCC", 10, 39.99m));
			_database.Add(new Book("Opowieści z Narnii", "C.K. Lewis", 1999, "DDDD", 10, 49.99m));
			_database.Add(new Book("Harry Potter", "J.K. Rowling", 2000, "EEEE", 10, 69.99m));
			_database.Add(new Book("Paragraf 22", "Joseph Heller", 2001, "FFFF", 10, 45.99m));
			_database.Add(new Book("Lalka", "Bolesław Prus", 2002, "GGGG", 10, 76.99m));
			_database.Add(new Book("To", "Stephen King", 2003, "HHHH", 10, 12.99m));
			_database.Add(new Book("Idiota", "Fiodor Dostojewski", 1950, "IIII", 10, 25.99m));
			_database.Add(new Book("Mistrz i Małgorzata", "Michaił Bułhakow", 1965, "JJJJ", 10, 36.99m));

			MenuUpdate();
		}
		//TODO 0 sprawdz czy nie dodać tych książek inaczej, jakoś ładniej


		public void Insert(Book book)
		{
			_database.Add(book);
			MenuUpdate();
		}

		public List<Book> GetAll()
		{
			return _database;
		}

		public bool RemoveTitle()
		{
			if (_database.Count == 0)
			{
				Console.WriteLine("Brak pozycji w repozytorium");
				return false;
			}
			int toDelete;
			do
			{
				Console.Clear();
				toDelete = menu.Wyswietl();
				if (toDelete == -1)
				{
					return false;
				}
				Book toDeleteBook = _database[toDelete];
				Console.WriteLine($"Czy na pewno usunąć {toDeleteBook.Title}? [TAK/NIE]");
				string choice = Console.ReadLine();
				if (String.Compare(choice, "TAK", true) != 0)
				{
					_database.Remove(_database[toDelete]);
					MenuUpdate();
					return true;
				}
			} while (toDelete != -1);

			return false;
		}

		public void ChangeState(string title, int stateChange)
		{
			if (_database.Count == 0)
			{
				Console.WriteLine("Brak książek w repozytorium");
				return;
			}
			var book = _database.FirstOrDefault(x => x.Title == title);
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
			return _database[id];
		}

		public List<string> ListTitleAuthorProductsAvaliable()
		{
			List<string> list = new List<string>();
			int maxTitleLength = _database.OrderByDescending(s => s.Title.Length).FirstOrDefault().Title.Length;
			int maxAuthorLength = _database.OrderByDescending(s => s.Author.Length).FirstOrDefault().Author.Length;

			for (int i = 0; i < _database.Count; i++)
			{
				list.Add($"{_database[i].Title.PadRight(maxTitleLength + 5)}" +
					$"{_database[i].Author.PadRight(maxAuthorLength + 5)}" +
					$"{_database[i].ProductsAvailable}");
			}
			return list;
		}

		public void MenuUpdate()
		{
			menu.Konfiguruj(ListTitleAuthorProductsAvaliable());
		}

		public int DatabaseCount()
		{
			return _database.Count;
		}
	}
}