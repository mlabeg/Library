using Library.Domain;
using System.Collections.Generic;

namespace Library.Persistence
{
	public class LibrarySeeder
	//klasa dodająca przykładowe dane do repozytoriów
	{
		private readonly BooksRepository _booksRepository;
		private readonly OrdersRepository _ordersRepository;

		public LibrarySeeder(BooksRepository booksRepository, OrdersRepository ordersRepository)
		{
			_booksRepository = booksRepository;
			_ordersRepository = ordersRepository;
			this.BooksRepositorySeed();
			this.OrdersRepositorySeed();
		}

		private void BooksRepositorySeed()
		{
			_booksRepository.database.Add(new Book("Stary człowiek i morze", "Ernest Hemingway", 1986, "AAAA", 10, 19.99m));
			_booksRepository.database.Add(new Book("Komu bije dzwon", "Ernest Hemingway", 1997, "BBBB", 10, 119.99m));
			_booksRepository.database.Add(new Book("Alicja w krainie czarów", "C.K. Lewis", 1998, "CCCC", 10, 39.99m));
			_booksRepository.database.Add(new Book("Opowieści z Narnii", "C.K. Lewis", 1999, "DDDD", 10, 49.99m));
			_booksRepository.database.Add(new Book("Harry Potter", "J.K. Rowling", 2000, "EEEE", 10, 69.99m));
			_booksRepository.database.Add(new Book("Paragraf 22", "Joseph Heller", 2001, "FFFF", 10, 45.99m));
			_booksRepository.database.Add(new Book("Lalka", "Bolesław Prus", 2002, "GGGG", 10, 76.99m));
			_booksRepository.database.Add(new Book("To", "Stephen King", 2003, "HHHH", 10, 12.99m));
			_booksRepository.database.Add(new Book("Idiota", "Fiodor Dostojewski", 1950, "IIII", 10, 25.99m));
			_booksRepository.database.Add(new Book("Mistrz i Małgorzata", "Michaił Bułhakow", 1965, "JJJJ", 10, 36.99m));
		}

		private void OrdersRepositorySeed()
		{
			List<BookOrdered> list1 = new List<BookOrdered>();
			list1.Add(new BookOrdered(_booksRepository.database[2], 1));
			list1.Add(new BookOrdered(_booksRepository.database[3], 7));
			_ordersRepository.database.Add(new Order(list1));

			List<BookOrdered> list2 = new List<BookOrdered>();
			list2.Add(new BookOrdered(_booksRepository.database[0], 2));
			list2.Add(new BookOrdered(_booksRepository.database[5], 3));
			list2.Add(new BookOrdered(_booksRepository.database[6], 1));
			_ordersRepository.database.Add(new Order(list2));

			List<BookOrdered> list3 = new List<BookOrdered>();
			list3.Add(new BookOrdered(_booksRepository.database[9], 1));
			_ordersRepository.database.Add(new Order(list3));
		}
	}
}