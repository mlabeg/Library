using Library.Domain;
using Library.Persistence;
using MenuUITools;
using System;
using System.Linq;

namespace Library.ConsoleApp
{
	internal class OrderService
	{
		private readonly OrdersRepository _ordersRepository;
		private readonly BooksRepository _booksRepository;
		private readonly int _booksRepositoryCount;
		private Menu booksMenu = new Menu();
		private Menu orderMenu = new Menu();

		public OrderService(OrdersRepository ordersRepository, BooksRepository booksRepository)
		{
			_ordersRepository = ordersRepository;
			_booksRepository = booksRepository;
			_booksRepositoryCount = booksRepository.DatabaseCount();
		}

		public bool PlaceOrder()
		{
			void ListCurrentOrder(Order currentOrder)
			{
				if (currentOrder.BooksOrderedList.Count == 0)
				{
					return;
				}

				Console.SetCursorPosition(0, _booksRepositoryCount + 3);
				Console.WriteLine("Aktualne zamówienie: ");
				foreach (var b in currentOrder.BooksOrderedList)
				{
					var book = b.GetOrderedBook();

					Console.WriteLine($"{book.Title.PadRight(25)} " +
						$"{book.Author.PadRight(20)}" +
						$"ilość egzemplarzy: {b.Amount}");
				}
			}

			if (_booksRepository.DatabaseCount() == 0)
			{
				Console.WriteLine("Brak książek do wypożyczenia");
				return false;
			}

			Order order = new Order();
			booksMenu.Konfiguruj(this._booksRepository.ListTitleAuthorProductsAvaliable());
			var _booksRepositoryList = this._booksRepository.GetAll();

			int amount;
			int positionChosen;
			string action;

			do
			{
				Console.Clear();

				ListCurrentOrder(order);

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
							Console.WriteLine("Ilość musi być większa niż 0!");
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
								orderPosition.Amount += amount;
							}
							else
							{
								BookOrdered _bookOrdered = new BookOrdered(_booksRepositoryList[positionChosen], amount);
								_booksRepositoryList[positionChosen].ProductsAvailable -= amount;
								order.BooksOrderedList.Add(_bookOrdered);
								booksMenu.Konfiguruj(this._booksRepository.ListTitleAuthorProductsAvaliable());
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
					ListCurrentOrder(order);

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

		//private void ListCurrentOrder(Order order)
		//{
		//	Console.SetCursorPosition(0, _booksRepositoryCount + 3);
		//	Console.WriteLine("Aktualne zamówienie: ");
		//	foreach (var b in order.BooksOrderedList)
		//	{
		//		var book = b.GetOrderedBook();

		//		Console.WriteLine($"{book.Title.PadRight(25)} " +
		//			$"{book.Author.PadRight(20)}" +
		//			$"ilość egzemplarzy: {b.Amount}");
		//	}
		//}

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
					Console.WriteLine($"\"{b._bookOrdered.Title}\" {b._bookOrdered.Author} wypożyczona ilość {b.Amount} sztuki");
				}
				Console.WriteLine();
			}
			return true;
		}

		public void ReturnOrder()
		{
			orderMenu.Konfiguruj(this._ordersRepository.GetOrders());
			Menu orderChoiceMenu = new Menu();
			orderChoiceMenu.Konfiguruj(new string[] { "Zwroc cale zamowienie", "Zwroc wybrane pozycje" });
			string choice = "";
			int toReturn, returnAction;

			do
			{
				Console.Clear();
				toReturn = orderMenu.Wyswietl();
				if (toReturn < 0)
				{
					return;
				}
				var rowCount = _ordersRepository.BooksAndOrdersCount();
				returnAction = orderChoiceMenu.Wyswietl(rowCount);

				Console.SetCursorPosition(0, rowCount + 3);
				if (returnAction == 0)
				{
					Console.WriteLine("Na pewno zwrócić książki z wybranej pozycji? [Tak/Nie] ");
					choice = Console.ReadLine();
				}
				else if (returnAction == 1)
				{
					/*Console.WriteLine("Usługa w przygotowaniu!");*/
					ReturnBooksFromOrder(_ordersRepository.GetOrder(toReturn));
					choice = "TAK";
				}
			} while (String.Compare(choice, "TAK", true) != 0);

			if (returnAction == 0)
			{
				_ordersRepository.ReturnWholeOrder(toReturn);
			}
		}

		public void ReturnBooksFromOrder(Order order)
		{
			orderMenu.Konfiguruj(order.getBooksFromOrder());
			Console.Clear();
			bool[] chosenToReturn = orderMenu.Zaznacz();
			order.returnBooksFromOrder(chosenToReturn);
		}
	}
}

//TODO 3? możliwość zwrotu pojedynczych książek za zamówienia - trochę już pod to napisałeś
//TODO