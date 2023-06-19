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
			Order order = new Order();
			menuBooks.Konfiguruj(_booksRepository.TitleAuthorProductsAvalliableList());
			var _booksRepositoryList = _booksRepository.GetAll();
			
			int amount;
			int positionChosen;
			string action = "add";

			do
			{
				Console.Clear();
				positionChosen = menuBooks.Wyswietl();

				if (positionChosen != -1)
				{
					Console.Write("Podaj ilość: ");
					amount = Convert.ToInt32(Console.ReadLine());
					_booksRepository.GetAll();
					BookOrdered _bookOrdered = new BookOrdered(_booksRepositoryList[positionChosen], amount);
					order.BooksOrderedList.Add(_bookOrdered);
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

			Console.ReadKey();

			if (!(order is null))
			{
				_ordersRepository.Insert(order);
				return true;
			}
			else
			{
				Console.WriteLine("Brak pozycji zamówienia!");
				return false;
			}
		}


		public List<Order> ListAll()
		{
			return _ordersRepository.GetAll();
		}

	}
}

//PYTANIA jeśli nie chcicałbym, zeby np. BookOrdered było public jak udostępniać to między projektami?
 //??

