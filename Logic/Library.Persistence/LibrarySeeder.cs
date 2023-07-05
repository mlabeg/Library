using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence
{
	internal class LibrarySeeder
	{
		private readonly BooksRepository _booksRepository;
		private readonly OrdersRepository _ordersRepository;

		public LibrarySeeder(BooksRepository booksRepository, OrdersRepository ordersRepository)
		{
			_booksRepository = booksRepository;
			_ordersRepository = ordersRepository;
		}

	}
}
