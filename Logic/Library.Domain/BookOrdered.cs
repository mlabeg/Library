namespace Library.Domain
{
	public class BookOrdered
	{
		public BookOrdered(Book bookOrdered, int numerOrdered)
		{
			_bookOrdered = bookOrdered;
			Amount = numerOrdered;
		}

		public Book _bookOrdered { get; }

		public int Amount { get; set; }

		public Book GetOrderedBook()
		{
			return _bookOrdered;
		}

		public string GetBookAndAmount()
		{
			return _bookOrdered.Title.PadRight(25) + Amount;
		}

		public void ReturnOrderedBooks()
		{
			_bookOrdered.ProductsAvailable += Amount;
		}
	}
}