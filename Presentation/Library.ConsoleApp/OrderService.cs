using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain;
using Library.Persistence;
using MenuUITools;


namespace Library.ConsoleApp
{
	internal class OrderService
	{
		private OrdersRepository _ordersRepository;
		private BooksRepository _booksRepository;
		private int _booksRepositoryCount;
		Menu menuBooks = new Menu();


		public OrderService(OrdersRepository ordersRepository, BooksRepository booksRepository)
		{
			_ordersRepository = ordersRepository;
			_booksRepository = booksRepository;
			_booksRepositoryCount = booksRepository.DatabaseCount();
		}

		public bool PlaceOrder()
		{

			if (_booksRepository.DatabaseCount() == 0)
			{
				Console.WriteLine("Brak książek do wypożyczenia");
				return false;
			}
			Order order = new Order();
			menuBooks.Konfiguruj(this._booksRepository.TitleAuthorProductsAvalliableList());
			var _booksRepositoryList = this._booksRepository.GetAll();

			int amount;
			int positionChosen;
			string action = "add";

			do
			{
				Console.Clear();
				if (order.BooksOrderedList.Count != 0)
				{
					ListCurrentOrder(order);
				}//TODO ? zastanów się czy nie lepiej wstawić linii 47 do powyższej funkcji - DRY, ale czy wywoływanie funkcji na puustym zamówieniu ma sens?
				positionChosen = menuBooks.Wyswietl();

				if (positionChosen != -1)
				{
					int booksAvailable = _booksRepositoryList[positionChosen].ProductsAvailable;
					if (booksAvailable > 0)
					{
						do
						{
							Console.Write("Podaj ilość: ");
						} while (!int.TryParse(Console.ReadLine(), out amount));

						if (amount <= 0)
						{
							Console.WriteLine("Ilość musi być mniejsza niż 0!");
						}
						else if (amount > booksAvailable)
						{
							Console.WriteLine("Brak wystarczającej ilości książek!");
						}
						else
						{
							BookOrdered _bookOrdered = new BookOrdered(_booksRepositoryList[positionChosen], amount);
							_booksRepositoryList[positionChosen].ProductsAvailable -= amount;
							order.BooksOrderedList.Add(_bookOrdered);
							menuBooks.Konfiguruj(this._booksRepository.TitleAuthorProductsAvalliableList());
						}

					}
					else
					{
						Console.WriteLine("Brak dostępnych pozycji!");
						Console.ReadKey();
					}
				}

				do
				{
					if (order.BooksOrderedList.Count != 0)
					{
						ListCurrentOrder(order);
					}
					Console.WriteLine("\nWybierz akcje: \n Add \n End");
					action = Console.ReadLine();
					if (checkAction(action))
					{
						Console.WriteLine("Nieznane polecenie!!");
					}
				} while (checkAction(action));
			} while (String.Compare(action, "end", true) != 0);

			if (order.BooksOrderedList.Count == 0)
			{
				Console.WriteLine("Brak pozycji zamówienia!");
				return false;
			}

			_ordersRepository.Insert(order);
			return true;
		}
		//TODO 3.5 dodać sprawdzanie czy książka jest już w zamówieniu i tylko aktualizowa jej ilość

		private bool checkAction(string action)
		{
			return String.Compare(action, "add", true) != 0 && String.Compare(action, "end", true) != 0;
		}
		//TODO ? czy to nie jest już zbytnie kombinowanie?

		public void ListCurrentOrder(Order order)
		{
			Console.SetCursorPosition(0, _booksRepositoryCount + 3);
			Console.WriteLine("Aktualne zamówienie: ");
			foreach (var b in order.BooksOrderedList)
			{
				var book = b.getOrderedBook();

				Console.WriteLine($"{book.Title.PadRight(25)} " +
					$"{book.Author.PadRight(20)}" +
					$"ilość egzemplarzy: {b.NumerOrdered}");
				//TODO 4 możesz pokombinować z PadRight(), ale to w sumie nie takie ważne
			}
		}

		public bool ListAll()
		{

			if (_ordersRepository.GetAll().Count == 0)
			{
				return false;

			}

			foreach (Order o in _ordersRepository.GetAll())
			{
				Console.WriteLine(o.Date + ": ");
				foreach (BookOrdered b in o.BooksOrderedList)
				{
					Console.WriteLine($"\"{b._bookOrdered.Title}\" {b._bookOrdered.Author} wypożyczona ilość {b.NumerOrdered} sztuki");
				}
				Console.WriteLine();

			}
			return true;


		}



	}
}
//TODO 1 dodać możliwość zwrotów książek

