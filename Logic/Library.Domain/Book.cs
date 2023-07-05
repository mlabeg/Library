namespace Library.Domain
{
	public class Book
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public int PublicationYear { get; set; }
		public string ISBN { get; set; }

		public int ProductsAvailable { get; set; }
		public int ProductsTotal { get; set; }
		public decimal Price { get; set; }

		public int State { get; set; }      //0 - unavaliable, 1 - avaliable
											//TODO ? sprawdz czy potrzebne

		public Book()
		{
			State = 1;
		}

		public Book(string title, string author, int publicationYear, string isbn, int productsAvailable, decimal price)
		{
			Title = title;
			Author = author;
			ISBN = isbn;
			PublicationYear = publicationYear;
			ProductsAvailable = productsAvailable;
			ProductsTotal = productsAvailable;
			Price = price;
		}

		public override string ToString()
		{
			return $"Title: {Title} Author: {Author} ProductsAvailable: {ProductsAvailable}";
		}
	}
}