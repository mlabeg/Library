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
		private readonly OrdersRepository _ordersRepository;
		private readonly BooksRepository _booksRepository;
		private readonly int _booksRepositoryCount;
		Menu booksMenu = new Menu();
		Menu ordersMenu = new Menu();


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
			booksMenu.Konfiguruj(this._booksRepository.TitleAuthorProductsAvalliableList());
			var _booksRepositoryList = this._booksRepository.GetAll();

			int amount;
			int positionChosen;
			string action;

			do
			{
				Console.Clear();
				if (order.BooksOrderedList.Count != 0)
				{
					ListCurrentOrder(order);
				}//TODO ? zastanów się czy nie lepiej wstawić linii 47 do powyższej funkcji - DRY, ale czy wywoływanie funkcji na puustym zamówieniu ma sens?
					//TODO 3 sprawdz jeszcze te linijki, czy tu nie odwołujesz się do wszystkich zamówień zamiast tylko do tego obecnego?

				positionChosen = booksMenu.Wyswietl();

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

							var orderPosition = order.BooksOrderedList.FirstOrDefault(b => b._bookOrdered == _booksRepositoryList[positionChosen]);
							if (orderPosition != null)
							{
								orderPosition.NumerOrdered += amount;
							}
							else
							{
								BookOrdered _bookOrdered = new BookOrdered(_booksRepositoryList[positionChosen], amount);
								_booksRepositoryList[positionChosen].ProductsAvailable -= amount;
								order.BooksOrderedList.Add(_bookOrdered);
								booksMenu.Konfiguruj(this._booksRepository.TitleAuthorProductsAvalliableList());
							}

						}

					}
					else
					{
						Console.WriteLine("Brak dostępnych pozycji!");
						Console.ReadKey();
					}
				}
				bool CheckAction(string act)
				{
					return String.Compare(act, "add", true) != 0 && String.Compare(act, "end", true) != 0;
				}

				do
				{
					if (order.BooksOrderedList.Count != 0)
					{
						ListCurrentOrder(order);
					}
					Console.WriteLine("\nWybierz akcje: \n Add \n End");
					action = Console.ReadLine();
					if (CheckAction(action))
					{
						Console.WriteLine("Nieznane polecenie!!");
					}
				} while (CheckAction(action));
			} while (String.Compare(action, "end", true) != 0);

			if (order.BooksOrderedList.Count == 0)
			{
				Console.WriteLine("Brak pozycji zamówienia!");
				return false;
			}

			_ordersRepository.Insert(order);
			return true;
		}




		private void ListCurrentOrder(Order order)
		{
			Console.SetCursorPosition(0, _booksRepositoryCount + 3);
			Console.WriteLine("Aktualne zamówienie: ");
			foreach (var b in order.BooksOrderedList)
			{
				var book = b.GetOrderedBook();

				Console.WriteLine($"{book.Title.PadRight(25)} " +
					$"{book.Author.PadRight(20)}" +
					$"ilość egzemplarzy: {b.NumerOrdered}");
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

		public void ReturnOrder()
		{
			ordersMenu.Konfiguruj(this._ordersRepository.GetOrders());
			Menu orderChoiceMenu = new Menu();
			orderChoiceMenu.Konfiguruj(new string[] { "Zwroc cale zamowienie", "Zwroc wybrane pozycje" });
			string choice = "";
			int toReturn, returnAction;

			do
			{
				Console.Clear();
				toReturn = ordersMenu.Wyswietl();
				if (toReturn < 0)
				{
					return;
				}
				var rowCount = _ordersRepository.BooksAndOrdersCount();
				returnAction = orderChoiceMenu.Wyswietl(rowCount);

				Console.SetCursorPosition(0, rowCount+3);
				if (returnAction == 0)
				{
					Console.WriteLine("Na pewno zwrócić książki z wybranej pozycji? [Tak/Nie] ");
					choice = Console.ReadLine();
				}
				else if (returnAction == 1)
				{
					Console.WriteLine("Usługa w przygotowaniu!");
					Console.WriteLine("Naciśnij dowolny klawisz.");
					Console.ReadKey();
				}
			} while (String.Compare(choice, "TAK", true) != 0);

			if (returnAction == 0)
			{
				_ordersRepository.ReturnWholeOrder(toReturn);
			}
		}

		public void ReturnBooksFromOrder()
		{
			ordersMenu.Konfiguruj(this._ordersRepository.GetOrders());
			Console.Clear();
			bool[] toReturn = booksMenu.Zaznacz();
			for (int i = 0; i < toReturn.Length; i++)
			{
				if (toReturn[i])
				{

				}
			}

		}



	}
}
//TODO 3? możliwość zwrotu pojedynczych książek za zamówienia - trochę już pod to napisałeś

