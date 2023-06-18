using MenuUITools;
using Library.Domain;
using Library.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Book book = new Book("Zamieć", "Neal Stephenson", 1992, "83-7150-531-0", 1, 29.50m);
			BooksRepository repository = new BooksRepository();
			BooksService booksService = new BooksService(repository);
			OrdersRepository ordersRepository = new OrdersRepository();
			OrderService orderService = new OrderService(ordersRepository, repository);

			Menu menu = new Menu();
			menu.Konfiguruj(new string[] { "Dodaj", "Usun", "Wypisz", "Zmien", "Dodaj zamowienie", "Lista zamowien", "Wyjdz" });


			int inputCommand;
			do
			{
				Console.Clear();
				inputCommand = menu.Wyswietl();

				if (inputCommand >= 0)
				{
					switch (inputCommand)
					{
						case 0:
							Console.WriteLine("proba dodania ksiazki");
							booksService.AddBook();
							break;
						case 1:
							//Console.WriteLine("proba usuniecia ksiazki");
							booksService.Remove();
							break;
						case 2:
							if (!booksService.ListBooks())
							{
								Console.WriteLine("Brak książek w repozytorium!");
								Console.ReadKey();
							}
							Console.ReadKey();
							break;
						//TODO wyrównać wszystkie nazwiska wyświetlane w konsoli
						case 3:
							Console.WriteLine("proba zmiany stanu magazynowego ksiazek");
							booksService.ChangeStat();
							break;
						case 4:
							if (orderService.PlaceOrder())
							{
								Console.WriteLine("pomyślnie dodano zamówienie!");
							}
							else
							{
								Console.WriteLine("Błąd przy dodawaniu zamówienia!");
							}

							break;
						case 5:
							foreach (Order o in orderService.ListAll())
							{
								Console.WriteLine(o.Date + ": ");
								foreach (BookOrdered b in o.BooksOrderedList)
								{
									var bookOrdered = repository.BookInfo(b.BookId);
									Console.WriteLine($"\"{bookOrdered.Title}\" {bookOrdered.Author} wypożyczona ilość {b.NumerOrdered} sztuki");
								}
								Console.WriteLine();
							}
							break;
						case 6:
							break;
						default:
							Console.WriteLine("Niepoprawna komenda, spróbuj jeszcze raz.");
							break;
					}
				}
			} while (!(inputCommand == -1 || inputCommand == 6));
		}
	}
}
