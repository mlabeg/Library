namespace Library.Domain
{
    public class BookOrdered
    {
        public int Amount { get; set; }
        public Book _bookOrdered { get; }

        public BookOrdered(Book bookOrdered, int amountOrdered)
        {
            _bookOrdered = bookOrdered;
            Amount = amountOrdered;
        }

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