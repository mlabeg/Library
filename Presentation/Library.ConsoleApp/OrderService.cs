using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain;
using Library.Persistence;


namespace Library.ConsoleApp
{
	internal class OrderService
	{
		private OrdersRepository _ordersRepository;
		public OrderService(OrdersRepository ordersRepository)
		{
			_ordersRepository = ordersRepository;
		}

		public bool PlaceOrder()
		{
			Order order = new Order();

			string action = "add";

			int bookId;
			int amount;
			do
			{
				Console.WriteLine("Podaj Id ksiazki: ");
				bookId = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(" Podaj ilość: ");
				amount = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("\nWybierz akcje: \n Add \n End");//<-wrocić tutaj po dodaniu książki, żeby zapytać czy dodać następną
				do
				{
					action = Console.ReadLine();
				} while (action != "end" && action != "add");
			} while (action != "end");


			BookOrdered bookOrdered = new BookOrdered(bookId, amount);
			order.BooksOrderedList.Add(bookOrdered);


		
			Console.ReadKey();

			if (!(order is null))
			{
				_ordersRepository.Insert(order);
				// Console.WriteLine("Pomyślnie dodano zamówienei do bazy!");
				return true;
			}
			else
			{
				Console.WriteLine("Brak pozycji zamówienia zamówienia");
				return false;
			}
		}


		public List<Order> ListAll()
		{
			return _ordersRepository.GetAll();
		}

	}
}
//TODO dopracować PlaceOrder (w tym momencie można dodać tylko 1 książkę do zamówienia), możesz dodać tu menu z poprzedniego semestru

//PYTANIA jeśli nie chcicałbym, zeby np. BookOrdered było public jak udostępniać to między projektami?

