using Library.Domain;
using Library.Persistence;
using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Library.ConsoleApp
{
	internal class BooksService
	{
		private readonly BooksRepository _repository;
		private Menu menu = new Menu();

		public BooksService(BooksRepository booksRepository)
		{
			_repository = booksRepository;
		}

		internal void AddBook()
		{
			Console.WriteLine("Podaj tytuł: ");
			string title = Console.ReadLine();
			Console.WriteLine("Podaj Autora: ");
			string author = Console.ReadLine();

			Console.WriteLine("Podaj rok wydania: ");
			int.TryParse(Console.ReadLine(), out int year);

			Console.WriteLine("Podaj numer ISBN");
			string isbn = Console.ReadLine();

			Console.WriteLine("Podaj ile jest dostepnych pozycji: ");
			int.TryParse(Console.ReadLine(), out int productsAvailable);

			decimal price = 0;
			int check;
			do
			{
				check = 0;
				try
				{
					Console.WriteLine("Podaj cenę: ");
					price = decimal.Parse(Console.ReadLine());
				}
				catch (FormatException)
				{
					Console.WriteLine("Bład ceny! Użyj przecinka!");
					check = 1;
				}
				catch (ArgumentNullException)
				{
					Console.WriteLine("Bląd ceny!");
					check = 1;
				}
			} while (check == 1);

			Book newBook = new Book(title, author, year, isbn, productsAvailable, price);
			_repository.Insert(newBook);

			Console.WriteLine("Pomyślnie dodano książkę!");
		}

		internal void Remove()
		{
			_repository.RemoveTitle();
		}

		internal bool ListBooks1()
		{
			List<Book> repository = _repository.GetAll();

			if (repository.Count == 0)
			{
				return false;
			}
			Console.WriteLine();
			int maxDlugosc = repository.OrderByDescending(s => s.Title.Length).FirstOrDefault().Title.Length;

			for (int i = 0; i < repository.Count; i++)
			{
				//działa poprawnie do 999. pozycji w repozytorium
				//potrzebne, żeby pozycje książek równo się wyświetlały
				if (i < 9)
				{
					Console.WriteLine($"{i + 1}.  {repository[i].Title.PadRight(maxDlugosc + 6)}" +
						$"{repository[i].Author.PadRight(20)}{repository[i].ProductsAvailable}");
				}
				else if (i < 99)
				{
					Console.WriteLine($"{i + 1}. {repository[i].Title.PadRight(maxDlugosc + 6)}" +
						$"{repository[i].Author.PadRight(20)}{repository[i].ProductsAvailable}");
				}
				else
				{
					Console.WriteLine($"{i + 1}.{repository[i].Title.PadRight(maxDlugosc + 6)}" +
						$"{repository[i].Author.PadRight(20)}{repository[i].ProductsAvailable}");
				}
			}
			return true;
		}

		public bool ListBooks()
		{
			if (_repository.DatabaseCount() == 0)
			{
				return false;
			}
			menu.Konfiguruj(_repository.ListTitleAuthorProductsAvaliable());
			int choice;
			do
			{
				Console.Clear();
				choice = menu.Wyswietl();
				Console.WriteLine();
				if (choice > 0)
				{
					_repository.GetBookFullInfo(choice);
				}
			} while (choice != -1);

			return true;
		}
		internal void ChangeStat()
		{
			Console.WriteLine("Podaj tytuł książki do zmiany statusu: ");
			string toChange = Console.ReadLine();
			Console.WriteLine("Podaj wymagany status ksiązki: (0 - niedostepa, 1 - dostepna) ");
			int state = Convert.ToInt32(Console.ReadLine());
			_repository.ChangeState(toChange, state);
		}
	}
}

//TODO 2 wyświetlenie pełnej informacji o książce

//TODO 4 dodanie możliwości edycji już istnijących ksiązek; w sumie można to załatwić za jednym zamachem używając rozwiąznia z zeszłego semestru