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
		MenuUITools.Menu menuBooks = new MenuUITools.Menu();


		public OrderService(OrdersRepository ordersRepository, BooksRepository booksRepository)
		{
			_ordersRepository = ordersRepository;
			_booksRepository = booksRepository;
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
				//Console.Clear();
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
					Console.WriteLine("\nWybierz akcje: \n Add \n End");
					action = Console.ReadLine();
					if ((action != "Add" && action != "End"))
					{
						Console.WriteLine("Nieznane polecenie!!");
					}
				} while (action != "Add" && action != "End");
			} while (action != "End");

			//Console.ReadKey();

			if (order.BooksOrderedList.Count == 0)
			{
				Console.WriteLine("Brak pozycji zamówienia!");
				return false;
			}

			_ordersRepository.Insert(order);
			return true;
		}


		public List<Order> ListAll()
		{
			return _ordersRepository.GetAll();
		}

	}
}
//TODO dodać możliwość zwrotów książek

//TODO PYTANIA jeśli nie chcicałbym, zeby np. BookOrdered było public jak udostępniać to między projektami?
//??

